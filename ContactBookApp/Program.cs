using System;
using System.Text.RegularExpressions;

namespace ContactBookApp
{
    class Program
    {
        // Helper method: Validates input against a regex pattern.
        // Keeps prompting until the user enters a valid value.
        static string GetValidInput(string prompt, string pattern, string errorMessage)
        {
            string input;
            while (true)
            {
                Console.Write(prompt);
                input = Console.ReadLine();

                // Check input against regex pattern
                if (Regex.IsMatch(input, pattern))
                {
                    return input; // Valid input, return it
                }
                else
                {
                    Console.WriteLine(errorMessage); // Show error and re-prompt
                }
            }
        }

        // Helper method: Validates date input using regex + DateTime.TryParse
        // Accepts formats like yyyy-MM-dd or dd/MM/yyyy
        static DateTime GetValidDate(string prompt)
        {
            string input;
            DateTime date;
            // Regex allows either dd/MM/yyyy or yyyy-MM-dd formats
            string pattern = @"^\d{1,2}[-/ ]\d{1,2}[-/ ]\d{4}$|^\d{4}[-/ ]\d{1,2}[-/ ]\d{1,2}$";

            while (true)
            {
                Console.Write(prompt);
                input = Console.ReadLine();

                // Check regex first, then try parsing
                if (Regex.IsMatch(input, pattern) && DateTime.TryParse(input, out date))
                {
                    return date; // Valid date
                }
                else
                {
                    Console.WriteLine("Invalid date format. Please enter a valid date (e.g., 1990-01-01 or 01/01/1990).");
                }
            }
        }

        static void Main(string[] args)
        {
            ContactBook book = new ContactBook(); // ContactBook manages all contacts
            int choice;

            do
            {
                // Display menu options
                Console.WriteLine("\n--- Main Menu ---");
                Console.WriteLine("1: Add Contact");
                Console.WriteLine("2: Show All Contacts");
                Console.WriteLine("3: Show Contact Details");
                Console.WriteLine("4: Update Contact");
                Console.WriteLine("5: Delete Contact");
                Console.WriteLine("0: Exit");
                Console.Write("Enter your choice: ");

                // Validate menu choice input
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue; // Go back to menu
                }

                switch (choice)
                {
                    case 1: // Add Contact
                        // Validate each field using regex
                        string firstName = GetValidInput("First Name: ", @"^[A-Za-z][A-Za-z\s\-]*$", "Invalid first name. Use only letters.");
                        string lastName = GetValidInput("Last Name: ", @"^[A-Za-z][A-Za-z\s\-]*$", "Invalid last name. Use only letters.");
                        string company = GetValidInput("Company: ", @"^[A-Za-z0-9\s\.\-&]+$", "Invalid company name.");
                        string mobile = GetValidInput("Mobile Number (9 digits, not starting with 0): ", @"^[1-9][0-9]{8}$", "Invalid mobile number format.");
                        string email = GetValidInput("Email: ", @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", "Invalid email format.");
                        DateTime birthdate = GetValidDate("Birthdate (yyyy-MM-dd or dd/MM/yyyy): ");

                        try
                        {
                            // Create new contact object
                            Contact newContact = new Contact(firstName, lastName, company, mobile, email, birthdate);
                            book.AddContact(newContact); // Add to ContactBook
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message); // Show validation errors from Contact class
                        }
                        break;

                    case 2: // Show All Contacts
                        book.ShowAllContacts();
                        break;

                    case 3: // Show Contact Details
                        string searchMobile = GetValidInput("Enter mobile number to view details: ", @"^[1-9][0-9]{8}$", "Invalid mobile number format.");
                        book.ShowContactDetails(searchMobile);
                        break;

                    case 4: // Update Contact
                        string oldMobile = GetValidInput("Enter mobile number to update: ", @"^[1-9][0-9]{8}$", "Invalid mobile number format.");
                        string newFirst = GetValidInput("New First Name: ", @"^[A-Za-z][A-Za-z\s\-]*$", "Invalid first name.");
                        string newLast = GetValidInput("New Last Name: ", @"^[A-Za-z][A-Za-z\s\-]*$", "Invalid last name.");
                        string newCompany = GetValidInput("New Company: ", @"^[A-Za-z0-9\s\.\-&]+$", "Invalid company name.");
                        string newMobile = GetValidInput("New Mobile Number: ", @"^[1-9][0-9]{8}$", "Invalid mobile number format.");
                        string newEmail = GetValidInput("New Email: ", @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", "Invalid email format.");
                        DateTime newBirth = GetValidDate("New Birthdate (yyyy-MM-dd or dd/MM/yyyy): ");

                        try
                        {
                            // Create updated contact object
                            Contact updated = new Contact(newFirst, newLast, newCompany, newMobile, newEmail, newBirth);
                            book.UpdateContact(oldMobile, updated); // Update existing contact
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case 5:
                        // Ask for mobile number to delete
                        string deleteMobile = GetValidInput("Enter mobile number to delete: ", @"^[1-9][0-9]{8}$", "Invalid mobile number format.");

                        // Confirm deletion
                        Console.Write($"Are you sure you want to delete contact with mobile {deleteMobile}? (Y/N): ");
                        string confirm = Console.ReadLine();

                        if (confirm?.Trim().ToUpper() == "Y")
                        {
                            book.DeleteContact(deleteMobile); // Proceed with deletion
                        }
                        else
                        {
                            Console.WriteLine("Deletion cancelled.");
                        }
                        break;
                        
                    case 0: // Exit
                        Console.WriteLine("Exiting program...");
                        break;

                    default: // Invalid menu choice
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

            } while (choice != 0); // Keep looping until user chooses Exit
        }
    }
}