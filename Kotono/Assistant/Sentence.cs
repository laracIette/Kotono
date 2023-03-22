using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks.Sources;

namespace Kotono.Assistant
{
    public class Sentence
    {
        public string Text { get; private set; }

        public string Reply { get; private set; }

        public int Register { get; private set; } = 0;

        public List<WordAttribute> Attributes { get; private set; } = new();

        public Sentence(string text) 
        {
            Text = text;
            Analyze();
            Reply = CreateReply();
        }

        private void Analyze()
        {
            var words = new List<Word>();

            foreach (var word in Text.Split(' '))
            {
                var addWord = WordBank.Words.Where(w => w.Text == word);
                if (addWord.Count() > 0)
                {
                    words.Add(addWord.First());
                }
            }
            
            foreach (var word in words)
            {
                Register += (int)word.Register;

                foreach (var attribute in word.Attributes)
                {
                    Attributes.Add(attribute);
                }
            }

            if (words.Count > 0) Register /= words.Count;
        }

        private string CreateReply()
        {
            string reply = "";

            foreach (var word in WordBank.Words)
            {
                if (word.AreAttributesSimilar(Attributes.ToArray()))
                {
                    reply += word.Text + ' ';
                }
            }

            return reply;
        }
    }
}
