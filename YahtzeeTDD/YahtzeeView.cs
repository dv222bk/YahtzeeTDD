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

        private void WriteScoreString(string format, int commandNumber, int? score, bool pad = true)
        {
            if (pad && commandNumber != 0)
            {
                UserConsole.WriteLine(String.Format(format, "", 
                    score == null ? "[" + commandNumber + "]" : commandNumber.ToString(),
                    score.ToString()));
            }
            else if (!pad)
            {
                UserConsole.WriteLine(String.Format(format,
                    score == null ? "[" + commandNumber + "]" : commandNumber.ToString(),
                    score.ToString()));
            }
            else
            {
                UserConsole.WriteLine(String.Format(format, "", score.ToString()));
            }
        }

        public void ShowScore()
        {
            UserConsole.WriteLine(String.Format(Strings.ScoreHeader, ""));
            UserConsole.WriteLine(Strings.ScoreLine);
            WriteScoreString(Strings.Aces, 1, Score.aces);
            UserConsole.WriteLine(Strings.ScoreLine);
            WriteScoreString(Strings.Twos, 2, Score.twos);
            UserConsole.WriteLine(Strings.ScoreLine);
            WriteScoreString(Strings.Threes, 3, Score.threes);
            UserConsole.WriteLine(Strings.ScoreLine);
            WriteScoreString(Strings.Fours, 4, Score.fours);
            UserConsole.WriteLine(Strings.ScoreLine);
            WriteScoreString(Strings.Fives, 5, Score.fives);
            UserConsole.WriteLine(Strings.ScoreLine);
            WriteScoreString(Strings.Sixes, 6, Score.sixes);
            UserConsole.WriteLine(Strings.ScoreLine);
            WriteScoreString(Strings.Bonus, 0, Score.UpperBonus);
            UserConsole.WriteLine(Strings.ScoreLine);
            WriteScoreString(Strings.UpperScore, 0, Score.UpperScore);
            UserConsole.WriteLine(Strings.ScoreLine);
            WriteScoreString(Strings.OnePair, 7, Score.onePair);
            UserConsole.WriteLine(Strings.ScoreLine);
            WriteScoreString(Strings.TwoPair, 8, Score.twoPair);
            UserConsole.WriteLine(Strings.ScoreLine);
            WriteScoreString(Strings.ToaK, 9, Score.toak, false);
            UserConsole.WriteLine(Strings.ScoreLine);
            WriteScoreString(Strings.FoaK, 10, Score.foak, false);
            UserConsole.WriteLine(Strings.ScoreLine);
            WriteScoreString(Strings.SmallStraight, 11, Score.smallStraight, false);
            UserConsole.WriteLine(Strings.ScoreLine);
            WriteScoreString(Strings.LargeStraight, 12, Score.largeStraight, false);
            UserConsole.WriteLine(Strings.ScoreLine);
            WriteScoreString(Strings.FullHouse, 13, Score.fullHouse);
            UserConsole.WriteLine(Strings.ScoreLine);
            WriteScoreString(Strings.Chance, 14, Score.chance);
            UserConsole.WriteLine(Strings.ScoreLine);
            WriteScoreString(Strings.Yahtzee, 15, Score.yahtzee);
            UserConsole.WriteLine(Strings.ScoreLine);
            WriteScoreString(Strings.TotalScore, 0, Score.TotalScore);
            UserConsole.WriteLine(Strings.ScoreLine);
            UserConsole.WriteLine("");
        }
    }
}
