using Kotono.Graphics.Objects.Settings;
using Kotono.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using IO = System.IO;

namespace Kotono.File
{
    internal static class Settings
    {
        private const string ALLOWED_CHARS = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        internal static T Parse1<T>(string path) where T : DrawableSettings
        {
            if (!path.EndsWith(".ktf"))
            {
                throw new FormatException($"error: file path \"{path}\" must end with \".ktf\"");
            }

            var tokens = IO.File.ReadAllText(path).Replace("\r", "").Split("\n").ToList();

            if (tokens[0] != "# Kotono Settings File")
            {
                throw new Exception($"error: file type must be \"properties\", file must start with \"# Kotono Settings File\"");
            }
            else
            {
                tokens.RemoveAt(0);
            }

            var data = new Dictionary<string[], string>();

            ExtractData();

            var members = new Dictionary<MemberInfo, string[]>();

            // List of parents 
            var currentParents = new List<string>();

            T settings = Activator.CreateInstance<T>();

            ExtractMembers(typeof(T));

            foreach (var (member, parents) in members)
            {
                // If data has a value corresponding to parents
                var value = data.Any(p => p.Key.SequenceEqual(parents))
                    ? data.First(p => p.Key.SequenceEqual(parents)).Value
                    : null;

                if (value != null)
                {
                    // Kotono types
                    if (parents.Length > 1)
                    {
                        // works but bad
                        string key = parents[^2];

                        string[] values = [.. data.Where(p => p.Key[0] == key).ToDictionary().Values];

                        switch (settings)
                        {
                            case AnimationSettings animation:
                                SetAnimationSettingsValues(animation, key, values);
                                break;

                            case Object2DSettings object2d:
                                SetObject2DSettingsValues(object2d, key, values);
                                break;

                            case Object3DSettings object3d:
                                SetObject3DSettingsValues(object3d, key, values);
                                break;

                            default:
                                break;
                        }
                    }
                    // Built-in types
                    else
                    {
                        member.SetValue(settings, value);
                    }
                }
            }

            return settings;


            void ExtractData()
            {
                var currentParents = new List<string>();

                foreach (var token in tokens)
                {
                    // if line is empty, skip
                    if (token == "")
                    {
                        continue;
                    }

                    // line with only '}' goes to the precedent parent
                    if ((token.Count(c => !"\t".Contains(c)) == 1) && token.Contains('}'))
                    {
                        currentParents.RemoveLast();
                    }
                    else if (IsPair(token, out string key, out string value))
                    {
                        data[[.. currentParents, key]] = value;
                    }
                    else
                    {
                        currentParents.Add(key);
                    }
                }
            }

            void ExtractMembers(Type type)
            {
                // For each member of type's parsable members
                foreach (var member in type.GetMembers().OfAttribute<ParsableAttribute>())
                {
                    // Add the member's name to the parents list
                    currentParents.Add(member.Name);

                    // Parents of the member is the current parents
                    members[member] = [.. currentParents];

                    ExtractMembers(member.MemberType());
                }

                // Remove last parent once it has been parsed
                currentParents.RemoveLast();
            }

            void SetAnimationSettingsValues(AnimationSettings animation, string key, string[] values)
            {
                SetObject2DSettingsValues(animation, key, values);

                switch (key)
                {
                    case "Color":
                        // Reorder for R, G, B, A
                        (values[0], values[1], values[2], values[3]) = (values[3], values[2], values[1], values[0]);
                        animation.Color = Color.Parse(values);
                        break;

                    default:
                        break;
                }
            }

            void SetObject2DSettingsValues(Object2DSettings object2d, string key, string[] values)
            {
                switch (key)
                {
                    case "Dest":
                        // Reorder for X, Y, W, H
                        (values[0], values[1], values[2], values[3]) = (values[2], values[3], values[1], values[0]);
                        object2d.Dest = Rect.Parse(values);
                        break;

                    default:
                        break;
                }
            }

            void SetObject3DSettingsValues(Object3DSettings object3d, string key, string[] values)
            {
                // works but bad
                // maybe just remove transform in .ktf but would just delay the issue
                switch (key)
                {
                    case "Location":
                        object3d.Location = Vector.Parse(values);
                        break;

                    case "Rotation":
                        object3d.Rotation = Vector.Parse(values);
                        break;

                    case "Scale":
                        object3d.Scale = Vector.Parse(values);
                        break;

                    default:
                        break;
                }
            }
        }

        internal static T Parse<T>(string path) where T : DrawableSettings
        {
            if (!path.EndsWith(".ktf"))
            {
                throw new FormatException($"error: file path \"{path}\" must end with \".ktf\"");
            }

            var tokens = IO.File.ReadAllText(path).Replace("\r", "").Split("\n").ToList();

            if (tokens[0] != "# Kotono Settings File")
            {
                throw new FormatException($"error: file type must be \"properties\", file must start with \"# Kotono Settings File\"");
            }
            else
            {
                tokens.RemoveAt(0);
            }

            T settings = Activator.CreateInstance<T>();

            foreach (var token in tokens)
            {
                if (token == "")
                {
                    continue;
                }

                var parts = token.Split(' ');

                var type = Type.GetType(parts[0]) ?? throw new Exception($"error: specified type \"{parts[0]}\" doesn't exist.");

                var name = parts[1] ?? throw new Exception($"error: no specified name.");

                var value = parts[2..] ?? throw new Exception($"error: no specified value.");

                foreach (var member in typeof(T).GetMembers().OfAttribute<ParsableAttribute>())
                {
                    if (member.MemberType() == type && member.Name == name)
                    {
                        switch (type.FullName)
                        {
                            case "Kotono.Utils.Color":
                                break;

                            case "Kotono.Utils.Rect":
                                break;

                            default:
                                member.SetValue(settings, value[0]);
                                break;
                        }
                    }
                }
            }

            return settings;
        }

        /// <summary>
        /// Determines if the passed string is a pair or opening an array.
        /// </summary>
        /// <param name="str"> The string to analyze. </param>
        /// <param name="key"> The key if the function returns true, else an empty string. </param>
        /// <param name="value"> The value if the function returns true, else an empty string. </param>
        /// <returns> <see langword="true"/> if the passed string is a pair, <see langword="false"/> if the passed string is opening an array. </returns>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="Exception"></exception>
        private static bool IsPair(string str, out string key, out string value)
        {
            if (!str.Contains(':'))
            {
                throw new FormatException($"error: string \"{str}\" must contain \":\" to be a Key / Value string");
            }

            // separate key / value
            int firstColonIndex = str.IndexOf(':');

            key = str[..firstColonIndex];
            value = str[(firstColonIndex + 1)..];

            key = new string(key.Where(ALLOWED_CHARS.Contains).ToArray()) ?? throw new Exception($"error: key string must not be empty");

            // if starts with ' ', remove this first ' ' (should always be true)
            if (value.StartsWith(' '))
            {
                value = value.Remove(0, 1);
            }

            // it's an array
            if (value == "{")
            {
                return false;
            }

            return true;
        }

        internal static void WriteFile<T>(string path, T settings) where T : DrawableSettings
        {
            string text = "# Kotono Settings File\n\n";

            foreach (var member in settings.GetType().GetMembers().OfAttribute<ParsableAttribute>())
            {
                text += $"{member.MemberType()} {member.Name}";

                var value = member.GetValue(settings);

                if (value?.GetType().Namespace?.StartsWith("Kotono") ?? false)
                {
                    AddMemberValue(member.GetValue(settings));
                }
                else
                {
                    text += $" {member.GetValue(settings)}";
                }

                text += "\n";

                void AddMemberValue(object? obj)
                {
                    if (obj == null)
                    {
                        throw new Exception("error: obj shouldn't be null.");
                    }

                    // For each member of type's parsable members
                    foreach (var member in obj.GetType().GetMembers().OfAttribute<ParsableAttribute>())
                    {
                        text += $" {member.GetValue(obj)}";

                        AddMemberValue(member.GetValue(obj));
                    }
                }
            }

            IO.File.WriteAllText(path, text.Sorted("\n", "\n").Trim());
        }
    }
}
