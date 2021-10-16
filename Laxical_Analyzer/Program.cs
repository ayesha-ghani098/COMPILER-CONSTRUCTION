using System;
using System.Collections.Generic;


namespace Laxical_Analyzer
{
    class Program
	{
		static void Main(string[] args)
		{
            // Words dictionary
            var words = new Dictionary<int, Tuple<int, string>>();


            // File path
            string path = @"C:\Users\Technolet\Desktop\Laxical_Analyzer\Laxical_Analyzer\input.txt";

            Tools t = new Tools();
            string fileText = t.readFile(path);
            fileText += " ";


            // defined line number
            int lineNumberCode = 1;

            Essentials essenials = new Essentials();
            LaxicalAnalyxer la = new LaxicalAnalyxer(lineNumberCode, essenials.operators, essenials.punctuators);
            words = la.BreakWord(ref fileText);


            // looping through wordlist dictionary
            foreach (KeyValuePair<int, Tuple<int, string>> data in words)
                Console.WriteLine(data.Value);


        }
	}
}