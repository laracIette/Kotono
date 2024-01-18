﻿using Kotono.Graphics.Objects.Settings;
using Kotono.Utils;
using System;
using System.Linq;
using IO = System.IO;

namespace Kotono.File
{
    internal static class Settings
    {
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

                var values = parts[2..] ?? throw new Exception($"error: no specified value.");

                foreach (var member in typeof(T).GetMembers().OfAttribute<ParsableAttribute>())
                {
                    if (member.MemberType() == type && member.Name == name)
                    {
                        if (type.FullName?.StartsWith("Kotono") ?? false)
                        {
                            switch (settings)
                            {
                                case AnimationSettings animation:
                                    SetAnimationSettingsValues(animation, name, values);
                                    break;

                                case Object2DSettings object2d:
                                    SetObject2DSettingsValues(object2d, name, values);
                                    break;

                                case Object3DSettings object3d:
                                    SetObject3DSettingsValues(object3d, name, values);
                                    break;

                                default:
                                    break;
                            }
                        }
                        else
                        {
                            member.SetValue(settings, values[0]);
                        }
                    }
                }
            }

            return settings; 
            
            void SetAnimationSettingsValues(AnimationSettings animation, string member, string[] values)
            {
                SetObject2DSettingsValues(animation, member, values);

                switch (member)
                {
                    case "Color":
                        animation.Color = Color.Parse(values);
                        break;

                    default:
                        break;
                }
            }

            void SetObject2DSettingsValues(Object2DSettings object2d, string member, string[] values)
            {
                switch (member)
                {
                    case "Dest":
                        object2d.Dest = Rect.Parse(values);
                        break;

                    default:
                        break;
                }
            }

            void SetObject3DSettingsValues(Object3DSettings object3d, string member, string[] values)
            {
                switch (member)
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

        internal static void WriteFile<T>(string path, T settings) where T : DrawableSettings
        {
            string text = "# Kotono Settings File\n";

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
