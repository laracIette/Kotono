using System;
using System.Collections.Generic;
using System.Linq;
using IO = System.IO;

namespace Kotono.File
{
    internal class Properties
    {
        internal string Path { get; }

        private readonly Data _data;

        //internal DataDict<string> Strings => _data.Strings;

        //internal DataDict<float> Floats => _data.Floats;

        //internal DataDict<double> Doubles => _data.Doubles;

        //internal DataDict<int> Ints => _data.Ints;

        internal Dictionary<string, string> Strings => _data.Strings;

        internal Dictionary<string, float> Floats => _data.Floats;

        internal Dictionary<string, double> Doubles => _data.Doubles;

        internal Dictionary<string, int> Ints => _data.Ints;

        internal Properties(string path, Data data)
        {
            Path = path;
            _data = data;
        }

        internal static Properties Parse(string path)
        {
            if (!path.EndsWith(".ktf"))
            {
                throw new FormatException($"error: file path \"{path}\" must end with \".ktf\"");
            }

            var fileString = IO.File.ReadAllText(path);
            var tokens = fileString.Split("\n");
            if (tokens[0] != "# Kotono Properties File")
            {
                throw new Exception($"error: file type must be \"properties\", file must start with \"# Kotono Properties File\"");
            }

            Data data = new();
            string parent = "";

            for (int i = 1; i < tokens.Length; i++)
            {
                // if line is empty, skip
                if (tokens[i] == "")
                {
                    continue;
                }

                // line with '}' should never have anything else, so go back in hierarchy and skip
                if (tokens[i].Contains('}'))
                {
                    parent = RemoveParent(parent);
                    continue;
                }

                (var key, var type, var value) = GetKeyValue(tokens[i]);

                switch (type)
                {
                    case "array":
                        parent += key + '.';
                        break;

                    case "string":
                        data.Strings[parent + key] = value;
                        break;

                    case "float":
                        data.Floats[parent + key] = float.Parse(value);
                        break;

                    case "double":
                        data.Doubles[parent + key] = double.Parse(value);
                        break;

                    case "int":
                        data.Ints[parent + key] = int.Parse(value);
                        break;

                    default:
                        break;
                }
            }

            return new Properties(path, data);
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

        /// <returns> A tuple with Item1 being the key, Item2 being the type of the value, Item3 being the value </returns>
        private static Tuple<string, string, string> GetKeyValue(string str)
        {
            if (!str.Contains(':'))
            {
                throw new FormatException($"error: string \"{str}\" must contain \":\" to be a Key / Value string");
            }
            
            // separate key / value
            int firstColonIndex = str.IndexOf(':');

            string key = str[..firstColonIndex];
            string value = str[(firstColonIndex + 1)..];

            const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            key = new string(key.Where(allowedChars.Contains).ToArray()) ?? throw new Exception($"error: key string must not be empty");

            // if starts with ' ', remove this first ' ' (should always be true)
            if (value.StartsWith(' '))
            {
                value = value.Remove(0, 1);
            }

            string type;

            bool isNegative = key.First() == '-';

            // it's an array
            if (value == "{")
            {
                type = "array";
            }
            // it's a string
            else if (IsAString(value))
            {
                type = "string";
            }
            // if last letter is 'f', it's a float
            else if (value.Last() == 'f')
            {
                type = "float";
                value = value.Remove(value.Length - 1);
            }
            // if it has a '.', it's a double
            else if (value.Contains('.'))
            {
                type = "double";
            }
            // it's an int
            else
            {
                type = "int";
            }

            return Tuple.Create(key, type, value);
        }

        private static bool IsAString(string str)
        {
            const string numbers = "0123456789";

            var strNumbers = str.Where(numbers.Contains).ToList();

            // if there is a letter
            if ((str.Length - strNumbers.Count) > 0)
            {
                // if there is 1 letter
                if ((str.Length - strNumbers.Count) == 1)
                {
                    // if the 1 letter is a '.', or a 'f' and it's the last letter, or a '-' and it's the first letter
                    if (str.Any(c => c == '.') || (str.Last() == 'f') || (str.First() == '-'))
                    {
                        // it's not a string
                        return false;
                    }
                }
                // if there is 2 letters
                else if ((str.Length - strNumbers.Count) == 2)
                {
                    // if there is a dot
                    if (str.Any(c => c == '.'))
                    {
                        // if last is 'f' or first is '-'
                        if ((str.Last() == 'f') || (str.First() == '-'))
                        {
                            // it's not a string
                            return false;
                        }
                    }
                    // if last is 'f' and first is '-'
                    else if ((str.Last() == 'f') && (str.First() == '-'))
                    {
                        // it's not a string
                        return false;
                    }
                }
                // if there is 3 letters
                else if ((str.Length - strNumbers.Count) == 3)
                {
                    // if it has a '.' and the last is 'f' and the first is '-'
                    if (str.Any(c => c == '.') && (str.Last() == 'f') && (str.First() == '-'))
                    {
                        // it's not a string
                        return false;
                    }
                }

                // it's a string
                return true;
            }

            else
            {
                // it's not a string
                return false;
            }
        }

        internal void WriteFile()
        {
            string text = "# Kotono Properties File\n\n";

            var keyValues = _data.ToString().Split('\n').Where(s => s != "").ToList();
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

            IO.File.WriteAllText(Path, result);
        }

        private static bool AreEqual(List<string> a, List<string> b)
        {
            if (a.Count != b.Count)
            {
                return false;
            }
            for (int i = 0; i < a.Count; i++)
            {
                if (a[i] != b[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
