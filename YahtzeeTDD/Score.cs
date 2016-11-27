using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahtzeeTDD
{
    public class Score
    {
        public YahtzeeSet YahtzeeSet;
        public int? aces;
        public int? twos;

        public Score(YahtzeeSet yahtzeeSet)
        {
            YahtzeeSet = yahtzeeSet;
        }

        public bool saveAces()
        {
            if (aces != null)
            {
                return false;
            }

            int score = 0;
            foreach (Dice dice in YahtzeeSet.DiceSet)
            {
                if (dice.Number == 1)
                {
                    score += 1;
                }
            }

            aces = score;

            return true;
        }

        public bool saveTwos()
        {
            int score = 0;
            foreach (Dice dice in YahtzeeSet.DiceSet)
            {
                if (dice.Number == 2)
                {
                    score += 2;
                }
            }

            twos = score;

            return true;
        }
    }
}
