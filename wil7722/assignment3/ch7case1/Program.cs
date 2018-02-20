using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch7Case1
{
    class Program
    {
        static void Main(string[] args)
        {
            // basic variables

            int entryFee = 25;
            int minContestant = 0;
            int maxContestant = 30;

            // arrays for storing talent information
            // i chose to leave these out here and pass them in to functions because i can
            // conceivably see them being set in the main function or changed later.

            ArrayList talentCodes = new ArrayList { "S", "D", "M", "O" };
            ArrayList talentDescs = new ArrayList { "Singing", "Dancing", "Musical Instrument", "Other" };

            int contestantsLastYear, contestantsThisYear;
            string input;

            Console.WriteLine(
                "\n*********************************************************" +
                "\n* Greenville County Fair: The stars shine in Greenville *" +
                "\n*********************************************************\n"
            );

            // get contestants from last year

            Console.WriteLine(
                "Contestants who competed last year\n"
                + "------------------------------------------------------------"
            );
            contestantsLastYear = GetNumContestants(minContestant, maxContestant);
            
            // get contestants from this year

            Console.WriteLine(
                "\nContestants competing this year\n"
                + "------------------------------------------------------------"
            );
            contestantsThisYear = GetNumContestants(minContestant, maxContestant);

            // get names and codes

            Console.WriteLine(
                "\nPlease enter the names of each of the "
                + contestantsThisYear
                + " contestants and their talent code.\n"
                + "------------------------------------------------------------"
            );

            String[,] namesAndTalents = new String[contestantsThisYear, 2];
            int[] talentCounts = { 0, 0, 0, 0 };
            PopulateContestantData(ref namesAndTalents, ref talentCounts, talentCodes, talentDescs);            

            // searching the talent array

            Console.Write("Would you like to list contestants by talent? (y/n): ");
            input = Console.ReadLine();

            while (input != "y" && input != "n")
            {
                Console.Write("Would you like to list contestants by talent? (y/n): ");
                input = Console.ReadLine();
            }

            if (input == "y")
            {
                SearchContestantData(namesAndTalents, talentCodes, talentDescs);
            }

            // print the turnout information

            EvaluateTurnout(contestantsThisYear, contestantsLastYear, entryFee);

        } // end of main method


        /**
         * asks user to enter a number that falls within a range. User is re-prompted if input is invalid
         * @param int minContestants the start of the range
         * @param int maxContestants the end of the range
         */
        private static int GetNumContestants(int minContestants, int maxContestants)
        {
            string input;
            int numContestants = 0;

            Console.Write(
                    "Please enter a number of contestants betweeen "
                    + minContestants
                    + " and "
                    + maxContestants
                    + ": "
            );

            input = Console.ReadLine();
            while (
                !int.TryParse(input, out numContestants)
                || numContestants < minContestants
                || numContestants > maxContestants
            )
            {
                Console.WriteLine("\nInvalid input entered.\n");
                Console.Write(
                    "Please enter a number of contestants betweeen " 
                    + minContestants 
                    + " and " 
                    + maxContestants 
                    + ": "
                );
                input = Console.ReadLine();
            }

            return numContestants;
        }


        /**
         * evaluates the expected turnout financially and discriptively
         * @param int contestantsThisYear number of contestants this year
         * @param int contestantsLastYear number of contestants last year
         * @param int entryFee price for entering the contest
         */
        private static void EvaluateTurnout(int contestantsThisYear, int contestantsLastYear, int entryFee)
        {
            Console.WriteLine("\n\n*********************************************************\n");

            Console.WriteLine("Contestants entered last year: " + contestantsLastYear);
            Console.WriteLine("Contestants entered this year: " + contestantsThisYear);

            Console.WriteLine("\nLast year's revenue: " + (contestantsLastYear * entryFee).ToString("C"));
            Console.WriteLine("This year's revenue: " + (contestantsThisYear * entryFee).ToString("C"));
            Console.WriteLine("Revenue difference:  " + ((contestantsThisYear * entryFee) - (contestantsLastYear * entryFee)).ToString("C"));

            if (contestantsThisYear > contestantsLastYear)
            {
                if (contestantsThisYear > (contestantsLastYear * 2))
                {
                    Console.WriteLine("\nThe competition is more than twice as big this year!");
                }
                else
                {
                    Console.WriteLine("\nThe competition is bigger than ever!");
                }
            }
            else if (contestantsLastYear > contestantsThisYear)
            {
                Console.WriteLine("\nA tighter race this year! Come out and cast your vote!");
            }
            else
            {
                Console.WriteLine("\nAnother tight race this year! Come out and cast your vote!");
            }
        }


        /**
         * populate an array with contestant data entered by user
         * @param ref string[,] nameAndTalents the array to fill with data
         * @param int[] talentCounts an array for keeping track of counts for each type
         * @param arraylist talentCodes defined talent codes
         * @param talentDescs corresponding talent descriptions
         */
        private static void PopulateContestantData(
            ref string[,] namesAndTalents,
            ref int[] talentCounts,
            ArrayList talentCodes,
            ArrayList talentDescs
        )
        {
            string input;

            int numContestants = namesAndTalents.GetLength(0);

            String validTalentOptions = "Valid talent code options: ";
            foreach (String code in talentCodes)
            {
                validTalentOptions += "[" + code + "]" + talentDescs[talentCodes.IndexOf(code)] + "   ";
            }

            int contestantNum = 0;
            while (contestantNum < numContestants)
            {
                Console.Write("\nContestant " + (contestantNum + 1) + " name: ");
                input = Console.ReadLine();
                while (input == "")
                {
                    Console.WriteLine("\nInvalid input entered.\n");
                    Console.Write("Contestant " + (contestantNum + 1) + " name: ");
                    input = Console.ReadLine();
                }

                namesAndTalents[contestantNum, 0] = input;

                Console.WriteLine(validTalentOptions);
                Console.Write(namesAndTalents[contestantNum, 0] + "'s talent code: ");
                input = Console.ReadLine();

                while (!talentCodes.Contains(input))
                {
                    Console.WriteLine("\nInvalid input entered.\n");
                    Console.WriteLine(validTalentOptions);
                    Console.Write(namesAndTalents[contestantNum, 0] + "'s talent code: ");
                    input = Console.ReadLine();
                }

                namesAndTalents[contestantNum, 1] = input;
                talentCounts[talentCodes.IndexOf(input)]++;

                contestantNum++;
            }

            Console.WriteLine("\n\n*********************************************************\n");

            Console.WriteLine("Counts for each type of talent: ");
            foreach (string talent in talentDescs)
            {
                Console.WriteLine(talent + ": " + talentCounts[talentDescs.IndexOf(talent)]);
            }

            Console.WriteLine("\n*********************************************************\n");
        }


        /**
         * Search through contestant data for a talent code and display names associated with that code
         * @param string [,] namesAndTalents the data array to search through
         * @param arraylist talentCodes defined talent codes
         * @param talentDescs corresponding talent descriptions
         */
        private static void SearchContestantData(
            string [,] namesAndTalents,
            ArrayList talentCodes,
            ArrayList talentDescs
        )
        {
            string input;
            string quitSeq = "q";

            int numContestants = namesAndTalents.GetLength(0);

            String validTalentOptions = "Valid talent code options: ";
            foreach (String code in talentCodes)
            {
                validTalentOptions += "[" + code + "]" + talentDescs[talentCodes.IndexOf(code)] + "   ";
            }

            Console.WriteLine(validTalentOptions);
            Console.Write("Please enter a talent code or '" + quitSeq + "' to end: ");

            input = Console.ReadLine();

            // the user didn't immediately quit
            while (input != quitSeq)
            {
                // the user didn't enter a valid code and the code was also not the quit sequence
                while (!talentCodes.Contains(input) && input != quitSeq)
                {
                    Console.WriteLine("\nInvalid input entered.\n");
                    Console.WriteLine(validTalentOptions);
                    Console.Write("Please enter a talent code or '" + quitSeq + "' to end: ");
                    input = Console.ReadLine();
                }
                if (input == quitSeq) break;

                // handle we got valid input here
                Console.WriteLine("\nContestants signed up for " + talentDescs[talentCodes.IndexOf(input)] + ": ");
                Console.WriteLine("--------------------------------------------------------");

                for (int i = 0; i < namesAndTalents.GetLength(0); i++)
                {
                    if (namesAndTalents[i, 1] == input)
                        Console.WriteLine(namesAndTalents[i, 0]);
                }

                // want to search again?
                Console.WriteLine("\n\nSearch for another talent code?");
                Console.WriteLine(validTalentOptions);
                Console.Write("Please enter a talent code or '" + quitSeq + "' to end: ");
                input = Console.ReadLine();
            }
        }
    }
}
