using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch9Case1
{
    class Program
    {
        static void Main(string[] args)
        {
            // basic variables

            int entryFee = 25;
            int minContestant = 0;
            int maxContestant = 30;

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

            Contestant[] contestants = new Contestant[contestantsThisYear];
            int[] talentCounts = InitTalentCountArr(Contestant.talentCodes.Capacity);

            PopulateContestantData(contestants, talentCounts);

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
                SearchContestantData(contestants);
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
         * @param Contestant[] contestants, the array to fill with data
         * @param int[] talentCounts an array for keeping track of counts for each type
         */
        private static void PopulateContestantData(Contestant[] contestants, int[] talentCounts)
        {
            string input, name;
            char code;

            int numContestants = contestants.Length;

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

                Console.WriteLine(Contestant.GetValidTalentString());
                Console.Write(name + "'s talent code: ");
                input = Console.ReadLine();

                while (!char.TryParse(input, out code) || !Contestant.IsValidCode(code))
                {
                    Console.WriteLine("\nInvalid input entered.\n");
                    Console.WriteLine(Contestant.GetValidTalentString());
                    Console.Write(name + "'s talent code: ");
                    input = Console.ReadLine();
                }

                contestants[contestantNum] = new Contestant(name, code);
                talentCounts[Contestant.talentCodes.IndexOf(code)]++;

                contestantNum++;
            }

            Console.WriteLine("\n\n*********************************************************\n");

            Console.WriteLine("Counts for each type of talent: ");
            foreach (string talent in Contestant.talentDescs)
            {
                Console.WriteLine(talent + ": " + talentCounts[Contestant.talentDescs.IndexOf(talent)]);
            }

            Console.WriteLine("\n*********************************************************\n");
        }


        /**
         * Search through contestant data for a talent code and display names associated with that code
         * @param Contestant[] contestants, the array to fill with data
         */
        private static void SearchContestantData(Contestant[] contestants)
        {
            string input, quitSeq = "q";
            char choice;

            int numContestants = contestants.GetLength(0);

            Console.WriteLine(Contestant.GetValidTalentString());
            Console.Write("Please enter a talent code or '" + quitSeq + "' to end: ");

            input = Console.ReadLine();

            // the user didn't immediately quit
            while (input != quitSeq)
            {
                // the user didn't enter a valid code and the code was also not the quit sequence
                while ((!char.TryParse(input, out choice) || !Contestant.IsValidCode(choice)) && input != quitSeq)
                {
                    Console.WriteLine("\nInvalid input entered.\n");
                    Console.WriteLine(Contestant.GetValidTalentString());
                    Console.Write("Please enter a talent code or '" + quitSeq + "' to end: ");
                    input = Console.ReadLine();
                }

                if (input == quitSeq) break;

                // handle we got valid input here
                Console.WriteLine("\nContestants signed up for " + Contestant.GetDescForCode(choice) + ": ");
                Console.WriteLine("--------------------------------------------------------");

                for (int i = 0; i < contestants.Length; i++)
                {
                    if (contestants[i].TalentCode == choice)
                        Console.WriteLine(contestants[i].Name);
                }

                // want to search again?
                Console.WriteLine("\n\nSearch for another talent code?");
                Console.WriteLine(Contestant.GetValidTalentString());
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
