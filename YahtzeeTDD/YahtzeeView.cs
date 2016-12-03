using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YahtzeeTDD
{
    public class YahtzeeView
    {
        private YahtzeeSet YahtzeeSet;
        private Score Score;
        private UserConsole UserConsole;

        public YahtzeeView(YahtzeeSet yahtzeeSet, Score score, UserConsole userConsole)
        {
            YahtzeeSet = yahtzeeSet;
            Score = score;
            UserConsole = userConsole;
        }

        public virtual void ClearConsole()
        {
            UserConsole.Clear();
        }

        public virtual void ShowLogo()
        {
            UserConsole.WriteLine(Strings.LogoTop);
            UserConsole.WriteLine(Strings.LogoText);
            UserConsole.WriteLine(Strings.LogoBottom);
            UserConsole.WriteLine("");
        }

        public virtual void ShowView(CurrentView currentView)
        {
            throw new NotImplementedException();
        }

        public virtual string ReadInput()
        {
            throw new NotImplementedException();
        }

        public void ShowCommands(CurrentView currentView)
        {
            UserConsole.WriteLine(Strings.CommandsHeader);

            switch (currentView)
            {
                case CurrentView.Roll:
                case CurrentView.CheckScore:
                    UserConsole.WriteLine(Strings.PlayingCommands);
                    break;
                case CurrentView.SaveScore:
                    UserConsole.WriteLine(Strings.SaveScoreCommands);
                    break;
                case CurrentView.SaveDie:
                    UserConsole.WriteLine(Strings.SaveDieCommands);
                    break;
            }

            UserConsole.WriteLine(Strings.StandardCommands);
            UserConsole.WriteLine("");
        }

        public void ShowStartView()
        {
            UserConsole.WriteLine(Strings.StartView);
            UserConsole.WriteLine("");
        }

        public void ShowRollView()
        {
            UserConsole.WriteLine(Strings.RollView);
            UserConsole.WriteLine("");

            UserConsole.WriteLine(String.Format(Strings.CurrentRoll, YahtzeeSet.CurrentRoll.ToString()));

            PrintDice();

            UserConsole.WriteLine("");
        }

        public void ShowSaveDieView()
        {
            UserConsole.WriteLine(Strings.SaveDieView);
            UserConsole.WriteLine("");

            PrintDice();

            UserConsole.Write(new String(' ', Strings.Dice.Length));
            for (int i = 0; i < YahtzeeSet.DiceSet.Length; i += 1)
            {
                string commandString = String.Format("{0,-5}", YahtzeeSet.DiceSet[i].Saved ? "[" + i + "]" : "   ");
                if (i != YahtzeeSet.DiceSet.Length - 1)
                {
                    UserConsole.Write(commandString);
                }
                else
                {
                    UserConsole.WriteLine(commandString);
                }
            }

            UserConsole.WriteLine("");
        }

        private void PrintDice()
        {
            UserConsole.Write(new String(' ', Strings.Dice.Length));
            for (int i = 0; i < YahtzeeSet.DiceSet.Length; i += 1)
            {
                string savedString = String.Format("{0,-5}", YahtzeeSet.DiceSet[i].Saved ? "(S)" : "   ");
                if (i != YahtzeeSet.DiceSet.Length - 1)
                {
                    UserConsole.Write(savedString);
                }
                else
                {
                    UserConsole.WriteLine(savedString);
                }
            }

            UserConsole.Write(Strings.Dice);
            for (int i = 0; i < YahtzeeSet.DiceSet.Length; i += 1)
            {
                string diceString = String.Format("{0,1}{1,-4}", "", YahtzeeSet.DiceSet[i].Number.ToString());
                if (i != YahtzeeSet.DiceSet.Length - 1)
                {
                    UserConsole.Write(diceString);
                }
                else
                {
                    UserConsole.WriteLine(diceString);
                }
            }
        }

        public void ShowScore()
        {
            throw new NotImplementedException();
        }
    }
}
