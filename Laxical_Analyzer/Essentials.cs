using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;



namespace Laxical_Analyzer
{
    class Tools
    {
        public string readFile(string path) {
            // Open the file to read from.
            return File.ReadAllText(path);
        }
    }

    public class Essentials
    {
        // Punctuators
        // dictionary for punctuators and its classpart
        public IDictionary<char, string> punctuators = new Dictionary<char, string>() {
            {'(', "("}, {')',")"}, {'{', "{"}, {'}', "}"}, {'[', "["}, {']', "]"}, {';',";"}, {'.', "."}, {',', ","},
            {'@', "lexical_error"}, {'$', "lexical_error"}, {'^', "lexical_error"}, {':', "lexical_error"}, {'?',"lexical_error"},
            {'`', "lexical_error"}, {'~', "lexical_error"}, {'\'', "lexical_error"}, {'_', "lexical_error"}, {'\\', "lexical_error"},
        };

        // Operators
        // dictionary for operators and its classpart
        public IDictionary<string, string> operators = new Dictionary<string, string>() {
            {"+", "PM"}, {"-", "PM"},
            { "*", "MD"}, {"/", "MD"},
            { "<", "RO"}, {">", "RO"}, {"<=", "RO"}, {">=", "RO"}, {"!=", "RO"}, {"==", "RO"},
            {"=", "AO"}, {"+=", "AO"}, {"-=", "AO"},
            {"&", "Lexeme error"}, {"|", "Lexeme error"},
            {"!", "LO"}, {"||", "LO"}, {"&&", "LO"},
        };

        //
        static IDictionary<string, string> keyword_list = new Dictionary<string, string>(){
                {"int","dt"},{"float","dt"},{"bool","dt"},{"string","dt"},
               {"public","access-modifier"},{"static","static"},
                {"class","class"},
                {"while","while"},
                {"if","if"},
                {"else","else"},
                {"for","for"},
                {"break","break"},
                {"true","bool-const"},
                {"false","bool-const"},
                {"continous","continous"},
                 {"struct","struct" }



            };

        static Regex RE_integer = new Regex(@"^\d*$");
        static Regex RE_alphabtes = new Regex(@"^[A-Za-z]$");
        static Regex RE_number = new Regex(@"^[0-9]$");
        static Regex RE_float = new Regex(@"^(\d*.\d+|\d*[^.])$");
        static Regex RE_punctuators = new Regex(@"^,|.|;|[|]|(|)|{|}|:$");
        static Regex RE_all_punctuators = new Regex(@"^[\x20-\x2F]|[\x3A-\x40]|[\x5B-\x5E]|[\x7B-\x7E]|`$");
        static Regex RE_identifier = new Regex(@"^([a-zA-Z]+_[0-9])$");
    }
}
