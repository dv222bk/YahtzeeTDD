using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahtzeeTDD
{
    class Program
    {
        static void Main(string[] args)
        {
            Dice[] dices = new Dice[5];
            Random random = new Random();
            for (int i = 0; i < 5; i += 1)
            {
                dices[i] = new Dice(random);
            }

            YahtzeeSet diceSet = new YahtzeeSet(dices);
            Score score = new Score(diceSet);
            GameLogic logic = new GameLogic(diceSet, score);
            UserConsole console = new UserConsole();
            YahtzeeView view = new YahtzeeView(diceSet, score, console);
            GameLoop gameLoop = new GameLoop(view, logic);

            gameLoop.Loop();
        }
    }
}
