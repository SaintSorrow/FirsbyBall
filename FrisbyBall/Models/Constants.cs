using FrisbyBall.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FrisbyBall
{
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
	}
}

