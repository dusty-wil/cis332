using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch8Case1
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

            ArrayList talentCodes = new ArrayList { 'S', 'D', 'M', 'O' };
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

            String[] names = new String[contestantsThisYear];
            char[] talents = new char[contestantsThisYear];
            int[] talentCounts = InitTalentCountArr(talentCodes.Capacity);

            PopulateContestantData(names, talents, talentCounts, talentCodes, talentDescs);

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
                SearchContestantData(names, talents, talentCodes, talentDescs);
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
         * @param string[] names the array to fill with contestant names
         * @param char[] talents the array to fill with contestant talent codes
         * @param int[] talentCounts an array for keeping track of counts for each type
         * @param arraylist talentCodes defined talent codes
         * @param talentDescs corresponding talent descriptions
         */
        private static void PopulateContestantData(
            string[] names,
            char[] talents,
            int[] talentCounts,
            ArrayList talentCodes,
            ArrayList talentDescs
        )
        {
            string input, name;
            char code;

            int numContestants = talents.Length;

            String validTalentOptions = "Valid talent code options: ";
            foreach (char talentCode in talentCodes)
            {
                validTalentOptions += "[" + talentCode + "]" + talentDescs[talentCodes.IndexOf(talentCode)] + "   ";
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
                name = input;

                names[contestantNum] = name;

                Console.WriteLine(validTalentOptions);
                Console.Write(name + "'s talent code: ");
                input = Console.ReadLine();

                while (!char.TryParse(input, out code) || !talentCodes.Contains(code))
                {
                    Console.WriteLine("\nInvalid input entered.\n");
                    Console.WriteLine(validTalentOptions);
                    Console.Write(name + "'s talent code: ");
                    input = Console.ReadLine();
                }

                talents[contestantNum] = code;

                talentCounts[talentCodes.IndexOf(code)]++;

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
         * @param string [] names the data array to search through
         * @param char [] talents the data array to search through
         * @param arraylist talentCodes defined talent codes
         * @param talentDescs corresponding talent descriptions
         */
        private static void SearchContestantData(
            string[] names,
            char[] talents,
            ArrayList talentCodes,
            ArrayList talentDescs
        )
        {
            string input, quitSeq = "q";
            char choice;

            int numContestants = talents.Length;

            String validTalentOptions = "Valid talent code options: ";
            foreach (char talentCode in talentCodes)
            {
                validTalentOptions += "[" + talentCode + "]" + talentDescs[talentCodes.IndexOf(talentCode)] + "   ";
            }

            Console.WriteLine(validTalentOptions);
            Console.Write("Please enter a talent code or '" + quitSeq + "' to end: ");

            input = Console.ReadLine();

            // the user didn't immediately quit
            while (input != quitSeq)
            {
                // the user didn't enter a valid code and the code was also not the quit sequence
                while ((!char.TryParse(input, out choice) || !talentCodes.Contains(choice)) && input != quitSeq)
                {
                    Console.WriteLine("\nInvalid input entered.\n");
                    Console.WriteLine(validTalentOptions);
                    Console.Write("Please enter a talent code or '" + quitSeq + "' to end: ");
                    input = Console.ReadLine();
                }
                if (input == quitSeq) break;

                // handle we got valid input here
                Console.WriteLine("\nContestants signed up for " + talentDescs[talentCodes.IndexOf(choice)] + ": ");
                Console.WriteLine("--------------------------------------------------------");

                for (int i = 0; i < talents.Length; i++)
                {
                    if (talents[i] == choice)
                        Console.WriteLine(names[i]);
                }

                // want to search again?
                Console.WriteLine("\n\nSearch for another talent code?");
                Console.WriteLine(validTalentOptions);
                Console.Write("Please enter a talent code or '" + quitSeq + "' to end: ");
                input = Console.ReadLine();
            }
        }


        /**
         * Creates and initializes an array for holding counts of talents
         * @param int numberOfTalents, the number of available talents
         * @return int[]
         */
        private static int[] InitTalentCountArr(int numberOfTalents)
        {
            int[] talentCountArr = new int[numberOfTalents];
            for (int i = 0; i < numberOfTalents; i++)
            {
                talentCountArr[i] = 0;
            }

            return talentCountArr;
        }
    }
}
