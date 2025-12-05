using System;

namespace FileExtensionInfoSystem
{
    /// <summary>
    /// Main entry point of the application.
    /// Displays a numbered menu and handles user input.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            ExtensionService service = new ExtensionService();

            // Show the menu at the start
            ShowMenu();

            // Infinite loop until user chooses exit
            while (true)
            {
                Console.Write("\nEnter a file extension (e.g., .mp4, .pdf, .jpg) or choose an option (1,2,3): ");
                string input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("⚠️ Please enter a valid input.");
                    continue;
                }

                // Option 2 - Exit
                if (input.Equals("2", StringComparison.OrdinalIgnoreCase) || input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Goodbye! Thank you for using the system.");
                    break;
                }

                // Option 1 - List all extensions
                if (input.Equals("1", StringComparison.OrdinalIgnoreCase) || input.Equals("list", StringComparison.OrdinalIgnoreCase))
                {
                    service.ListAllExtensions();
                    ShowMenu(); // Return to main menu
                    continue;
                }

                // Option 3 - Help (re-show menu)
                if (input.Equals("3", StringComparison.OrdinalIgnoreCase) || input.Equals("help", StringComparison.OrdinalIgnoreCase))
                {
                    service.ShowHelp();
                    ShowMenu(); // Return to main menu
                    continue;
                }

                // Default: treat input as a file extension query
                service.QueryExtension(input);

                // After query, return to main menu
                ShowMenu();
            }
        }

        /// <summary>
        /// Displays the numbered menu options to the user.
        /// </summary>
        static void ShowMenu()
        {
            Console.WriteLine("\n=== File Extension Information System ===");
            Console.WriteLine("Enter a file extension (e.g., .mp4, .pdf, .jpg).");
            Console.WriteLine("Option 1 - List all supported extensions.");
            Console.WriteLine("Option 2 - Exit the program.");
            Console.WriteLine("Option 3 - Show instructions again.");
        }
    }
}