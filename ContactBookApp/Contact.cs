using System;
using System.Text.RegularExpressions;

namespace ContactBookApp
{
    public class Contact
    {
        // Properties with safe defaults to avoid null warnings
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string MobileNumber { get; private set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime Birthdate { get; set; }

        // Validate and set mobile number
        public bool SetMobileNumber(string mobileNumber)
        {
            if (string.IsNullOrWhiteSpace(mobileNumber))
            {
                Console.WriteLine("❌ Mobile number cannot be empty.");
                return false;
            }

            // Simple validation: digits only, length 7–15
            if (!Regex.IsMatch(mobileNumber, @"^\d{7,15}$"))
            {
                Console.WriteLine("❌ Invalid mobile number format.");
                return false;
            }

            MobileNumber = mobileNumber;
            return true;
        }
    }
}