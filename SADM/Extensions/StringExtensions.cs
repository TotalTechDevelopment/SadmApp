using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SADM.Extensions
{
    /// <summary>
    /// String extensions.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Ises the email.
        /// </summary>
        /// <returns><c>true</c>, if email was ised, <c>false</c> otherwise.</returns>
        /// <param name="email">Email.</param>
        public static bool IsEmail(this string email)
        {
            return email.IsValid(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        }

        /// <summary>
        /// Ises the valid password.
        /// </summary>
        /// <returns><c>true</c>, if valid password was ised, <c>false</c> otherwise.</returns>
        /// <param name="password">Password.</param>
        public static bool IsPassword(this string password)
        {
            return password != null;//password.IsValid(@"^[A-Za-z]+\d+.*$");
        }

        /// <summary>
        /// Ises the name.
        /// </summary>
        /// <returns><c>true</c>, if name was ised, <c>false</c> otherwise.</returns>
        /// <param name="name">Name.</param>
        public static bool IsName(this string name)
        {
            return name.IsValid(@"^[A-Za-z ]+.*$");
        }

        /// <summary>
        /// Ises the phone.
        /// </summary>
        /// <returns><c>true</c>, if phone was ised, <c>false</c> otherwise.</returns>
        /// <param name="phone">Phone.</param>
        public static bool IsPhone(this string phone)
        {
            return phone.IsValid(@"\+?[0-9]{10}");
        }

        /// <summary>
        /// Ises the zip code.
        /// </summary>
        /// <returns><c>true</c>, if zip code was ised, <c>false</c> otherwise.</returns>
        /// <param name="zipCode">Zip code.</param>
        public static bool IsZipCode(this string zipCode)
        {
            return zipCode.IsValid(@"\+?[0-9]{5}");
        }

        public static bool IsNir(this string nis)
        {
            return nis.IsValid(@"[0-9]{18}$");
        }

        public static bool IsPreviousReading(this string name)
        {
            return name.IsValid(@"\+?[0-9]{4}");
        }

        /// <summary>
        /// Ises the valid.
        /// </summary>
        /// <returns><c>true</c>, if valid was ised, <c>false</c> otherwise.</returns>
        /// <param name="str">String.</param>
        /// <param name="regex">Regex.</param>
        public static bool IsValid(this string str, string regex)
        {
            return str != null && regex != null && Regex.IsMatch(str, regex);
        }

        public static bool IsIntegerNumber(this string str)
        {
            return str.IsValid(@"^[0-9]*$");
        }

        public static bool IsCardNumber(this string str)
        {
            return str.IsValid(@"[0-9]{16}$");
        }

        public static IEnumerable<string> GetCardHolderErrorList(this string cardHolder)
        {
            var errorList = new List<string>();
            if(string.IsNullOrWhiteSpace(cardHolder))
            {
                errorList.Add(AppResources.CardHolderEmpty);
            }
            else if (!cardHolder.IsValid(@"^[A-Za-z ]+.*$"))
            {
                errorList.Add(AppResources.CardHolderInvalid);
            }
            return errorList;
        }

        public static IEnumerable<string> GetCardNumberErrorList(this string cardNumber)
        {
            var errorList = new List<string>();
            if (string.IsNullOrWhiteSpace(cardNumber))
            {
                errorList.Add(AppResources.CardNumberEmpty);
            }
            else if (!cardNumber.IsValid(@"[0-9]{16}$"))
            {
                errorList.Add(AppResources.CardNumberInvalid);
            }
            return errorList;
        }

        public static IEnumerable<string> GetCvvErrorList(this string cvv)
        {
            var errorList = new List<string>();
            if (string.IsNullOrWhiteSpace(cvv))
            {
                errorList.Add(AppResources.CvvEmpty);
            }
            else if (!cvv.IsValid(@"[0-9]{3}$"))
            {
                errorList.Add(AppResources.CvvInvalid);
            }
            return errorList;
        }

        public static string GetEmailError(this string email)
        {
            string error;
            if (string.IsNullOrWhiteSpace(email))
            {
                error = AppResources.EmailEmpty;
            }
            else if (!email.IsEmail())
            {
                error = AppResources.EmailInvalid;
            }
            else
            {
                error = null;
            }
            return error;
        }

        public static string GetPasswordError(this string password)
        {
            string error;
            if (string.IsNullOrWhiteSpace(password))
            {
                error = AppResources.PasswordEmpty;
            }
            else if (!password.IsPassword())
            {
                error = AppResources.PasswordInvalid;
            }
            else
            {
                error = null;
            }
            return error;
        }

        public static string GetNirError(this string nir)
        {
            string error;
            if (string.IsNullOrWhiteSpace(nir))
            {
                error = AppResources.NirRequired;
            }
            else if (!nir.IsNir())
            {
                error = AppResources.NisInvalid;
            }
            else
            {
                error = null;
            }
            return error;
        }

        public static string GetPreviousReadingError(this string previousReading)
        {
            string error;
            if (string.IsNullOrWhiteSpace(previousReading))
            {
                error = AppResources.PreviousReadingEmpty;
            }
            else if (!previousReading.IsPreviousReading())
            {
                error = AppResources.PreviousReadingInvalid;
            }
            else
            {
                error = null;
            }
            return error;
        }
    }
}
