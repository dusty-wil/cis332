using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch6Case1
{
    class Program
    {
        static void Main(string[] args)
        {
            const int ENTRY_FEE = 25;
            const int MIN_CONTESTANT = 0;
            const int MAX_CONTESTANT = 30;
            const String QUIT_SEQ = "q";

            String lastYearPrompt = "Please enter a number between " 
                + MIN_CONTESTANT 
                + " and " 
                + MAX_CONTESTANT 
                + " for the contestants from last year: "
            ;

            String thisYearPrompt = "Please enter a number between "
                + MIN_CONTESTANT
                + " and "
                + MAX_CONTESTANT
                + " for the contestants for this year: "
            ;

            ArrayList talentCodes  = new ArrayList { "S", "D", "M", "O" };
            ArrayList talentDescs  = new ArrayList { "Singing", "Dancing", "Musical Instrument", "Other" };
            int[] talentCounts = { 0, 0, 0, 0 };

            String validTalentOptions = "Valid talent code options: "; 

            foreach (String code in talentCodes)
            {
                validTalentOptions += "[" + code + "]" + talentDescs[talentCodes.IndexOf(code)] + "   ";
            }

            int contestantsLastYear, contestantsThisYear;
            string input;

            Console.WriteLine(
                "\n*********************************************************" +
                "\n* Greenville County Fair: The stars shine in Greenville *" +
                "\n*********************************************************\n"
            );

            // get contestants from last year

            Console.Write(lastYearPrompt);

            input = Console.ReadLine();
            while (
                !int.TryParse(input, out contestantsLastYear) 
                || contestantsLastYear < MIN_CONTESTANT 
                || contestantsLastYear > MAX_CONTESTANT
            )
            {
                Console.WriteLine("Invalid input entered.");
                Console.Write(lastYearPrompt);
                input = Console.ReadLine();
            }
            
            // get contestants from this year

            Console.Write(thisYearPrompt);
            input = Console.ReadLine();

            while (
                !int.TryParse(input, out contestantsThisYear) 
                || contestantsThisYear < MIN_CONTESTANT 
                || contestantsThisYear > MAX_CONTESTANT
            )
            {
                Console.WriteLine("Invalid input entered.");
                Console.Write(thisYearPrompt);
                input = Console.ReadLine();
            }

            Console.WriteLine(
                "Please enter the names of each of the " 
                + contestantsThisYear 
                + " contestants and their talent code."
            );

            // [n,0] = name; [n,1] = talent code

            String[,] namesAndTalents = new String[contestantsThisYear, 2];

            int contestantNum = 0;
            while (contestantNum < contestantsThisYear)
            {
                Console.Write("Contestant " + (contestantNum + 1) + " name: ");
                input = Console.ReadLine();
                while (input == "")
                {
                    Console.WriteLine("Invalid input entered.");
                    Console.Write("Contestant " + (contestantNum + 1) + " name: ");
                    input = Console.ReadLine();
                }

                namesAndTalents[contestantNum, 0] = input;

                Console.WriteLine(validTalentOptions);
                Console.Write(namesAndTalents[contestantNum, 0] + "'s talent code: ");
                input = Console.ReadLine();

                while (!talentCodes.Contains(input))
                {
                    Console.WriteLine("Invalid input entered.");
                    Console.WriteLine(validTalentOptions);
                    Console.Write(namesAndTalents[contestantNum, 0] + "'s talent code: ");
                    input = Console.ReadLine();
                }

                namesAndTalents[contestantNum, 1] = input;
                talentCounts[talentCodes.IndexOf(input)]++;

                contestantNum++;
            }
            
            // done entering names and codes

            Console.WriteLine("\n\n*********************************************************\n");

            Console.WriteLine("Counts for each type of talent: ");
            foreach (string talent in talentDescs) 
            {
                Console.WriteLine(talent + ": " + talentCounts[talentDescs.IndexOf(talent)]);
            }

            Console.WriteLine("\n*********************************************************\n");

            Console.Write("Would you like to list contestants by talent? (y/n): ");
            input = Console.ReadLine();

            while (input != "y" && input != "n")
            {
                Console.Write("Would you like to list contestants by talent? (y/n): ");
                input = Console.ReadLine();
            }

            if (input == "y")
            {
                // yes we want to search
                Console.WriteLine(validTalentOptions);
                Console.Write("Please enter a talent code or '" + QUIT_SEQ + "' to end: ");

                input = Console.ReadLine();

                // the user didn't immediately quit
                while (input != QUIT_SEQ)
                {
                    // the user didn't enter a valid code and the code was also not the quit sequence
                    while (!talentCodes.Contains(input) && input != QUIT_SEQ)
                    {
                        Console.WriteLine("Invalid input entered.");
                        Console.WriteLine(validTalentOptions);
                        Console.Write("Please enter a talent code or '" + QUIT_SEQ + "' to end: ");
                        input = Console.ReadLine();
                    }
                    if (input == QUIT_SEQ) break;

                    // handle we got valid input here
                    Console.WriteLine("Contestants signed up for " + talentDescs[talentCodes.IndexOf(input)] + ": ");
                    Console.WriteLine("--------------------------------------------------------");

                    for (int i = 0; i < namesAndTalents.GetLength(0); i++)
                    {
                        if (namesAndTalents[i, 1] == input)
                            Console.WriteLine(namesAndTalents[i, 0]);
                    }

                    // want to search again?
                    Console.WriteLine("\n\nSearch for another talent code?");
                    Console.WriteLine(validTalentOptions);
                    Console.Write("Please enter a talent code or '" + QUIT_SEQ + "' to end: ");
                    input = Console.ReadLine();
                }
            }

            Console.WriteLine("\n\n*********************************************************\n");

            Console.WriteLine("Contestants entered last year: " + contestantsLastYear);
            Console.WriteLine("Contestants entered this year: " + contestantsThisYear);

            Console.WriteLine("\nLast year's revenue: " + (contestantsLastYear * ENTRY_FEE).ToString("C"));
            Console.WriteLine("This year's revenue: " + (contestantsThisYear * ENTRY_FEE).ToString("C"));
            Console.WriteLine("Revenue difference:  " + ((contestantsThisYear * ENTRY_FEE) - (contestantsLastYear * ENTRY_FEE)).ToString("C"));

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
    }
}
