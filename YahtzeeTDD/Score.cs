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
        public int? sixes;

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
            if (sixes != null)
            {
                return false;
            }

            sixes = countDiceNumberScore(6);

            return true;
        }

        public int? UpperBonus 
        {
            get
            {
                int?[] upperScores = new int?[] { aces, twos, threes, fours, fives, sixes };
                if (upperScores.Sum() >= 63)
                {
                    return 50;
                }
                return null;
            }
        }

        public int UpperScore {
            get
            {
                int?[] upperScores = new int?[] { aces, twos, threes, fours, fives, sixes };
                return (int)(upperScores.Sum() + UpperBonus);
            }
        }
    }
}
