using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahtzeeTDD
{
    public class GameLoop
    {
        YahtzeeView YahtzeeView;
        GameLogic GameLogic;

        public GameLoop(YahtzeeView yahtzeeView, GameLogic gameLogic)
        {
            YahtzeeView = yahtzeeView;
            GameLogic = gameLogic;
        }

        public void Loop()
        {
            throw new NotImplementedException();
        }
    }
}
