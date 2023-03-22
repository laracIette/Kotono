using System;

namespace Kotono.Assistant
{
    public class Partner : IDisposable
    {
        public Partner() 
        { 
        }

        public void Run()
        {
            string? input;
            do
            {
                input = Console.ReadLine();
            }
            while(input == null);

            var sentence = new Sentence(input);
            Console.WriteLine(sentence.Reply);
        }

        public void Dispose()
        {
        }
    }
}
