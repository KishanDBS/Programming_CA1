using System;

namespace ContactBookApp
{
    /// <summary>
    /// Represents a single contact with personal details.
    /// Demonstrates encapsulation via private fields and public properties.
    /// </summary>
    public class Contact
    {
        // Private backing field for mobile number
        private string mobileNumber;

        // Public properties (auto-implemented)
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }

        /// <summary>
        /// Mobile number property with validation:
        /// - Must be exactly 9 digits
        /// - Must not start with 0
        /// - Must be numeric and positive
        /// If invalid, throws ArgumentException (caught in Program.cs so app does not crash).
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
                    // Throwing exception here ensures invalid data never gets stored.
                    // Program.cs catches this and shows a friendly error message.
                    throw new ArgumentException("Invalid mobile number. Must be 9 digits, not starting with 0.");
                }
            }
        }

        /// <summary>
        /// Constructor: initializes a new Contact object.
        /// Validation occurs automatically when setting MobileNumber.
        /// </summary>
        public Contact(string firstName, string lastName, string company, string mobileNumber, string email, DateTime birthdate)
        {
            FirstName = firstName;
            LastName = lastName;
            Company = company;
            MobileNumber = mobileNumber; // validation happens here
            Email = email;
            Birthdate = birthdate;
        }

        /// <summary>
        /// Overloaded method: shows either summary or full details.
        /// </summary>
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