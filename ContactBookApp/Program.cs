using System;

namespace ContactBookApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ContactBook book = new ContactBook();
            int choice;

            do
            {
                Console.Clear();
                Console.WriteLine("=== Contact Book Menu ===");
                Console.WriteLine("1: Add Contact");
                Console.WriteLine("2: Show All Contacts");
                Console.WriteLine("3: Show Contact Details");
                Console.WriteLine("4: Update Contact");
                Console.WriteLine("5: Delete Contact");
                Console.WriteLine("0: Exit");
                Console.Write("Enter choice: ");

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("❌ Invalid input. Please enter a number.");
                    Pause();
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        var newContact = book.PromptNewContact();
                        book.AddContact(newContact);
                        Pause();
                        break;

                    case 2:
                        book.ShowAllContacts();
                        Pause();
                        break;

                    case 3:
                        Console.WriteLine("Enter First Name:");
                        string fn = Console.ReadLine() ?? string.Empty;
                        Console.WriteLine("Enter Last Name:");
                        string ln = Console.ReadLine() ?? string.Empty;
                        book.ShowContactDetails(fn, ln);
                        Pause();
                        break;

                    case 4:
                        Console.WriteLine("Enter First Name:");
                        fn = Console.ReadLine() ?? string.Empty;
                        Console.WriteLine("Enter Last Name:");
                        ln = Console.ReadLine() ?? string.Empty;
                        book.UpdateContact(fn, ln);
                        Pause();
                        break;

                    case 5:
                        Console.WriteLine("Enter First Name:");
                        fn = Console.ReadLine() ?? string.Empty;
                        Console.WriteLine("Enter Last Name:");
                        ln = Console.ReadLine() ?? string.Empty;
                        book.DeleteContact(fn, ln);
                        Pause();
                        break;

                    case 0:
                        Console.WriteLine("Exiting...");
                        break;

                    default:
                        Console.WriteLine("❌ Invalid choice.");
                        Pause();
                        break;
                }
            } while (choice != 0);
        }

        // Helper method to pause consistently
        private static void Pause()
        {
            Console.WriteLine("\nPress Enter to return to the menu...");
            Console.ReadLine();
        }
    }
}