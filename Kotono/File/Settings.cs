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

        internal static T Parse<T>(string path) where T : DrawableSettings
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

                KT.Log($"{member.MemberType()}, {string.Join('.', parents)}, {value}");

                if (value != null)
                {
                    // Kotono types
                    if (parents.Length > 1)
                    {
                        string type = parents[0];

                        string[] values = [.. data.Where(p => p.Key[0] == type).ToDictionary().Values];

                        if (settings is AnimationSettings animation)
                        {
                            switch (type)
                            {
                                case "Color":
                                    // Reorder for R, G, B, A
                                    (values[0], values[1], values[2], values[3]) = (values[3], values[2], values[1], values[0]);
                                    animation.Color = Color.Parse(values);
                                    break;
                            }
                        }
                        if (settings is Object2DSettings object2d)
                        {
                            switch (type)
                            {
                                case "Dest":
                                    // Reorder for X, Y, W, H
                                    (values[0], values[1], values[2], values[3]) = (values[2], values[3], values[1], values[0]);
                                    object2d.Dest = Rect.Parse(values);
                                    break;
                            }
                        }
                    }
                    // Built-in types
                    else
                    {
                        member.SetValue(settings, value);
                    }
                }
            }

            KT.Log();
            KT.Log(settings);
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

        internal static void WriteFile<T>(string path, T properties) where T : DrawableSettings
        {
            string text = "# Kotono Settings File\n\n";

            var keyValues = properties.ToString().Split('\n').Where(s => s != "").ToList();
            keyValues.Sort();

            // contains the parents path already seen
            var writtenParents = new List<List<string>>();

            foreach (var keyValue in keyValues)
            {
                // separate key / value
                int firstColonIndex = keyValue.IndexOf(':');

                string key = keyValue[..firstColonIndex];
                string value = keyValue[firstColonIndex..];

                // parents path, ending with the value
                var parents = key.Split('.').ToList();

                // i from 1 to parents.Count / means it has to have at least 1 parent
                for (int i = 1; i < parents.Count; i++)
                {
                    var currentParents = parents.GetRange(0, i);

                    // if parents paths doesn't contain path to i
                    if (!writtenParents.Any(a => a.SequenceEqual(currentParents)))
                    {
                        // add it
                        writtenParents.Add(currentParents);

                        // indent text
                        for (int j = 0; j < i - 1; j++)
                        {
                            text += '\t';
                        }

                        // add last parent of current path and opening brace
                        text += currentParents.Last() + ": {" + '\n';
                    }
                }

                // indent
                for (int i = 0; i < parents.Count - 1; i++)
                {
                    text += '\t';
                }
                // add the value
                text += parents.Last() + value + '\n';
            }

            // add closing braces
            string result = "";
            var textLines = text.Split('\n');
            for (int i = 0; i < textLines.Length - 1; i++)
            {
                result += textLines[i] + "\n";

                var currentIndent = textLines[i].Count(c => c == '\t');
                var nextIndent = textLines[i + 1].Count(c => c == '\t');

                if (currentIndent > nextIndent)
                {
                    for (int j = currentIndent; j > nextIndent; j--)
                    {
                        for (int k = 0; k < j - 1; k++)
                        {
                            result += "\t";
                        }
                        result += "}\n";
                    }
                }
            }

            IO.File.WriteAllText(path, result);
        }
    }
}
