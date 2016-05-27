using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleSVN
{
    public class Constants
    {
        public const char sectionNameStartIndicator = '[';
        public const char sectionNameEndIndicator = ']';
        public const char commentIndicator = ';';
        public const char configurationDelimiter = '=';
        public const int substringCount = 2;
        public const int startingLineNumber = 1;

        public const string SVNCheckoutCommand = "checkout {0} {1} --username {2} --password {3}";
        public const string SVNUpdateCommand = "update {0}";
        public const string SVNCommitCommand = "commit {0} -m \"{1}\"";
        public const string SVNAddCommand = "add {0}";
        public const string SVNCleanupCommand = "cleanup {0}";
        public const string SVNRevisionCommand = "info -r head {0} | find \"Revision\"";
        public const string SVNCommitedRevisionStartString = "Committed revision";
        public static string[] SVNlineDelimiter = new string[] { Environment.NewLine };

        public const string TEXT = "text";
        public const string DATE = "date";
        public const string HEXA = "hexadecimal";
        public const string NUMERIC = "numeric";
        public const string NONE = "none";
        public const string DELIMITED = "delimited";

        public const string CommitedRevisionStartString = "Committed revision";
        public const string NonValidatedSectionString = "NON_VALIDATED";
        public const int minHexaToDecimal = 0;
        public const int maxHexaToDecimal = 65535;

        public static char[] sectionNameDelimiters = new char[] { sectionNameStartIndicator, sectionNameEndIndicator };
        public static char[] configurationDelimiters = new char[] { configurationDelimiter };
        public static string[] configurationTypes = new string[] { "B", "I", "E" };

        // The files seem to be coming from a Unix system (new line indicator is \n) 
        public static string[] lineDelimiter = new string[] { Environment.NewLine, "\n" };

        public static string[] productSectionSeparator = new string[] { "_" };
        public static string[] commentIndicators = new string[] { "#", ";" };

        public static string GetDelimitedFormat(string Delimiter)
        {
            switch (Delimiter.ToLower())
            {
                case "comma": return ",";
                case "space": return " ";
                case "tab": return "\t";
                default: return Delimiter;
            }
        }
    }
}
