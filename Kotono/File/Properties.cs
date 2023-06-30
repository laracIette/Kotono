using System;
using System.Collections.Generic;
using System.Linq;
using IO = System.IO;

namespace Kotono.File
{
    internal class Properties
    {
        internal string Path { get; }

        internal Data Data { get; }

        internal Properties(string path, Data data)
        {
            Path = path;
            Data = data;
        }

        internal static Properties Parse(string path)
        {
            if (!path.EndsWith(".ktf"))
            {
                throw new FormatException($"error: file path \"{path}\" must end with \".ktf\"");
            }

            var fileString = IO.File.ReadAllText(path);
            var tokens = fileString.Split("\r\n");
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
                if (type == "array")
                {
                    parent += key + '.';
                }

                if (type == "string")
                {
                    data.Strings[parent + key] = value;
                }
                if (type == "float")
                {
                    data.Floats[parent + key] = float.Parse(value);
                }
                if (type == "double")
                {
                    data.Doubles[parent + key] = double.Parse(value);
                }
                if (type == "int")
                {
                    data.Ints[parent + key] = int.Parse(value);
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

            var tokens = str.Split(':');
            if (tokens.Length != 2)
            {
                throw new Exception($"error: tokens \"{tokens}\" Length \"{tokens.Length}\" must be of \"2\"");
            }



            const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            tokens[0] = new string(tokens[0].Where(allowedChars.Contains).ToArray()) ?? throw new Exception($"error: token string must not be empty");
            tokens[1] = new string(tokens[1].Where((allowedChars + ".-{").Contains).ToArray()) ?? throw new Exception($"error: token string must not be empty");
            bool isNegative = false;
            // if the first character is '-'
            if (tokens[1][0] == '-')
            {
                // if there's only one '-', it's negative
                if (tokens[1].Where(c => c == '-').ToArray().Length == 1)
                {
                    isNegative = true;
                }
            }
            tokens[1] = new string(tokens[1].Where((allowedChars + ".{").Contains).ToArray()) ?? throw new Exception($"error: token string must not be empty");

            string type;

            const string letters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";


            // it's an array
            if (tokens[1].Contains('{'))
            {
                if (tokens[1].Length != 1)
                {
                    throw new Exception("error: the only character opening an array should be '{'");
                }
                else
                {
                    type = "array";
                }
            }
            // if has more than 1 letter, it's a string
            else if (tokens[1].Where(letters.Contains).ToArray().Length > 1)
            {
                type = "string";
            }
            // if has 1 letter, it can be either a float or a string
            else if (tokens[1].Where(letters.Contains).ToArray().Length == 1)
            {
                // it's a float
                if (tokens[1][^1] == 'f')
                {
                    type = "float";
                    tokens[1] = tokens[1].Remove(tokens[1].Length - 1);
                }
                // it's a string
                else
                {
                    type = "string";
                }
            }
            // it's a number
            else
            {
                if (tokens[1].Contains('.'))
                {
                    // there is more than 1 '.'
                    if (tokens[1].Where(c => c == '.').ToArray().Length != 1)
                    {
                        throw new Exception($"error: supposed number \"{tokens[1]}\" contains more than 1 '.'");
                    }
                    // it's a double
                    else
                    {
                        type = "double";
                    }
                }
                // it's an int
                else
                {
                    type = "int";
                }
            }

            if (isNegative && ((type == "float") || (type == "double") || (type == "int")))
            {
                tokens[1] = tokens[1].Insert(0, "-");
            }

            return Tuple.Create(tokens[0], type, tokens[1]);
        }

        internal void WriteFile()
        {
            string text = "# Kotono Properties File\n";

            var keyValues = Data.ToString().Split('\n').Where(s => s != "").ToArray();
            Array.Sort(keyValues);

            // contains the parents path already seen
            var writtenParents = new List<string[]>();

            foreach (var keyValue in keyValues)
            {
                // parents path, ending with the value
                var parrents = keyValue.Split('.');

                // if it's not only the value / means if it has parents
                if (parrents.Length > 1)
                {
                    // i from 1 to parents.Length - 1
                    for (int i = 0; i < parrents.Length - 1; i++)
                    {
                        // if parents paths doesn't contain path to i + 1
                        if (!writtenParents.Where(a => AreEqual(a, parrents[..^(i + 1)])).Any())
                        {
                            // add it
                            writtenParents.Add(parrents[..^(i + 1)]);

                            // indent text
                            for (int j = 0; j < i; j++)
                            {
                                text += '\t';
                            }
                            // add it
                            text += parrents[i] + '\n';
                        }
                    }

                }

                for (int i = 0; i < parrents.Length - 1; i++)
                {
                    text += '\t';
                }
                text += parrents[^1] + '\n';
            }

            IO.File.WriteAllText(KT.KotonoPath + "Assets/cube.kt", text);
        }

        private static bool AreEqual(string[] a, string[] b)
        {
            if (a.Length != b.Length)
            {
                return false;
            }
            for (int i = 0; i < a.Length; i++)
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
