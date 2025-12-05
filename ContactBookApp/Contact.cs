using System;

namespace ContactBookApp
{
    /// <summary>
    /// Represents a single contact with personal details.
    /// Demonstrates encapsulation via private fields and public properties.
    /// </summary>
    public class Contact
    {
        // Private fields
        private string mobileNumber;

        // Public properties with validation
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }

        /// <summary>
        /// Mobile number property with validation:
        /// - Must be 9 digits
        /// - Must not start with 0
        /// - Must be positive
        /// </summary>
        public string MobileNumber
        {
            get { return mobileNumber; }
            set
            {
                if (value.Length == 9 && long.TryParse(value, out long num) && num > 0 && !value.StartsWith("0"))
                {
                    mobileNumber = value;
                }
                else
                {
                    throw new ArgumentException("Invalid mobile number. Must be a 9-digit positive number not starting with 0.");
                }
            }
        }

        // Constructor
        public Contact(string firstName, string lastName, string company, string mobileNumber, string email, DateTime birthdate)
        {
            FirstName = firstName;
            LastName = lastName;
            Company = company;
            MobileNumber = mobileNumber; // validation occurs here
            Email = email;
            Birthdate = birthdate;
        }

        // Overloaded method example: Show contact summary vs full details
        public string ShowDetails()
        {
            return $"{FirstName} {LastName}, {Company}, {MobileNumber}, {Email}, {Birthdate.ToShortDateString()}";
        }

        public string ShowDetails(bool full)
        {
            if (full)
                return $"Name: {FirstName} {LastName}\nCompany: {Company}\nMobile: {MobileNumber}\nEmail: {Email}\nBirthdate: {Birthdate.ToShortDateString()}";
            else
                return $"{FirstName} {LastName} - {MobileNumber}";
        }
    }
}