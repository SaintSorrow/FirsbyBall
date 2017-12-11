using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrisbyBall.Models;

namespace FrisbyBall.Views
{
    class StatisticsPageController
    {
        public static List<Match> UserMatches()
        {
            return Constants.userMatches;
        }
    }
}
