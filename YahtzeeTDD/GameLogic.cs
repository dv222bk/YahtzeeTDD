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
        private State State = State.Start;
        private CurrentView CurrentView = CurrentView.Start;

        public GameLogic(YahtzeeSet yahtzeeSet, Score score, YahtzeeView yahtzeeView)
        {
            YahtzeeSet = yahtzeeSet;
            Score = score;
            YahtzeeView = yahtzeeView;
        }
    }
}
