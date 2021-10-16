using System;
using System.Collections.Generic;


namespace Laxical_Analyzer
{
    class LaxicalAnalyxer
    {
        static int lineNumber;
        IDictionary<string, string> operators;
        IDictionary<char, string> punctuators;

        public LaxicalAnalyxer(int line, IDictionary<string, string> opr, IDictionary<char, string> punc) {
            lineNumber = line;
            operators = opr;
            punctuators = punc;
        }

        private static bool line_increase(ref int index){
            // Line Increment Function 
            lineNumber++;
            index++;
            return true;
        }


        private static  bool IsAllDigits(string s) {
            // Checking if string is all digits
            foreach (char c in s) {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }

        private static bool singleLineComment(ref string input, ref int index) {
            // Single Line Comment
            while (index < input.Length) {
                if (input[index] == '\r') {
                    index++;
                    line_increase(ref index);
                    return true;
                }
                index++;
            }
            return true;
        }

        static bool multiLineComment(ref string input, ref int index) {
            // Multiline Comment Function
            while (index < input.Length){
                if (input[index] == '\r'){
                    index++;
                    line_increase(ref index);
                }

                if (input[index] == '*'){
                    if (input[index + 1] == '/'){
                        index += 2;
                        return true;
                    }
                }
                index++;
            }
            return true;
        }

        private static string double_quote(ref string input, ref int index) {
            // String Handler Function
            string temp = "";
            temp += input[index];
            index++;
            while (index < input.Length){
                if (input[index] == '\r'){
                    index++;
                    line_increase(ref index);
                    return temp;
                }
                if (input[index] == '\"') {
                    if (input[index - 1] == '\\'){
                        //included
                        temp += input[index];
                        temp += input[index + 1];
                        index += 2;
                    }
                    else {
                        temp += input[index];
                        index++;
                        return temp;
                    }
                }
                else{
                    temp += input[index];
                    index++;
                }
            }
            return temp;
        }

        private static string operator_separator(ref string input, ref int index){
            // operator handler
            string temp = "";
            temp += input[index];
            // if Character is * then 1 possibilities
            if (input[index] == '*'){
                string word = input[index].ToString();
                index++;
                return word;
            }

            // if Character is + then 3 possibilities
            else if (input[index] == '+') {
                if ((input[index + 1] == '+') && !Char.IsDigit(input[index + 2])) {
                    temp += input[index + 1];
                    index += 2;
                    return temp;
                }
                else if (input[index + 1] == '=') {
                    temp += input[index + 1];
                    index += 2;
                    return temp;
                }
                else if (Char.IsDigit(input[index + 1])) {
                    if ((index - 1 >= 0 && index - 1 < input.Length)) {
                        if (!Char.IsLetterOrDigit(input[index - 1])){
                            index++;
                            do {
                                temp += input[index];
                                index++;

                            } while (Char.IsDigit(input[index]));
                            return temp;
                        }
                        else {
                            string word = input[index].ToString();
                            index++;
                            return word;
                        }
                    }
                    else {
                        index++;
                        do {
                            temp += input[index];
                            index++;

                        } while (Char.IsDigit(input[index]));
                        return temp;
                    }

                }
                else {
                    string word = input[index].ToString();
                    index++;
                    return word;
                }
            }

            // if Character is - then 3 possibilities
            else if (input[index] == '-') {
                if ((input[index + 1] == '-') && !Char.IsDigit(input[index + 2])) {
                    temp += input[index + 1];
                    index += 2;
                    return temp;
                }
                else if (input[index + 1] == '=') {
                    temp += input[index + 1];
                    index += 2;
                    return temp;
                }
                else if (Char.IsDigit(input[index + 1])) {
                    if ((index - 1 >= 0 && index - 1 < input.Length)) {
                        if (!Char.IsLetterOrDigit(input[index - 1])) {
                            index++;
                            do {
                                temp += input[index];
                                index++;

                            } while (Char.IsDigit(input[index]));
                            return temp;
                        }
                        else {
                            string word = input[index].ToString();
                            index++;
                            return word;
                        }
                    }
                    else {
                        index++;
                        do {
                            temp += input[index];
                            index++;

                        } while (Char.IsDigit(input[index]));
                        return temp;
                    }

                }
                else{
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

            // If Character is & then 2 possibilities
            else if (input[index] == '&') {
                if (input[index + 1] == '&') {
                    temp += input[index + 1];
                    index += 2;
                    return temp;
                }
                else {
                    string word = input[index].ToString();
                    index++;
                    return word;
                }
            }


            // If Character  | then 2 possibilities
            else if (input[index] == '|')
            {
              
              if (input[index + 1] == '|')
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
            return temp;


        }

        // Word Breaker
        public Dictionary<int, Tuple<int, string>> BreakWord(ref string input) {
            // Dictionary for word and line number
            var wordList = new Dictionary<int, Tuple<int, string>>();

            // Dictionary Key
            int id = 1;

            // for indexing through each character
            int i = 0;

            // Empty String for concatenation of characters
            string temp = "";

            while (i < input.Length) {
                // if Char is operator
                if (operators.ContainsKey(input[i].ToString()) || input[i] == '&' || input[i] == '|') {
                    // if character is / then 3 possibilities 
                    // Single line comment //
                    // multi line comment /*
                    // divide /
                    if (input[i] == '/') {
                        if (input[i + 1] == '/') {
                            i += 2;
                            singleLineComment(ref input, ref i);
                        }
                        else if (input[i + 1] == '*') {
                            i += 2;
                            multiLineComment(ref input, ref i);
                        }
                        else {
                            string word = input[i].ToString();
                            i++;
                            wordList.Add(id, Tuple.Create(lineNumber, word));
                        }
                        temp = "";
                    }

                    // if Char is operator other than /
                    // it will call operator separator 
                    else if (input[i] != '/') {
                        string text = operator_separator(ref input, ref i);
                        wordList.Add(id, Tuple.Create(lineNumber, text));
                    }
                }






                // if Character is Char or String 
                // it will call double_quote function  
                else if (input[i] == '\"') {
                    string text = double_quote(ref input, ref i);
                    wordList.Add(id, Tuple.Create(lineNumber, text));
                }







                //  if Character is letter or digit
                else if (Char.IsLetterOrDigit(input[i])) {
                    temp += input[i];
                    if (!Char.IsLetterOrDigit(input[i + 1]) && input[i + 1] != '.')
                    {
                        wordList.Add(id, Tuple.Create(lineNumber, temp));
                        temp = "";
                        i++;
                    }
                    else if (input[i + 1] == '.')
                    {
                        if (IsAllDigits(temp) && Char.IsDigit(input[i + 2]))
                        {
                            Console.Write("Yes");
                            i++;
                            temp += input[i];
                            i++;
                            do
                            {
                                temp += input[i];
                                i++;

                            } while (Char.IsDigit(input[i]));
                            wordList.Add(id, Tuple.Create(lineNumber, temp));
                            temp = "";
                        }
                        else if(!IsAllDigits(temp))
                        {
                    
                            wordList.Add(id, Tuple.Create(lineNumber, temp));
                            temp = "";
                        }
                        i++;
                    }
                    else
                    {
                        i++;
                    }
                 

                }






                // if Character is punctuator it will check through dictionary and add word to wordlist and handle .(integer) part too
                else if (punctuators.ContainsKey(input[i])) {

                    if(input[i]== '.')
                    {
                        temp += input[i];
                        if (Char.IsDigit(input[i + 1]))
                        {
                            i++;
                            temp += input[i];
                            i++;
                        }
                        else
                        {
                         
                            wordList.Add(id, Tuple.Create(lineNumber,temp));
                            i++;
                        }
                     
                    }
                    else if(input[i] != '.')
                    {
                        string word = input[i].ToString();
                        wordList.Add(id, Tuple.Create(lineNumber, word));
                        i++;
                    }
                  
                }

                // Line break Checker
                else if (input[i] == '\r') {
                    if (input[i + 1] == '\n') {
                        i++;
                        line_increase(ref i);
                    }
                    else {
                        i++;
                    }
                }

                // if Character is Space
                else if (input[i] == ' ') {
                    temp = "";
                    i++;
                }

                // else
                else {
                    Console.Write("error");
                }

                // dictionary id increment
                id++;

                // loop end
                if (input.Length == 0)
                    break;
            }

            return wordList;
        }
    }
}

