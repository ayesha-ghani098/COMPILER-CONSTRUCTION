using System;
using System.IO;
using System.Collections.Generic;

namespace Laxical_Analyzer
{
	class Program
	{
		// file path to read input 
		static readonly string textFile = "C:/Users/Technolet/Desktop/Laxical_Analyzer/Laxical_Analyzer/input.txt";


		// defined line number
		static int lineNumber = 1;


		// dictionary for punctuators and its classpart
		static IDictionary<char,
		string> punctuators = new Dictionary<char,
		string>() {

			{
				'(',
				"("
			},
			{
				')',
				")"
			},
			{
				'{',
				"{"
			},
			{
				'}',
				"}"
			},
			{
				'[',
				"["
			},
			{
				']',
				"]"
			},
			{
				'@',
				"lexical_error"
			},
			{
				'$',
				"lexical_error"
			},
			{
				'^',
				"lexical_error"
			},
			{
				'`',
				"lexical_error"
			},
			{
				'~',
				"lexical_error"
			},
			{
				';',
				";"
			},
			{
				'.',
				"."
			},
			{
				':',
				"lexical_error"
			},
			{
				'\'',
				"lexical_error"
			},
			{
				'_',
				"lexical_error"
			},
			{
				',',
				","
			},
			{
				'?',
				"lexical_error"
			},
			{
				'\\',
				"lexical_error"
			},
		};



		// dictionary for operators and its classpart
		static IDictionary<string,
		string> operators = new Dictionary<string,
		string>() {

			{
				"+",
				"PM"
			},
			{
				"-",
				"PM"
			},
			{
				"*",
				"MD"
			},
			{
				"/",
				"MD"
			},
			{
				"<",
				"RO"
			},
			{
				">",
				"RO"
			},
			{
				"=",
				"AO"
			},
			{
				"&",
				"Lexeme error"
			},
			{
				"|",
				"Lexeme error"
			},
			{
				"!",
				"LO"
			},
			{
				"<=",
				"RO"
			},
			{
				">=",
				"RO"
			},
			{
				"!=",
				"RO"
			},
			{
				"==",
				"RO"
			},
			{
				"||",
				"LO"
			},
			{
				"&&",
				"LO"
			},
			{
				"+=",
				"AO"
			},
			{
				"-=",
				"AO"
			},

		};





		// Line Increment Function 
		static bool line_increase(ref int index)
		{
			lineNumber++;
			index++;
			return true;
		}

		// Checking if string is all digits
		static bool IsAllDigits(string s)
		{
			foreach (char c in s)
			{
				if (!char.IsDigit(c))
					return false;
			}
			return true;
		}





		// Single Line Comment
		static bool singleLineComment(ref string input, ref int index)
		{
			while (index < input.Length)
			{
				if (input[index] == '\r')
				{
					index++;
					line_increase(ref index);
					return true;
				}
				index++;
			}
			return true;
		}





		// Multiline Comment Function
		static bool multiLineComment(ref string input, ref int index)
		{
			while (index < input.Length)
			{
				if (input[index] == '\r')
				{
					index++;
					line_increase(ref index);

				}

				if (input[index] == '*')
				{
					if (input[index + 1] == '/')
					{
						index += 2;
						return true;
					}
				}
				index++;
			}
			return true;
		}










		// String Handler Function
		static string double_quote(ref string input, ref int index)
		{
			string temp = "";
			temp += input[index];
			index++;
			while (index < input.Length)
			{
				if (input[index] == '\r')
				{
					index++;
					line_increase(ref index);
					return temp;
				}
				if (input[index] == '\"')
				{
					if (input[index - 1] == '\\')
					{
						//included
						temp += input[index];
						temp += input[index + 1];
						index += 2;
					}
					else
					{
						temp += input[index];
						index++;
						return temp;
					}
				}
				else
				{
					temp += input[index];
					index++;
				}
			}

			return temp;
		}








		// operator handler
		static string operator_separator(ref string input, ref int index)
		{
			string temp = "";
			temp += input[index];
			// if Character is * then 1 possibilities
			if (input[index] == '*')
			{
				string word = input[index].ToString();
				index++;
				return word;
			}

			// if Character is + then 3 possibilities
			else if (input[index] == '+')
			{
				
				if (input[index + 1] == '=')
				{
					temp += input[index + 1];
					index += 2;
					return temp;
				}
				else if (input[index + 1] == '+' && !Char.IsDigit(input[index + 2]))
				{
					temp += input[index + 1];
					index += 2;
					return temp;
				}
				else if (input[index] == '+' && input[index+1] != '+')
				{
					string word = input[index].ToString();
					index++;
					return word;
				}
				else if (!Char.IsLetterOrDigit(input[index - 1]) && Char.IsDigit(input[index + 1]))
				{
					index++;
					do
					{
						temp += input[index];
						index++;

					} while (Char.IsDigit(input[index]));
					return temp;
				}
				



			}
			// if Character is - then 3 possibilities
			else if (input[index] == '-')
			{
				if (input[index + 1] == '-' && !Char.IsDigit(input[index + 2]))
				{
					temp += input[index + 1];
					index += 2;
					return temp;
				}
				else if (input[index + 1] == '=')
				{
					temp += input[index + 1];
					index += 2;
					return temp;
				}
				else if (!Char.IsLetterOrDigit(input[index - 1]) && Char.IsDigit(input[index + 1]))
				{
					index++;
					do
					{
						temp += input[index];
						index++;

					} while (Char.IsDigit(input[index]));
					return temp;
				}
				else
				{
					string word = input[index].ToString();
					index++;
					return word;
				}
			}
			// If Character is <,>,!,= then 4 possibilities
			else if (input[index] == '<' || input[index] == '>' || input[index] == '!' || input[index] == '=')
			{
				if (input[index + 1] == '=')
				{
					temp += input[index + 1];
					index += 2;
					return temp;
				}
				else
				{
					string word = input[index].ToString();
					index++;
					return word;
				}
			}
			// If Character is &, | then 2 possibilities
			else if (input[index] == '&' || input[index] == '|')
			{
				if (input[index + 1] == '&')
				{
					temp += input[index + 1];
					index += 2;
					return temp;
				}
				else if (input[index + 1] == '|')
				{
					temp += input[index + 1];
					index += 2;
					return temp;
				}
				else
				{
					string word = input[index].ToString();
					index++;
					return temp;
				}
			}
			return temp;
		}









		// Word Breaker
		static void breakWord(ref string input)
		{
			// Dictionary for word and line number
			var wordList = new Dictionary<int, Tuple<int, string>>();


			// Dictionary Key
			int id = 1;


			// for indexing through each character
			int i = 0;

			// Empty String for concatenation of characters
			string temp = "";

			while (i < input.Length)
			{


				// if Char is operator
				if (operators.ContainsKey(input[i].ToString()) || input[i] == '&' || input[i] == '|')
				{

					// if character is / then 3 possibilities 
					// Single line comment //
					// multi line comment /*
					// divide /
					if (input[i] == '/')
					{
						if (input[i + 1] == '/')
						{
							i += 2;
							singleLineComment(ref input, ref i);
						}
						else if (input[i + 1] == '*')
						{
							i += 2;
							multiLineComment(ref input, ref i);
						}
						else
						{
							string word = input[i].ToString();
							i++;
							wordList.Add(id, Tuple.Create(lineNumber, word));
						}
						temp = "";
					}

					// if Char is operator other than /
					// it will call operator separator 
					else if (input[i] != '/')
					{
						string text = operator_separator(ref input, ref i);
						wordList.Add(id, Tuple.Create(lineNumber, text));
					}
				}




				// if Character is punctuator it will check through dictionary and add word to wordlist
				else if (punctuators.ContainsKey(input[i]))
				{
					string word = input[i].ToString();
					wordList.Add(id, Tuple.Create(lineNumber, word));
					i++;
				}






				// if Character is Char or String 
				// it will call double_quote function  
				else if (input[i] == '\"')
				{
					string text = double_quote(ref input, ref i);
					wordList.Add(id, Tuple.Create(lineNumber, text));
				}






				//  if Character is letter or digit
				else if (Char.IsLetterOrDigit(input[i]))
				{
					temp += input[i];

					
					if (!Char.IsLetterOrDigit(input[i + 1]) && input[i + 1] != '.')
					{
						wordList.Add(id, Tuple.Create(lineNumber, temp));
						temp = "";
					}

					i++;

				}



				// Line break Checker
				else if (input[i] == '\r')
				{
					if (input[i + 1] == '\n')
					{
						i++;
						line_increase(ref i);
					}
					else
					{
						i++;
					}
				}





				// if Character is Space
				else if (input[i] == ' ')
				{
					temp = "";
					i++;
				}



				// else
				else
				{
					Console.Write("error");
				}


				// dictionary id increment
				id++;

				// loop end
				if (input.Length == 0) break;
			}


			// looping through wordlist dictionary
			foreach (KeyValuePair<int, Tuple<int, string>> data in wordList)
				Console.WriteLine(data.Value);
		}







		static void Main(string[] args)
		{
			if (File.Exists(textFile))
			{

				string fileData = File.ReadAllText(textFile);
				fileData +=" ";
				breakWord(ref fileData);
			}
            else
            {
				Console.Write("File not exist");
            }

		}
	}
}