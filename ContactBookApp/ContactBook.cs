using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ContactBookApp
{
    public class ContactBook
    {
        private List<Contact> contacts = new List<Contact>();
        private string filePath = "contacts.json";

        // Load contacts from JSON file
      public void LoadContacts()
{
    if (File.Exists(filePath))
    {
        string json = File.ReadAllText(filePath);
        if (!string.IsNullOrWhiteSpace(json))
        {
            try
            {
                // Try to deserialize
                var loadedContacts = JsonSerializer.Deserialize<List<Contact>>(json);

                // Validate each contact safely
                contacts = new List<Contact>();
                foreach (var c in loadedContacts)
                {
                    try
                    {
                        // Re-run constructor validation
                        Contact validContact = new Contact(
                            c.FirstName,
                            c.LastName,
                            c.Company,
                            c.MobileNumber,
                            c.Email,
                            c.Birthdate
                        );
                        contacts.Add(validContact);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Skipped invalid contact: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading contacts.json: {ex.Message}");
                contacts = new List<Contact>(); // fallback
            }
        }
        else
        {
            contacts = new List<Contact>(); // empty file
        }
    }
}

        // Save contacts to JSON file
        public void SaveContacts()
        {
            string json = JsonSerializer.Serialize(contacts, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        public void AddContact(Contact contact)
        {
            foreach (var c in contacts)
            {
                if (c.MobileNumber == contact.MobileNumber)
                {
                    Console.WriteLine("Error: Duplicate mobile number.");
                    return;
                }
            }
            contacts.Add(contact);
            Console.WriteLine("Contact added successfully.");
        }

        public void ShowAllContacts()
    {
        if (contacts.Count == 0)
        {
            Console.WriteLine("No contacts found.");
            return;
        }

        Console.WriteLine("--- All Contacts ---");

        // Print table header
        Console.WriteLine(
            $"{"Name",-20} {"Company",-15} {"Mobile",-12} {"Email",-25} {"Birthdate",-12}"
        );
        Console.WriteLine(new string('-', 90)); // separator line

        // Print each contact in table format
        foreach (var contact in contacts)
        {
            string fullName = $"{contact.FirstName} {contact.LastName}";
            Console.WriteLine(
                $"{fullName,-20} {contact.Company,-15} {contact.MobileNumber,-12} {contact.Email,-25} {contact.Birthdate.ToShortDateString(),-12}"
            );
        }
}

        public void ShowContactDetails(string mobileNumber)
        {
            if (contacts.Count == 0)
            {
                Console.WriteLine("No contacts available.");
                return;
            }

            var contact = contacts.Find(c => c.MobileNumber == mobileNumber);
            if (contact != null)
                Console.WriteLine(contact.ShowDetails(true));
            else
                Console.WriteLine("Contact not found.");
        }

        public void UpdateContact(string mobileNumber, Contact updatedContact)
        {
            if (contacts.Count == 0)
            {
                Console.WriteLine("No contacts available to update.");
                return;
            }

            var contact = contacts.Find(c => c.MobileNumber == mobileNumber);
            if (contact != null)
            {
                contact.FirstName = updatedContact.FirstName;
                contact.LastName = updatedContact.LastName;
                contact.Company = updatedContact.Company;
                contact.Email = updatedContact.Email;
                contact.Birthdate = updatedContact.Birthdate;
                contact.MobileNumber = updatedContact.MobileNumber;
                Console.WriteLine("Contact updated successfully.");
            }
            else
            {
                Console.WriteLine("Contact not found.");
            }
        }

        public void DeleteContact(string mobileNumber)
        {
            if (contacts.Count == 0)
            {
                Console.WriteLine("No contacts available to delete.");
                return;
            }

            var contact = contacts.Find(c => c.MobileNumber == mobileNumber);
            if (contact != null)
            {
                contacts.Remove(contact);
                Console.WriteLine("Contact deleted successfully.");
            }
            else
            {
                Console.WriteLine("Contact not found.");
            }
        }
    }
}