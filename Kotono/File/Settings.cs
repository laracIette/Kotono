using Kotono.Graphics.Objects.Settings;
using Kotono.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
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

            var fileString = IO.File.ReadAllText(path).Replace("\r", "");
            var tokens = fileString.Split("\n").ToList();
            
            if (tokens[0] != "# Kotono Properties File")
            {
                throw new Exception($"error: file type must be \"properties\", file must start with \"# Kotono Properties File\"");
            }

            tokens.RemoveAt(0);

            var data = new Dictionary<string, string>();
            
            string parent = "";

            foreach (var token in tokens)
            {
                // if line is empty, skip
                if (token == "")
                {
                    continue;
                }

                // line with only '}' goes to the precedent parent
                if ((token.Count(c => !"\t\n\r".Contains(c)) == 1) && token.Contains('}'))
                {
                    parent = RemoveParent(parent);
                    continue;
                }

                if (IsPair(token, out string key, out string value))
                {
                    data[parent + key] = value;
                }
                else
                {
                    parent += key + '.';
                }
            }

            //var dataManager = new DataManager(data);

            T obj = Activator.CreateInstance<T>();

            foreach (var property in typeof(T).GetProperties())
            {
                if (data.TryGetValue(property.Name, out string? value))
                {
                    object convertedValue = Convert.ChangeType(value, property.PropertyType);
                    property.SetValue(obj, convertedValue);
                }
                else
                {
                    object? prop = null;

                    switch (property.Name)
                    {
                        case "Color":
                            prop = new Color(
                                float.Parse(data[data.Keys.Where(k => k.EndsWith("Color.R")).First()]),
                                float.Parse(data[data.Keys.Where(k => k.EndsWith("Color.G")).First()]),
                                float.Parse(data[data.Keys.Where(k => k.EndsWith("Color.B")).First()]),
                                float.Parse(data[data.Keys.Where(k => k.EndsWith("Color.A")).First()])
                            );
                            break;

                        case "Dest":
                            prop = new Rect(
                                float.Parse(data[data.Keys.Where(k => k.EndsWith("Dest.X")).First()]),
                                float.Parse(data[data.Keys.Where(k => k.EndsWith("Dest.Y")).First()]),
                                float.Parse(data[data.Keys.Where(k => k.EndsWith("Dest.W")).First()]),
                                float.Parse(data[data.Keys.Where(k => k.EndsWith("Dest.H")).First()])
                            );
                            break;

                        case "Location":
                            prop = new Vector(
                                float.Parse(data[data.Keys.Where(k => k.EndsWith("Location.X")).First()]),
                                float.Parse(data[data.Keys.Where(k => k.EndsWith("Location.Y")).First()]),
                                float.Parse(data[data.Keys.Where(k => k.EndsWith("Location.Z")).First()])
                            );
                            break;

                        case "Rotation":
                            prop = new Vector(
                                float.Parse(data[data.Keys.Where(k => k.EndsWith("Rotation.X")).First()]),
                                float.Parse(data[data.Keys.Where(k => k.EndsWith("Rotation.Y")).First()]),
                                float.Parse(data[data.Keys.Where(k => k.EndsWith("Rotation.Z")).First()])
                            );
                            break;

                        case "Scale":
                            prop = new Vector(
                                float.Parse(data[data.Keys.Where(k => k.EndsWith("Scale.X")).First()]),
                                float.Parse(data[data.Keys.Where(k => k.EndsWith("Scale.Y")).First()]),
                                float.Parse(data[data.Keys.Where(k => k.EndsWith("Scale.Z")).First()])
                            );
                            break;

                        default:
                            break;
                    }

                    if (prop != null)
                    {
                        property.SetValue(obj, prop);
                    }
                }
            }

            return obj;
        }

        private static string RemoveParent(string key)
        {
            int dotIndex = 0;
            for (int i = 0; i < key.Length - 1; i++)
            {
                if (key[i] == '.')
                {
                    dotIndex = i;
                }
            }
            // if dotIndex == 0, there is no dot so don't ignore the first character
            return key.Remove(dotIndex + ((dotIndex == 0) ? 0 : 1));
        }

        /// <summary>
        /// Determines if the passed string is a pair or opening an array
        /// </summary>
        /// <param name="str">The string to analyze</param>
        /// <param name="key">The key if the function returns true, else an empty string</param>
        /// <param name="value">The value if the function returns true, else an empty string</param>
        /// <returns>true if the passed string is a pair, false if the passed string is opening an array</returns>
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
            string text = "# Kotono Properties File\n\n";

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
                    if (!writtenParents.Any(a => AreEqual(a, currentParents)))
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

        private static bool AreEqual(List<string> left, List<string> right)
        {
            if (left.Count != right.Count)
            {
                return false;
            }

            for (int i = 0; i < left.Count; i++)
            {
                if (left[i] != right[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
