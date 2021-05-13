using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SEDOL_CaseStudy
{
    /// <summary>
    /// Class implementing SEDOL validator interface
    /// </summary>
    public class SedolValidator : ISedolValidator
    {
        /// <summary>
        /// Check if the input string is a valid SEDOL 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ISedolValidationResult ValidateSedol(string input)
        {
            string validationdesc;
            bool isUserdefined = false;
            bool isValidChecksum = false;

            validationdesc = ValidateString(input);
            
            ////Validate Checksum only if the string is a valid SEDOL
            ////Check the string is userdefined only if its a valid string
            if (validationdesc == Constants.Valid)
            {
                isUserdefined = IsUserDefinedSEDOL(input);
                isValidChecksum = ValidateChecksum(input);
                if (!isValidChecksum)
                    validationdesc = Constants.InvalidChecksum;
            }

            var result = new SedolValidationResult(input, isValidChecksum, isUserdefined, validationdesc);
            return result;
        }

        /// <summary>
        /// This method is used to validate the checksum if it matches the checkdigit
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static bool ValidateChecksum(string input)
        {
            int sum = 0;
            int checksum = 0;
            int checkDigit = -1;

            ////If the last digit is an alphabet means the value is greater than 9, which means it cannot be a SEDOL 
            ////As per the Check digit logic
            ////The check digit is then calculated by:
            ////[10 - (checksum modulo 10)] modulo 10

            if (Regex.IsMatch(input, @"$\d"))
                return false;

            char[] ch = new char[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                ch[i] = input[i];
            }
            for (int i = 0; i < ch.Length; i++)
            {
                int n;
                if (Char.IsDigit(ch[i]))
                    n = (int)Char.GetNumericValue(ch[i]);
                else
                    n = char.ToUpper(ch[i]) - 55;

                switch (i + 1)
                {
                    case 1:
                    case 3:
                        sum += n * 1;
                        break;
                    case 2:
                    case 5:
                        sum += n * 3;
                        break;
                    case 4:
                        sum += n * 7;
                        break;
                    case 6:
                        sum += n * 9;
                        break;
                    case 7:
                        checkDigit = n;
                        break;
                }
            }

            checksum = (10 - (sum % 10)) % 10;
            if (checksum == checkDigit)
                return true;
            else
                return false;
        }

        /// <summary>
        /// This method is used to ensure the input string is a valid string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string ValidateString(string input)
        {
            if (String.IsNullOrEmpty(input) || input.Length != 7)
                return Constants.InvalidInputLenght;
            else if (!Regex.IsMatch(input, @"^[a-zA-Z0-9]*$"))
                return Constants.InvalidInputChar;
            else
                return Constants.Valid;
        }

        /// <summary>
        /// This method is used to check if the input string is a user defined SEDOL or not
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static bool IsUserDefinedSEDOL(string input)
        {
            if (Regex.IsMatch(input, @"^9"))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Using these constant values for ValidationDescription
        /// </summary>
        private static class Constants
        {
            public static readonly string InvalidInputLenght = "Input string was not 7-characters long";
            public static readonly string InvalidInputChar = "SEDOL contains invalid characters";
            public static readonly string InvalidChecksum = "Checksum digit does not agree with the rest of the input";
            public static readonly string Valid = "Null";
        }
    }
}