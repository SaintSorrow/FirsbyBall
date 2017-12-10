using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FrisbyBall.Validations
{
    /// <summary>
    /// a static class containing a bunch of regex methods
    /// </summary>
    public static class Validation
    {
        /// <summary>
        /// checks if username input is valid
        /// </summary>
        /// <param name="_username">
        /// incoming username parameter
        /// </param>
        /// <returns>
        /// true - username macthes pattern
        /// false - username does not match pattern
        /// </returns>
        public static bool UsernamePatternMatch(string _username)
        {
            if (_username == "" || _username == null)
            {
                return false;
            }

            Regex regex = new Regex(@"^\w+$");
            if (regex.IsMatch(_username))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// checks if password contains valid symbols
        /// </summary>
        /// <param name="_password">
        /// incoming password to function
        /// </param>
        /// <returns>
        /// true - password matches pattern
        /// false - password does not match pattern
        /// </returns>
        public static bool PasswordPatternMatch(string _password)
        {
            if (_password == "" || _password == null)
            {
                return false;
            }

            Regex regex = new Regex(@"^\w+$");
            if (regex.IsMatch(_password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Method checking if email matches pattern 
        /// </summary>
        /// <param name="_email">
        /// incoming email string
        /// </param>
        /// <returns>
        /// true - email matches pattern
        /// false - email does not match the pattern
        /// </returns>
        public static bool EmailPatternMatch(string _email)
        {
            if (_email == "" || _email == null)
            {
                return false;
            }

            string EmailMatchPattern = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                     + "@"
                                     + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";

            Regex regex = new Regex(EmailMatchPattern);
            if (regex.IsMatch(_email))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
