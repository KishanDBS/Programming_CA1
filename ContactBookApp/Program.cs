using System;
using System.Text.RegularExpressions;

namespace ContactBookApp
{
    class Program
    {
        /// <summary>
        /// Generic input validator using regex.
        /// - Prompts user for input.
        /// - Validates against regex pattern.
        /// - Shows error if invalid.
        /// - Allows "X" to cancel input.
        /// - Limits invalid attempts to 3.
        /// Returns string or null if cancelled/too many invalid attempts.
        /// </summary>
        static string? GetValidInput(string prompt, string pattern, string errorMessage)
        {
            string? input;
            int attempts = 0;

            while (true)
            {
                Console.Write(prompt);
                input = Console.ReadLine();

                // Allow user to cancel
                if (input?.Trim().ToUpper() == "X")
                {
                    Console.WriteLine("Cancelled input. Returning to menu...");
                    return null;
                }

                // Validate against regex
                if (!string.IsNullOrEmpty(input) && Regex.IsMatch(input, pattern))
                {
                    return input; // Valid
                }
                else
                {
                    Console.WriteLine(errorMessage);
                    attempts++;

                    if (attempts >= 3)
                    {
                        Console.WriteLine("Too many invalid attempts. Returning to menu...");
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Mobile number validator.
        /// - Must be 9 digits, not starting with 0.
        /// - Allows "X" to cancel.
        /// - Limits invalid attempts to 3.
        /// Returns string or null if cancelled/too many invalid attempts.
        /// </summary>
        static string? GetValidMobile(string prompt)
        {
            string? input;
            int attempts = 0;

            while (true)
            {
                Console.Write(prompt);
                input = Console.ReadLine();

                if (input?.Trim().ToUpper() == "X")
                {
                    Console.WriteLine("Cancelled input. Returning to menu...");
                    return null;
                }

                if (!string.IsNullOrEmpty(input) && Regex.IsMatch(input, @"^[1-9][0-9]{8}$"))
                {
                    return input; // Valid mobile
                }
                else
                {
                    Console.WriteLine("Invalid mobile number. Must be 9 digits, not starting with 0.");
                    attempts++;

                    if (attempts >= 3)
                    {
                        Console.WriteLine("Too many invalid attempts. Returning to menu...");
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Date validator.
        /// - Accepts yyyy-MM-dd or dd/MM/yyyy formats.
        /// - Uses regex + DateTime.TryParse.
        /// - Allows "X" to cancel.
        /// - Limits invalid attempts to 3.
        /// Returns DateTime? (nullable) so caller can handle cancellation.
        /// </summary>
        static DateTime? GetValidDate(string prompt)
        {
            string? input;
            DateTime date;
            int attempts = 0;
            string pattern = @"^\d{1,2}[-/ ]\d{1,2}[-/ ]\d{4}$|^\d{4}[-/ ]\d{1,2}[-/ ]\d{1,2}$";

            while (true)
            {
                Console.Write(prompt);
                input = Console.ReadLine();

                if (input?.Trim().ToUpper() == "X")
                {
                    Console.WriteLine("Cancelled input. Returning to menu...");
                    return null;
                }

                if (!string.IsNullOrEmpty(input) && Regex.IsMatch(input, pattern) && DateTime.TryParse(input, out date))
                {
                    return date; // Valid date
                }
                else
                {
                    Console.WriteLine("Invalid date format. Please enter again (yyyy-MM-dd or dd/MM/yyyy).");
                    attempts++;

                    if (attempts >= 3)
                    {
                        Console.WriteLine("Too many invalid attempts. Returning to menu...");
                        return null;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            ContactBook book = new ContactBook();
            book.LoadContacts(); // Load contacts from JSON

            int choice;
            do
            {
                Console.WriteLine("\n--- Main Menu ---");
                Console.WriteLine("1: Add Contact");
                Console.WriteLine("2: Show All Contacts");
                Console.WriteLine("3: Show Contact Details");
                Console.WriteLine("4: Update Contact");
                Console.WriteLine("5: Delete Contact");
                Console.WriteLine("0: Exit");
                Console.Write("Enter your choice: ");

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1: // Add Contact
                        string? firstName = GetValidInput("First Name: ", @"^[A-Za-z][A-Za-z\s\-]*$", "Invalid first name.");
                        if (firstName == null) break;

                        string? lastName = GetValidInput("Last Name: ", @"^[A-Za-z][A-Za-z\s\-]*$", "Invalid last name.");
                        if (lastName == null) break;

                        string? company = GetValidInput("Company: ", @"^[A-Za-z0-9\s\.\-&]+$", "Invalid company name.");
                        if (company == null) break;

                        string? mobile = GetValidMobile("Mobile (9 digits, not starting with 0): ");
                        if (mobile == null) break;

                        string? email = GetValidInput("Email: ", @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", "Invalid email.");
                        if (email == null) break;

                        DateTime? birthdate = GetValidDate("Birthdate (yyyy-MM-dd or dd/MM/yyyy): ");
                        if (birthdate == null) break;

                        try
                        {
                            Contact newContact = new Contact(firstName, lastName, company, mobile, email, birthdate.Value);
                            book.AddContact(newContact);
                            book.SaveContacts();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;

                    case 2: // Show All Contacts
                        book.ShowAllContacts();
                        break;

                    case 3: // Show Contact Details
                        string? searchMobile = GetValidMobile("Enter mobile to view details (or X to cancel): ");
                        if (searchMobile != null)
                            book.ShowContactDetails(searchMobile);
                        break;

                    case 4: // Update Contact
                        string? oldMobile = GetValidMobile("Enter mobile to update (or X to cancel): ");
                        if (oldMobile == null) break;

                        string? newFirst = GetValidInput("New First Name: ", @"^[A-Za-z][A-Za-z\s\-]*$", "Invalid first name.");
                        if (newFirst == null) break;

                        string? newLast = GetValidInput("New Last Name: ", @"^[A-Za-z][A-Za-z\s\-]*$", "Invalid last name.");
                        if (newLast == null) break;

                        string? newCompany = GetValidInput("New Company: ", @"^[A-Za-z0-9\s\.\-&]+$", "Invalid company.");
                        if (newCompany == null) break;

                        string? newMobile = GetValidMobile("New Mobile (or X to cancel): ");
                        if (newMobile == null) break;

                        string? newEmail = GetValidInput("New Email: ", @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", "Invalid email.");
                        if (newEmail == null) break;

                        DateTime? newBirth = GetValidDate("New Birthdate: ");
                        if (newBirth == null) break;

                        try
                        {
                            Contact updated = new Contact(newFirst, newLast, newCompany, newMobile, newEmail, newBirth.Value);
                            book.UpdateContact(oldMobile, updated);
                            book.SaveContacts();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;

                    case 5: // Delete Contact
                        string? deleteMobile = GetValidMobile("Enter mobile to delete (or X to cancel): ");
                        if (deleteMobile == null) break;

                        Console.Write($"Confirm delete {deleteMobile}? (Y/N): ");
                        string? confirm = Console.ReadLine();

                        if (confirm?.Trim().ToUpper() == "Y")
                        {
                            book.DeleteContact(deleteMobile);
                            book.SaveContacts();
                        }
                        else
                        {
                            Console.WriteLine("Deletion cancelled.");
                        }
                        break;

                    case 0:
                        Console.WriteLine("Exiting program...");
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }

            } while (choice != 0);
        }
    }
}