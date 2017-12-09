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
