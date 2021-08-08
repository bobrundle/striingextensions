using System;

namespace StringSupportExamples
{
    public static class StringExtensions1
    {
        public static int GetValue(this string s)
        {
            return int.Parse(s);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Parsing1();
            Parsing2();
            var cf = new ConfigFile();
            cf.BoardSize = new BoardSize() { Width = 100, Height = 60 };
            cf.DifficultySetting = ConfigFile.Difficulty.Hard;
            Console.WriteLine($"Before save: {cf}");
            cf.Save();
            cf = new ConfigFile();
            Console.WriteLine($"After reload: {cf}");
         }
        private static void Parsing1()
        {
            string s = "123";
            int i = int.Parse(s);
            Console.WriteLine($"i = {i}");
        }

        private static void Parsing2()
        {
            String s = "123";
            int i = s.GetValue();
            Console.WriteLine($"i = {i}");
        }
    }
}
