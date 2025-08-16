namespace VowelCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter your an English Word: ");
            string input = Console.ReadLine();
            var vowelsCount = CountOfVowels(input);
            Console.WriteLine("Vowel Counts: ");
            Console.WriteLine($"a: {vowelsCount['a']}");
            Console.WriteLine($"e: {vowelsCount['e']}");
            Console.WriteLine($"i: {vowelsCount['i']}");
            Console.WriteLine($"o: {vowelsCount['o']}");
            Console.WriteLine($"u: {vowelsCount['u']}");
        }

        //using the Dictionary type to loop over the word
        public static Dictionary<char, int> CountOfVowels(string input)
        {
            var counts = new Dictionary<char, int>
    {
        { 'a', 0 },
        { 'e', 0 },
        { 'i', 0 },
        { 'o', 0 },
        { 'u', 0 }
    };
            foreach (char c in input.ToLower())
            {
                if (counts.ContainsKey(c))
                {
                    counts[c]++;
                }
            }
            return counts;
        }
    }
}
