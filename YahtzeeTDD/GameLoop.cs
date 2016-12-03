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
            do
            {
                YahtzeeView.ClearConsole();
                YahtzeeView.ShowLogo();
                YahtzeeView.ShowView(GameLogic.CurrentView);
                YahtzeeView.ShowCommands(GameLogic.CurrentView);

                string input = YahtzeeView.ReadInput();
                GameLogic.ReactToStandardInput(input);
                switch (GameLogic.State)
                {
                    case State.Playing:
                        GameLogic.ReactToPlayingInput(input);
                        break;
                    case State.Saving:
                        GameLogic.ReactToSavingInput(input);
                        break;
                    case State.SaveDie:
                        GameLogic.ReactToSaveDieInput(input);
                        break;
                }
            } while (GameLogic.continueGame);
        }
    }
}
