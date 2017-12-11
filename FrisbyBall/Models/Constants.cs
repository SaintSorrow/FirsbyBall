using FrisbyBall.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Text.RegularExpressions;

namespace FrisbyBall
{
    /// <summary>
    /// class for static variables which are used in application
    /// </summary>
	public static class Constants
	{
		// Replace strings with your Azure Mobile App endpoint.
		public static string ApplicationURL = @"https://frisbyball.azurewebsites.net";

        public static Color BackgroundColor = Color.FromRgb(0, 94, 51);
        public static Color MainTextColor = Color.FromRgb(190,222,186) ;
        public static int LoginIconHeight = 120;
        public static int MaxGoalLimit = 10;

        public static User opponent;
        public static User LocalUser;
        //public static User selectedUser;
        public static List<Match> userMatches;
        public static int GoalLimit = 10;
        public static List<User> userList;
        public static List<User> opponentList;
        public static string EmailMatchPattern = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                      + "@"
                      + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";
        public static Regex regex = new Regex(@"^\w+$");
	}
}

