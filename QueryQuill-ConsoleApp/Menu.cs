using QueryQuill_Lib;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace QueryQuill_ConsoleApp
{
    /// <summary>
    /// Static class containing the functionality and interaction with the application
    /// </summary>
    internal static class Menu
    {
        private static DorkParams _dorkParms = new();
        private static int _paramUniqueKey = 0;

        /// <summary>
        /// Method displaying the application logo
        /// </summary>
        public static void QueryQuillLogo()
        {
            Console.WriteLine(@"
  ______                                            ______             __  __  __ 
 /      \                                          /      \           |  \|  \|  \
|  $$$$$$\ __    __   ______    ______   __    __ |  $$$$$$\ __    __  \$$| $$| $$
| $$  | $$|  \  |  \ /      \  /      \ |  \  |  \| $$  | $$|  \  |  \|  \| $$| $$
| $$  | $$| $$  | $$|  $$$$$$\|  $$$$$$\| $$  | $$| $$  | $$| $$  | $$| $$| $$| $$
| $$ _| $$| $$  | $$| $$    $$| $$   \$$| $$  | $$| $$ _| $$| $$  | $$| $$| $$| $$
| $$/ \ $$| $$__/ $$| $$$$$$$$| $$      | $$__/ $$| $$/ \ $$| $$__/ $$| $$| $$| $$
 \$$ $$ $$ \$$    $$ \$$     \| $$       \$$    $$ \$$ $$ $$ \$$    $$| $$| $$| $$
  \$$$$$$\  \$$$$$$   \$$$$$$$ \$$       _\$$$$$$$  \$$$$$$\  \$$$$$$  \$$ \$$ \$$
      \$$$                              |  \__| $$      \$$$                      
                                         \$$    $$                                
                                          \$$$$$$                                 
");
            return;
        }

        /// <summary>
        /// Method containing the display and functionality of the main menu
        /// </summary>
        public static void MainMenu()
        {
            WriteMenuOption([
                    "View active dork parameters",
                    "Add dork parameter",
                    "Remove dork parameter",
                    "Generate dork",
                    "Reset",
                    "Quit\n"
                ]);

            switch (ReadUserOptionChoice("Enter option number: ", 6))
            {
                case 0:
                    ViewActiveDorkParams(_dorkParms);
                    Console.Write("\nPress Any Key to go back");
                    Console.ReadKey();
                    return;

                case 1:
                    AddDorkParam(_dorkParms);
                    return;

                case 2:
                    RemoveDorkParam(_dorkParms);
                    Console.Write("\nPress Any Key to go back");
                    Console.ReadKey();
                    return;

                case 3:
                    GenerateDork(_dorkParms);
                    Console.Write("\nPress Any Key to go back");
                    Console.ReadKey();
                    return;

                case 4:
                    _dorkParms = new();
                    _paramUniqueKey = 0;
                    return;

                case 5:
                    Environment.Exit(0);
                    return;
            }

            return;
        }

        /// <summary>
        /// Method for managing the menu to add a parameter to the Dork.
        /// </summary>
        /// <param name="dorkParams">Represents the parameters of the Dork.</param>
        private static void AddDorkParam(DorkParams dorkParams)
        {
            string[] dorksList = Enum.GetNames(typeof(DorksList));
            WriteMenuOption(dorksList);

            byte userChoice = ReadUserOptionChoice($"Enter the chosen setting ([{dorksList.Length}] to cancel): ", (byte)(dorksList.Length + 1));
            Console.Clear();
            switch (userChoice)
            {
                case 8:
                    string userStartDateInput;
                    string userEndDateInput;
                    Console.WriteLine();

                    do
                    {
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Console.WriteLine(new string(' ', Console.WindowWidth));
                        Console.SetCursorPosition(0, Console.CursorTop - 1);

                        Console.Write("Enter the start date (YYYY-MM-DD): ");
                        userStartDateInput = Console.ReadLine()?.Replace("Enter the start date (YYYY-MM-DD): ", "") ?? "";

                    } while (!CheckDateValid(userStartDateInput));

                    do
                    {
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Console.WriteLine(new string(' ', Console.WindowWidth));
                        Console.SetCursorPosition(0, Console.CursorTop - 1);

                        Console.Write($"Enter the end date (YYYY-MM-DD): ");
                        userEndDateInput = Console.ReadLine()?.Replace($"Enter the end date (YYYY-MM-DD): ", "") ?? "";

                    } while (!CheckDateValid(userEndDateInput));

                    dorkParams.AddParam(
                                [
                                    dorksList[userChoice],
                                    userStartDateInput,
                                    userEndDateInput
                                ], _paramUniqueKey++
                            );
                    return;

                case 9:
                case 10:
                    string userDateInput;
                    Console.WriteLine();

                    do
                    {
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Console.WriteLine(new string(' ', Console.WindowWidth));
                        Console.SetCursorPosition(0, Console.CursorTop - 1);

                        Console.Write($"Enter the date (YYYY-MM-DD) for {dorksList[userChoice]}: ");
                        userDateInput = Console.ReadLine()?.Replace($"Enter the date (YYYY-MM-DD) for {dorksList[userChoice]}: ", "") ?? "";

                    } while (!CheckDateValid(userDateInput));

                    dorkParams.AddParam(
                                [
                                    dorksList[userChoice],
                                    userDateInput
                                ], _paramUniqueKey++
                            );
                    return;

                default:
                    Console.Write($"Enter the text querry for {dorksList[userChoice]}: ");
                    dorkParams.AddParam(
                            [
                                dorksList[userChoice],
                                Console.ReadLine()?.Replace($"Enter the text querry for {dorksList[userChoice]}: ", "") ?? ""
                            ], _paramUniqueKey++
                        );
                    return;
            }
        }

        /// <summary>
        /// Method for managing the menu to remove a parameter to the Dork.
        /// </summary>
        /// <param name="dorkParams">Represents the parameters of the Dork.</param>
        private static void RemoveDorkParam(DorkParams dorkParams)
        {
            ViewActiveDorkParams(dorkParams);
            Console.Write("\nEnter the parameter number to remove, or enter an invalid number to go back: ");

            _dorkParms.RemoveParam(int.Parse(Console.ReadLine()?.Replace("Enter the parameter number to remove, or enter an invalid number to go back: ", "") ?? ""));

            return;
        }

        /// <summary>
        /// Method for managing the menu to generate the Dork.
        /// </summary>
        /// <param name="dorkParams">Represents the parameters of the Dork.</param>
        private static void GenerateDork(DorkParams dorkParams)
        {
            Dork dorkGenerated = DorkGenerator.GenerateDork(dorkParams);

            Console.Clear();
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Query");
            Console.ResetColor();
            Console.Write("] : ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(dorkGenerated.Query + "\n");
            Console.ResetColor();

            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Url");
            Console.ResetColor();
            Console.Write("] : ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(dorkGenerated.Link.ToString().Replace(" ", "%20") + "\n");
            Console.ResetColor();

            return;
        }

        /// <summary>
        /// Method for displaying the active parameters of the Dork.
        /// </summary>
        /// <param name="dorkParams">Represents the parameters of the Dork.</param>
        private static void ViewActiveDorkParams(DorkParams dorkParams)
        {
            Console.Clear();

            foreach (KeyValuePair<int, string[]> dorkParam in dorkParams.Params)
            {
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Red;

                Console.Write(dorkParam.Key);

                Console.ResetColor();
                Console.Write("] : ");
                Console.ForegroundColor = ConsoleColor.Green;

                if (dorkParam.Value[0] == "daterange:")
                {
                    Console.Write($"daterange: {dorkParam.Value[1]} to {dorkParam.Value[2]}\n");
                    Console.ResetColor();
                    continue;
                }

                Console.Write($"{dorkParam.Value[0]} {dorkParam.Value[1]}\n");
                Console.ResetColor();
            }

            return;
        }

        /// <summary>
        /// Method for displaying the menu options.
        /// </summary>
        /// <param name="menuOptions">Represents the options of the menu.</param>
        private static void WriteMenuOption(string[] menuOptions)
        {
            byte optionNb = 0;
            foreach (string option in menuOptions)
            {
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Red;

                Console.Write(optionNb++);

                Console.ResetColor();
                Console.Write("] : ");
                Console.ForegroundColor = ConsoleColor.Green;

                Console.Write(option + "\n");

                Console.ResetColor();
            }

            return;
        }

        /// <summary>
        /// Method for verifying user input.
        /// </summary>
        /// <param name="inputPrompt">Represents the prompt</param>
        /// <param name="nbOption">Number of menu option</param>
        /// <returns></returns>
        private static byte ReadUserOptionChoice(string inputPrompt, byte nbOption)
        {
            byte optionNumberChoice;

            Console.WriteLine();
            do
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.WriteLine(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write(inputPrompt);
            }
            while
            (
                !byte.TryParse(Console.ReadLine()?.Replace(inputPrompt, ""), out optionNumberChoice)
                || optionNumberChoice < 0
                || optionNumberChoice >= nbOption
            );

            return optionNumberChoice;
        }

        /// <summary>
        /// Method for verifying the date in the correct format YYYY-MM-DD.
        /// </summary>
        /// <param name="dateStringTotTest">String who contain the date to test.</param>
        /// <returns></returns>
        private static bool CheckDateValid(string dateStringTotTest)
        {
            return Regex.IsMatch(dateStringTotTest, @"^\d{4}\-(0[1-9]|1[012])\-(0[1-9]|[12][0-9]|3[01])$");
        }
    }
}
