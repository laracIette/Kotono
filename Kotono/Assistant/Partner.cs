using Catalyst;
using Catalyst.Models;
using Mosaik.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Random = Kotono.Utils.Random;

namespace Kotono.Assistant
{
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

    public class Word
    {
        private static readonly string[] _greetings =
        {
            "hello", "greetings", "sup", "yo", "hey", "goods morning", "mornin", "hi"
        };

        private static readonly string[] _positive =
        {
            "nice", "good", "cool", "ok", "yes", "awesome"
        };

        private static readonly string[] _negative =
        {
            "ugly", "bad", "horrible", "no" , "cringe", "meh"
        };

        public string Text { get; }

        public PartOfSpeech Tag { get; }

        public List<WordAttribute> Attributes { get; }

        public Word(string text, PartOfSpeech tag)
        {
            Text = text;
            Tag = tag;

            Attributes = new List<WordAttribute>();
            if (_greetings.Where(w => w == Text).Any())
            {
                Attributes.Add(WordAttribute.Greeting);
            }

            if (_positive.Where(w => w == Text).Any())
            {
                Attributes.Add(WordAttribute.Positive);
            }
            if (_negative.Where(w => w == Text).Any())
            {
                Attributes.Add(WordAttribute.Negative);
            } // TODO: evaluate positive and negative level of sentence
        }
    }

    public class Sentence
    {
        public string Text { get; private set; } = "";

        public string Reply { get; private set; } = "";

        public List<Word> Words { get; private set; } = new();

        public List<Word> Themes { get; private set; } = new();

        // Describes the positivity / negativity ratio of the sentence > 0 is positive while < 0 is negative
        public float Polarity { get; private set; } = 0.0f;

        public List<Word> Questions { get; private set; } = new();

        public List<Word> Greetings { get; private set; } = new();

        public List<Word> Likes { get; private set; } = new();

        public Sentence(List<Word> words) 
        {
            Words = words;

            SetText();
            SetThemes();
            SetPolarity();
            SetQuestion();
            SetGreeting();
            SetLike();
        }

        private void SetText()
        {
            for (int i = 0; i < Words.Count; i++)
            {
                Text += Words[i].Text + ' ';

                if ((i < Words.Count - 1) && Words[i + 1].Tag == PartOfSpeech.PUNCT)
                {
                    Text = Text.Remove(Text.Length - 1);
                }
            }
        }

        private void SetThemes()
        {
            foreach (var word in Words) 
            {
                if (word.Tag == PartOfSpeech.NOUN)
                {
                    Themes.Add(word);
                }
            }
        }

        private void SetPolarity()
        {
            float numAttributes = 0.0f;
            foreach (var word in Words)
            {
                if (word.Attributes.Contains(WordAttribute.Positive))
                {
                    Polarity++;
                    numAttributes++;
                }
                if (word.Attributes.Contains(WordAttribute.Negative))
                {
                    Polarity--;
                    numAttributes++;
                }
            }

            Polarity /= numAttributes;
        }

        private void SetQuestion()
        {
            string[] wordBank =
            {
                "?", "who", "what", "when", "where", "why", "how"
            };

            foreach (var word in Words)
            {
                if (wordBank.Contains(word.Text.ToLower()))
                {
                    Questions.Add(word);
                }
            }
        }

        private void SetGreeting()
        {
            string[] wordBank =
            {
                "hello", "greetings", "sup", "yo"
            };

            foreach (var word in Words)
            {
                if (wordBank.Contains(word.Text.ToLower()))
                {
                    Greetings.Add(word);
                }
            }
        }

        private void SetLike()
        {
            string[] wordBank =
            {
                "like", "love", "appreciate", "enjoy", "live for"
            };

            foreach (var word in Words)
            {
                if (wordBank.Contains(word.Text.ToLower()))
                {
                    Likes.Add(word);
                }
            }
        }

        private static string GetValue(string word, Dictionary<string, string> dict)
        {
            return dict.TryGetValue(word, out string? value) ? value : word;
        }

        public void CreateReply()
        {
            string theme = Themes.Any() ?
                    Themes.First().Text :
            "";

            string question = Questions.Any() ?
                Questions.First().Text :
            "";

            string like = Likes.Any() ?
                Likes.First().Text :
            "";

            string greeting = Greetings.Any() ?
                Greetings.First().Text :
            "";

            string subject = Words.Where(w => w.Tag == PartOfSpeech.PRON).Any() ?
                Words.Where(w => w.Tag == PartOfSpeech.PRON).First().Text :
            "";

            string verb = Words.Where(w => w.Tag == PartOfSpeech.VERB).Any() ?
                Words.Where(w => w.Tag == PartOfSpeech.VERB).First().Text :
                "";

            var switchDict = new Dictionary<string, string>
            {
                {"i", "you"}, {"me", "you"}, {"my" ,"your"}, {"mine", "yours"},
                {"you", "i"},                {"your" ,"my"}, {"yours" , "mine"},
                {"are", "am"}, {"am", "are"}
            };

            string[] beingFine =
            {
                "fine", "doing well", "alright", "okay"
            };

            if (greeting != "") // add greeting
            {
                Reply += char.ToUpper(greeting[0]) + greeting[1..greeting.Length];
            }

            if (question != "")
            {
                if (greeting != "") // if greeting before
                {
                    Reply += ", ";
                }

                if (question == "?") // if question is not who, what, where... ("Do you like food" for example)
                {
                    Reply += "yes"; // always yes for the moment

                    if (like != "") // if the sentence is related to liking
                    {
                        Reply += ", " + GetValue(subject.ToLower(), switchDict) + ' ' + like + ' ' + theme;
                    }
                }

                if (question.ToLower() == "how")
                {
                    if (subject == "you")
                    {
                        if (verb == "are")
                        {
                            Reply += GetValue(subject.ToLower(), switchDict) + ' ' + GetValue(verb, switchDict) + ' ' + beingFine[Random.Int(0, beingFine.Length)];
                        }
                    }
                }
            }

            Reply = (Reply == "") ? 
                "Sorry I didn't understand your sentence." : 
                Reply;
        }
    }

    public class Partner : IDisposable
    {
        public Partner()
        {
        }

        public static async Task Run()
        {
            English.Register();

            Storage.Current = new DiskStorage("catalyst-models");

            var nlp = await Pipeline.ForAsync(Language.English);
            var doc = new Document("Yo, do you like ice ? Really ? How are you ?", Language.English);
            nlp.ProcessSingle(doc);

            var sentences = new List<Sentence>();
            
            foreach (var sentence in doc.TokensData)
            {
                var words = new List<Word>();
                foreach (var word in sentence)
                {
                    var text = doc.Value[word.Bounds[0]..(word.Bounds[1] + 1)];
                    var tag = word.Tag;

                    words.Add(new Word(text, tag));
                }

                sentences.Add(new Sentence(words));
            }

            foreach (var sentence in sentences)
            {
                foreach (var word in sentence.Words)
                {
                    Console.WriteLine($"{word.Text}: {word.Tag}");
                }

                Console.WriteLine();
            }

            Analyze(doc.Value, sentences);
        }

        private static void Analyze(string text, List<Sentence> sentences)
        {
            Console.WriteLine(text);

            foreach (var sentence in sentences)
            {
                sentence.CreateReply();

                Console.WriteLine(CheckSyntax(sentence.Reply));
            }
        }

        private static string CheckSyntax(string sentence)
        {
            return sentence;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
