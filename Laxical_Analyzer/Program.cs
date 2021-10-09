namespace Laxical_Analyzer
{
    class Program
	{
		static void Main(string[] args)
		{
            // File path
            string path = @"D:\code\CC\Laxical_Analyzer\input.txt";

            Tools t = new Tools();
            string fileText = t.readFile(path);
            fileText += " ";


            // defined line number
            int lineNumberCode = 1;

            Essentials essenials = new Essentials();
            LaxicalAnalyxer la = new LaxicalAnalyxer(lineNumberCode, essenials.operators, essenials.punctuators);
            la.breakWord(ref fileText);

            while (true) { }
        }
	}
}