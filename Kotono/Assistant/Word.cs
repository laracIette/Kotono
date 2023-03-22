using System;
using System.Linq;

namespace Kotono.Assistant
{
    public struct Word
    {
        public string Text { get; }

        public string[] Synonyms { get; }

        public WordClass Class { get; }

        public WordRegister Register { get; }

        public WordAttribute[] Attributes { get; }

        public Word(string text, string[] synonyms, WordClass wordClass, WordRegister wordRegister, WordAttribute[] wordAttributes) 
        {
            Text = text;
            Synonyms = synonyms;
            Class = wordClass;
            Register = wordRegister;
            Attributes = wordAttributes;
        }

        public bool AreAttributesSimilar(WordAttribute[] wordAttributes)
        {
            float inCommon = 0;

            foreach (var attribute in Attributes)
            {
                if (wordAttributes.Contains(attribute))
                {
                    inCommon++;
                }
            }

            return (inCommon / (float)wordAttributes.Length >= 0.5f) ? true : false;
        }
    }

    public enum WordClass
    {
        Noun,
        Verb,
        Adjective,
        Adverb,
        Pronoun,
        Preposition,
        Conjunction,
        Interjection
    }

    public enum WordRegister
    {
        Slang = -7,
        Informal = -5,
        Jargon = -3,
        Colloquial = -2,
        Neutral = 0,
        Technical = 3,
        Formal = 4,
        Literary = 5
    }

    public enum WordAttribute
    {
        Greeting,
        Positive,
        Negative
    }
}
