using System;

namespace QueryQuill_ConsoleApp
{
    /// <summary>
    /// Main class of the application
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Program's entry point method representing its main loop
        /// </summary>
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();

                Menu.QueryQuillLogo();

                Menu.MainMenu();
            }
        }
    }
}