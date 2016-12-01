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
            if (State == State.Playing)
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
        }

        public void NewGame()
        {
            YahtzeeSet.Reset();
            Score.Reset();
            CurrentView = CurrentView.Roll;
            State = State.Playing;
        }

        public void ShowScore()
        {
            CurrentView = CurrentView.CheckScore;
        }

        public void Quit()
        {
            throw new NotImplementedException();
        }
    }
}
