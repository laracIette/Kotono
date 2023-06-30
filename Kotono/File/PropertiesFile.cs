using Mosaik.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using IO = System.IO;

namespace Kotono.File
{
    internal class PropertiesFile
    {
        internal string Path { get; }

        private readonly Dictionary<string, float> _floats;

        internal PropertiesFile(string path) 
        {
            Path = path;
            _floats = new();
        }

        internal static PropertiesFile Parse(string path) 
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
            for (int i = 1; i < tokens.Length; i++)
            {
                Console.WriteLine(tokens[i]);
                if (tokens[i].Contains('}')) continue;
                if (tokens[i] == "") continue;
                
                var tuple = GetKeyValue(tokens[i]);
                if (tuple.Item2 == "string")
                {
                    Console.WriteLine("string:" + tuple.Item3.ToString());
                }
                if (tuple.Item2 == "float")
                {
                    Console.WriteLine("float:" + float.Parse(tuple.Item3).ToString());
                }
                if (tuple.Item2 == "double")
                {
                    Console.WriteLine("double:" + double.Parse(tuple.Item3).ToString());
                }
                if (tuple.Item2 == "int")
                {
                    Console.WriteLine("int:" + int.Parse(tuple.Item3).ToString());
                }
                if (tuple.Item2 == "undefined")
                {
                    Console.WriteLine("undefined:" + tuple.Item3.ToString());
                }
            }

            return new PropertiesFile(path);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns> A tuple with Item1 being the key, Item2 being the type of the value, Item3 being the value </returns>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="Exception"></exception>
        internal static Tuple<string, string, string> GetKeyValue(string str)
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
            
            return Tuple.Create(tokens[0], type, tokens[1]);
        }

        internal float GetFloat(string name)
        {
            if (_floats.TryGetValue(name, out float value))
            {
                return value;
            }
            else
            {
                throw new Exception($"error: _floats Dictionary doesn't contain the key \"{name}\"");
            }
        }

        internal void SetFloat(string name, float data)
        {
            _floats[name] = data;
        }
    }
}
