using FrisbyBall.Managers;
using FrisbyBall.Models;
using System;

namespace FrisbyBall.Controlers
{
    public class MatchPageController
    {
        private MatchManager matchManager;
        private UserManager userManager;
        private User win;
        private User lost;

        public MatchPageController()
        {
            matchManager = MatchManager.DefaultManager;
            userManager = UserManager.DefaultManager;
        }

        public bool HomeGoalProceudre()
        {
            try
            {
                Constants.homeGoalCount++;
                if (Constants.homeGoalCount >= Constants.GoalLimit)
                {
                    MatchPlayed();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }


        public bool AwayGoalProcedure()
        {
            try
            {
                Constants.awayGoalCount++;

                if (Constants.awayGoalCount >= Constants.GoalLimit)
                {
                    MatchPlayed();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public async void MatchPlayed()
        {
            try
            {
                int winP;
                int loseP;
                if (Constants.homeGoalCount > Constants.awayGoalCount)
                {
                    Constants.LocalUser.Wins++;
                    Constants.opponent.Loses++;
                    win = Constants.LocalUser;
                    lost = Constants.opponent;
                    winP = Constants.homeGoalCount;
                    loseP = Constants.awayGoalCount;
                }
                else
                {
                    Constants.LocalUser.Loses++;
                    Constants.opponent.Wins++;
                    win = Constants.opponent;
                    lost = Constants.LocalUser;
                    winP = Constants.awayGoalCount;
                    loseP = Constants.homeGoalCount;
                }

                await userManager.SaveUserAsync(Constants.LocalUser);
                await userManager.SaveUserAsync(Constants.opponent);

                Match match = new Match
                {
                    PlayerWon = win.UserName,
                    PlayerLost = lost.UserName,
                    WinPoints = winP,
                    LostPoints = loseP
                };

                Constants.homeGoalCount = 0;
                Constants.awayGoalCount = 0;
                Constants.opponent = null;

                await matchManager.SaveMatchAsync(match);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }
}
