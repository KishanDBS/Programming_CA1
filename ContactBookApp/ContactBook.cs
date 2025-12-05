using System;
using System.Collections.Generic;

namespace ContactBookApp
{
    /// <summary>
    /// Manages a collection of Contact objects.
    /// Demonstrates object relationships (ContactBook "has-a" Contact).
    /// </summary>
    public class ContactBook
    {
        private List<Contact> contacts = new List<Contact>();

        /// <summary>
        /// Adds a new contact to the list.
        /// </summary>
        public void AddContact(Contact contact)
        {
            // Prevent duplicate mobile numbers
            foreach (var c in contacts)
            {
                if (c.MobileNumber == contact.MobileNumber)
                {
                    Console.WriteLine("A contact with this mobile number already exists.");
                    return;
                }
            }
            contacts.Add(contact);
            Console.WriteLine("Contact added successfully.");
        }

        /// <summary>
        /// Displays all contacts (handles edge cases: none, one, many).
        /// </summary>
        public void ShowAllContacts()
        {
            if (contacts.Count == 0)
            {
                Console.WriteLine("No contacts found. Please add a contact first.");
                return;
            }

            Console.WriteLine("--- All Contacts ---");
            int index = 1;
            foreach (var contact in contacts)
            {
                Console.WriteLine($"{index}: {contact.ShowDetails(false)}");
                index++;
            }
        }

        /// <summary>
        /// Finds and displays details of a contact by mobile number.
        /// </summary>
        public void ShowContactDetails(string mobileNumber)
        {
            var contact = contacts.Find(c => c.MobileNumber == mobileNumber);
            if (contact != null)
                Console.WriteLine(contact.ShowDetails(true));
            else
                Console.WriteLine("Contact not found.");
        }

        /// <summary>
        /// Updates a contact’s details.
        /// </summary>
        public void UpdateContact(string mobileNumber, Contact updatedContact)
        {
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

        /// <summary>
        /// Deletes a contact by mobile number.
        /// </summary>
        public void DeleteContact(string mobileNumber)
        {
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