using System;
using System.Collections.Generic;


namespace ConsoleApp1 { 
    class Program
    {
        static void Main(string[] args)
        {
            // File path
            // Hamza: D:\code\CC\Laxical_Analyzer\input.txt
            string path = @"C:\Users\Hamza Arain\source\repos\ConsoleApp1\ConsoleApp1\input.txt";

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
            string text = "";
            foreach (var data in g) {
                Console.WriteLine(data.Value);
                text += data.Value + "\n";
            }

            t.writeFile(@"C:\Users\Hamza Arain\source\repos\ConsoleApp1\ConsoleApp1\write.txt", text);
            while(true) { }
        }
    }
}