using System;
using System.Collections.Generic;


namespace LaxicalAnalyzer
{ 
    class Program
    {
        static void Main(string[] args)
        {
            // File path
            // Hamza: D:\code\CC\Laxical_Analyzer\input.txt
            string path = @"D:\code\CC\Laxical_Analyzer\input.txt";

            // Read file
            Tools t = new Tools();
            string fileText = t.readFile(path);

            // Import predefined essentials
            Essentials essentials = new Essentials();

            // Laxical analyzer
            LaxicalAnalyzer la = new LaxicalAnalyzer(essentials.operators, essentials.punctuators,
                essentials.regexs, essentials.keywords);

            //// Words dictionary
            var words = new Dictionary<int, Tuple<int, string>>();
            words = la.BreakWord(ref fileText);
            var g = la.class_part(ref words);

            //// looping through wordlist dictionary
            foreach (var data in words)
                Console.WriteLine(data.Value);


            while (true) { }
        }
    }
}