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
        private UserConsole UserConsole;

        public GameLogic(YahtzeeSet yahtzeeSet, Score score, YahtzeeView yahtzeeView, UserConsole userConsole)
        {
            YahtzeeSet = yahtzeeSet;
            Score = score;
            YahtzeeView = yahtzeeView;
            UserConsole = userConsole;
        }
    }
}
