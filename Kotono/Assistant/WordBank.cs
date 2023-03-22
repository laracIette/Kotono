namespace Kotono.Assistant
{
    public static class WordBank
    {
        private static readonly Word Hello = new("hello", new string[] { "greetings" }, WordClass.Interjection, WordRegister.Neutral, new WordAttribute[] { WordAttribute.Greeting });
        private static readonly Word Greetings = new("greetings", new string[] { "hello" }, WordClass.Interjection, WordRegister.Formal, new WordAttribute[] { WordAttribute.Greeting });


        public static readonly Word[] Words =
        {
            Hello, Greetings
        };
    }
}
