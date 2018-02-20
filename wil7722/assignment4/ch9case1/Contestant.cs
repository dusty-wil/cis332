using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch9Case1
{
    class Contestant
    {
        // static talentCode and talentDesc values
        public static ArrayList talentCodes = new ArrayList { 'S', 'D', 'M', 'O' };
        public static ArrayList talentDescs = new ArrayList { "Singing", "Dancing", "Musical Instrument", "Other" };


        /**
         * Name property
         * Holds contestant's name
         */
        public string Name { get; set; }


        /**
         * TalentDesc property
         * Holds talent description, or "Invalid" if the talentCode does not match those defined in the class.
         * Read-only
         */
        public string TalentDesc { get; private set; }


        /**
         * TalentCode property
         * Holds the talent code, or 'I' if the code does not match those defined in the class.
         * The setter for this property also sets the talentDesc
         */
        private char talentCode;
        public char TalentCode
        {
            get
            {
                return talentCode;
            }

            set
            {
                if (Contestant.IsValidCode(value))
                {
                    talentCode = value;
                    TalentDesc = Contestant.GetDescForCode(value);
                }
                else
                {
                    talentCode = 'I';
                    TalentDesc = "Invalid";
                }
            }
        }


        /**
         * overloaded constructor
         * initializes Name with a blank string and TalentCode as invalid
         */ 
        public Contestant()
        {
            Name = "";
            TalentCode = 'I';
        }


        /**
         * overloaded constructor
         * @param string name, the contestant's name
         * @param string code, the talent code for the contestant
         */
        public Contestant(string name, char code)
        {
            Name = name;
            TalentCode = code;
        }


        /**
         * GetDescForCode
         * Gets the talent description that corresponds to a given code
         * @param char code, the code to search for
         * @return string
         */
        public static string GetDescForCode(char code)
        {
            if (!Contestant.IsValidCode(code)) return "Invalid";
            return (string)Contestant.talentDescs[Contestant.talentCodes.IndexOf(code)];
        }
        

        /**
         * GetValidTalentString
         * Creates a string that can be used to display valid talent options
         * @return string
         */
        public static string GetValidTalentString()
        {
            String validTalentOptions = "Valid talent code options: ";
            foreach (char code in Contestant.talentCodes)
            {
                validTalentOptions += "[" + code + "]" + Contestant.GetDescForCode(code) + "   ";
            }
            return validTalentOptions;
        }


        /**
         * IsValidCode
         * Checks to see if the passed in code exists as an available talent
         * @param char code, the code to check
         * @return boolean
         */
        public static Boolean IsValidCode(char code)
        {
            return Contestant.talentCodes.Contains(code);
        }
    }
}
