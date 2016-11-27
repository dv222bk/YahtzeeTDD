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
        public int? threes;
        public int? fours;
        public int? fives;
        public object sixes;

        public Score(YahtzeeSet yahtzeeSet)
        {
            YahtzeeSet = yahtzeeSet;
        }

        private int countDiceNumberScore(int number)
        {
            int score = 0;
            foreach (Dice dice in YahtzeeSet.DiceSet)
            {
                if (dice.Number == number)
                {
                    score += number;
                }
            }
            return score;
        }

        public bool saveAces()
        {
            if (aces != null)
            {
                return false;
            }

            aces = countDiceNumberScore(1);

            return true;
        }

        public bool saveTwos()
        {
            if (twos != null)
            {
                return false;
            }

            twos = countDiceNumberScore(2);

            return true;
        }

        public bool saveThrees()
        {
            if (threes != null)
            {
                return false;
            }

            threes = countDiceNumberScore(3);

            return true;
        }

        public bool saveFours()
        {
            if (fours != null)
            {
                return false;
            }

            fours = countDiceNumberScore(4);

            return true;
        }

        public bool saveFives()
        {
            if (fives != null)
            {
                return false;
            }

            fives = countDiceNumberScore(5);

            return true;
        }

        public bool saveSixes()
        {
            throw new NotImplementedException();
        }
    }
}
