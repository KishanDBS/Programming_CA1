using System;

namespace FileExtensionInfoSystem
{
    /// <summary>
    /// Service class that handles user queries and interacts with the repository.
    /// Responsible for user-facing logic (messages, validation, auto-correction).
    /// </summary>
    public class ExtensionService
    {
        private ExtensionRepository repository;

        // Constructor initializes the repository
        public ExtensionService()
        {
            repository = new ExtensionRepository();
        }

        /// <summary>
        /// Handles a user query for a file extension.
        /// Auto-corrects if the user forgets to type the leading dot.
        /// </summary>
        public void QueryExtension(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("⚠️ Please enter a valid file extension.");
                return;
            }

            // Auto-correct: if user forgets the dot, add it
            if (!input.StartsWith("."))
            {
                input = "." + input;
                Console.WriteLine($"ℹ️ Auto-corrected your input to '{input}'.");
            }

            var info = repository.GetExtensionInfo(input);
            if (info != null)
            {
                Console.WriteLine($"ℹ️ {info.Extension} : {info.Description}");
            }
            else
            {
                Console.WriteLine($"❓ Sorry, information for '{input}' is not available.");
                Console.WriteLine("Tip: Try common extensions like .mp4, .pdf, .jpg, .mp3, etc.");
            }
        }

        /// <summary>
        /// Lists all supported extensions with their descriptions.
        /// This is used for Option 1 in the menu.
        /// </summary>
        public void ListAllExtensions()
    {
        Console.WriteLine("\nSupported Extensions with Descriptions:");

        // Print table header
        Console.WriteLine($"{"Extension",-15} {"Description",-40}");
        Console.WriteLine(new string('-', 60)); // separator line

        // Print each extension in table format
        foreach (var info in repository.GetAllExtensionInfos())
        {
            Console.WriteLine($"{info.Extension,-15} {info.Description,-40}");
        }
    }

        /// <summary>
        /// Displays instructions for using the system.
        /// This is used for Option 3 in the menu.
        /// </summary>
        public void ShowHelp()
        {
            Console.WriteLine("\n=== Instructions ===");
            Console.WriteLine("Enter a file extension (e.g., .mp4, .pdf, .jpg).");
            Console.WriteLine("Option 1 - List all supported extensions with descriptions.");
            Console.WriteLine("Option 2 - Exit the program.");
            Console.WriteLine("Option 3 - Show instructions again.");
        }
    }
}