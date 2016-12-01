using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahtzeeTDD
{
    public class GameLogic
    {

        private YahtzeeSet YahtzeeSet;
        private Score Score;
        private YahtzeeView YahtzeeView;
        public State State = State.Start;
        public CurrentView CurrentView = CurrentView.Start;
        public bool continueGame = true;

        public GameLogic(YahtzeeSet yahtzeeSet, Score score, YahtzeeView yahtzeeView)
        {
            YahtzeeSet = yahtzeeSet;
            Score = score;
            YahtzeeView = yahtzeeView;
        }

        public void RollDices()
        {
            if (YahtzeeSet.CanRoll)
            {
                YahtzeeSet.RollUnsaved();

                if (YahtzeeSet.CanRoll)
                {
                    CurrentView = CurrentView.Roll;
                }
                else
                {
                    CurrentView = CurrentView.SaveScore;
                    State = State.Saving;
                }
            }
            else
            {
                CurrentView = CurrentView.SaveScore;
                State = State.Saving;
            }
        }

        public void NewGame()
        {
            YahtzeeSet.Reset();
            Score.Reset();
            CurrentView = CurrentView.Roll;
            State = State.Playing;
        }

        public void ReactToStandardInput(string input)
        {
            if (string.Equals(input, "Q", StringComparison.CurrentCultureIgnoreCase))
            {
                continueGame = false;
            }
            else if (string.Equals(input, "N", StringComparison.CurrentCultureIgnoreCase))
            {
                NewGame();
            }
        }

        public void ReactToPlayingInput(string input)
        {
            if (string.Equals(input, "C", StringComparison.CurrentCultureIgnoreCase))
            {
                CurrentView = CurrentView.CheckScore;
            }
            else if (string.Equals(input, "H", StringComparison.CurrentCultureIgnoreCase))
            {
                CurrentView = CurrentView.Roll;
            }
            else if (string.Equals(input, "R", StringComparison.CurrentCultureIgnoreCase))
            {
                RollDices();
            }
            else if (string.Equals(input, "A", StringComparison.CurrentCultureIgnoreCase))
            {
                CurrentView = CurrentView.SaveScore;
                State = State.Saving;
            }
            else if (string.Equals(input, "S", StringComparison.CurrentCultureIgnoreCase))
            {
                CurrentView = CurrentView.SaveDie;
                State = State.SaveDie;
            }
        }

        public void ReactToSaveDieInput(string input)
        {
            int intInput;
            if (int.TryParse(input, out intInput))
            {
                if (intInput >= 0 && intInput < YahtzeeSet.DiceSet.Length)
                {
                    YahtzeeSet.DiceSet[intInput].Saved ^= true;
                    CurrentView = CurrentView.Roll;
                    State = State.Playing;
                }
            }
        }

        public void ReactToSavingInput(string input)
        {
            int intInput;
            if (int.TryParse(input, out intInput))
            {
                if (SaveScore(intInput))
                {
                    if (!Score.IsFull)
                    {
                        YahtzeeSet.Reset();
                        CurrentView = CurrentView.Roll;
                        State = State.Playing;
                    }
                    else
                    {
                        CurrentView = CurrentView.Finish;
                        State = State.Start;
                    }
                }
            }
        }

        public bool SaveScore(int input)
        {
            switch (input)
            {
                case 1:
                    return Score.SaveAces();
                case 2:
                    return Score.SaveTwos();
                case 3:
                    return Score.SaveThrees();
                case 4:
                    return Score.SaveFours();
                case 5:
                    return Score.SaveFives();
                case 6:
                    return Score.SaveSixes();
                case 7:
                    return Score.SaveOnePair();
                case 8:
                    return Score.SaveTwoPair();
                case 9:
                    return Score.SaveToaK();
                case 10:
                    return Score.SaveFoaK();
                case 11:
                    return Score.SaveSmallStraight();
                case 12:
                    return Score.SaveLargeStraight();
                case 13:
                    return Score.SaveFullHouse();
                case 14:
                    return Score.SaveChance();
                case 15:
                    return Score.SaveYahtzee();
                default:
                    return false;
            }
        }
    }
}
