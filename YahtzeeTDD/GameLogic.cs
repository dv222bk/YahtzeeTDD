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
                if (YahtzeeSet.CanThrow)
                {
                    YahtzeeSet.RollUnsaved();

                    if (YahtzeeSet.CanThrow)
                    {
                        CurrentView = CurrentView.Roll;
                    }
                } 
            }
        }
    }
}
