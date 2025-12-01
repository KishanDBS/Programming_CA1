using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace ContactBookApp
{
    public class ContactBook
    {
        private List<Contact> contacts = new List<Contact>();
        private string filePath = "contacts.json";

        public ContactBook()
        {
            LoadFromJson();
        }

        public bool IsEmpty() => contacts.Count == 0;

        // Add contact with duplicate checks
        public void AddContact(Contact contact)
        {
            bool nameExists = contacts.Exists(c =>
                c.FirstName.Equals(contact.FirstName, StringComparison.OrdinalIgnoreCase) &&
                c.LastName.Equals(contact.LastName, StringComparison.OrdinalIgnoreCase));

            bool mobileExists = contacts.Exists(c => c.MobileNumber == contact.MobileNumber);

            if (nameExists)
            {
                Console.WriteLine("❌ A contact with the same name already exists.");
                return;
            }

            if (mobileExists)
            {
                Console.WriteLine("❌ A contact with the same mobile number already exists.");
                return;
            }

            if (!IsValidEmail(contact.Email))
            {
                Console.WriteLine("❌ Invalid email format.");
                return;
            }

            contacts.Add(contact);
            SaveToJson();
            Console.WriteLine("✅ Contact added successfully.");
        }

        public void ShowAllContacts()
        {
            if (IsEmpty())
            {
                Console.WriteLine("⚠️ No contacts available.");
                return;
            }

            Console.WriteLine("📇 All Contacts:");
            foreach (var c in contacts)
                Console.WriteLine($"{c.FirstName} {c.LastName} - {c.MobileNumber}");
        }

        public void ShowContactDetails(string firstName, string lastName)
        {
            var contact = contacts.Find(c =>
                c.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
                c.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));

            if (contact != null)
            {
                Console.WriteLine($"First Name: {contact.FirstName}");
                Console.WriteLine($"Last Name: {contact.LastName}");
                Console.WriteLine($"Company: {contact.Company}");
                Console.WriteLine($"Mobile Number: {contact.MobileNumber}");
                Console.WriteLine($"Email: {contact.Email}");
                Console.WriteLine($"Birthdate: {contact.Birthdate.ToShortDateString()}");
            }
            else
                Console.WriteLine("❌ Contact not found.");
        }

        public void UpdateContact(string firstName, string lastName)
        {
            var contact = contacts.Find(c =>
                c.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
                c.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));

            if (contact == null)
            {
                Console.WriteLine("❌ Contact not found.");
                return;
            }

            Console.WriteLine("Enter new first name:");
            string newFirstName = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter new last name:");
            string newLastName = Console.ReadLine() ?? string.Empty;

            bool nameExists = contacts.Exists(c =>
                c.FirstName.Equals(newFirstName, StringComparison.OrdinalIgnoreCase) &&
                c.LastName.Equals(newLastName, StringComparison.OrdinalIgnoreCase) &&
                c != contact);

            if (nameExists)
            {
                Console.WriteLine("❌ Another contact already uses this name.");
                return;
            }

            contact.FirstName = newFirstName;
            contact.LastName = newLastName;

            Console.WriteLine("Enter new company:");
            contact.Company = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter new mobile number:");
            string mob = Console.ReadLine() ?? string.Empty;

            bool mobileExists = contacts.Exists(c => c.MobileNumber == mob && c != contact);

            if (mobileExists)
            {
                Console.WriteLine("❌ Another contact already uses this mobile number.");
                return;
            }

            contact.SetMobileNumber(mob);

            Console.WriteLine("Enter new email:");
            string newEmail = Console.ReadLine() ?? string.Empty;
            if (IsValidEmail(newEmail))
                contact.Email = newEmail;
            else
                Console.WriteLine("❌ Invalid email format. Keeping old email.");

            Console.WriteLine("Enter new birthdate (dd/MM/yyyy):");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime bd))
                contact.Birthdate = bd;
            else
                Console.WriteLine("❌ Invalid date format. Keeping old birthdate.");

            SaveToJson();
            Console.WriteLine("✅ Contact updated successfully.");
        }

        public void DeleteContact(string firstName, string lastName)
        {
            var contact = contacts.Find(c =>
                c.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
                c.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));

            if (contact != null)
            {
                contacts.Remove(contact);
                SaveToJson();
                Console.WriteLine("✅ Contact deleted successfully.");
            }
            else
                Console.WriteLine("❌ Contact not found.");
        }

        private void SaveToJson()
        {
            string jsonString = JsonSerializer.Serialize(contacts, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, jsonString);
        }

        private void LoadFromJson()
        {
            if (File.Exists(filePath))
            {
                try
                {
                    string jsonString = File.ReadAllText(filePath);
                    contacts = JsonSerializer.Deserialize<List<Contact>>(jsonString) ?? new List<Contact>();
                }
                catch
                {
                    Console.WriteLine("⚠️ Error reading contacts.json. Starting with empty list.");
                    contacts = new List<Contact>();
                }
            }
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        // Public helper so Program.cs can call it
        public Contact PromptNewContact()
        {
            Console.WriteLine("Enter First Name:");
            string fn = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Enter Last Name:");
            string ln = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Enter Company:");
            string comp = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Enter Mobile Number:");
            string mob = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Enter Email:");
            string email = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Enter Birthdate (dd/MM/yyyy):");
            DateTime bd;
            if (!DateTime.TryParse(Console.ReadLine(), out bd))
            {
                bd = DateTime.Now;
                Console.WriteLine("⚠️ Invalid date format. Using today's date.");
            }

            var newContact = new Contact
            {
                FirstName = fn,
                LastName = ln,
                Company = comp,
                Email = email,
                Birthdate = bd
            };
            newContact.SetMobileNumber(mob);

            return newContact;
        }
    }
}