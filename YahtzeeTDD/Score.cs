﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public int? onePair;
        public int? twoPair;
        public int? toak; // two of a kind
        public object foak;

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

        private Dice[] sortDices()
        {
            Dice[] sortedDice = (Dice[])YahtzeeSet.DiceSet.Clone();
            Array.Sort<Dice>(sortedDice,
                    new Comparison<Dice>(
                            (d1, d2) => d1.Number.Value.CompareTo(d2.Number.Value)
                    ));
            return sortedDice;
        }

        private int findPairScore(int pairs)
        {
            Dice[] sortedDice = sortDices();
            int score = 0;
            int foundPairs = 0;
            for (int i = sortedDice.Length - 1; i > 0; i -= 1)
            {
                if (sortedDice[i].Number == sortedDice[i - 1].Number)
                {
                    score += (int)sortedDice[i].Number * 2;

                    foundPairs += 1;

                    if (foundPairs == pairs)
                    {
                        break;
                    }

                    i -= 1;
                }
            }

            return foundPairs == pairs ? score : 0;
        }

        private int findXOfAKindScore(int amount)
        {
            Dice[] sortedDice = sortDices();
            int score = 0;
            for (int i = sortedDice.Length - 1; i >= amount - 1; i -= 1)
            {
                for (int k = 1; k <= i && k < amount; k += 1)
                {
                    if (sortedDice[i].Number != sortedDice[i - k].Number)
                    {
                        score = 0;
                        break;
                    }
                    score += (int)sortedDice[i].Number;
                }
                if (score != 0)
                {
                    score += (int)sortedDice[i].Number;
                    break;
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
                return Convert.ToInt32(upperScores.Sum()) + Convert.ToInt32(UpperBonus);
            }
        }

        public bool saveOnePair()
        {
            if (onePair != null)
            {
                return false;
            }

            onePair = findPairScore(1);

            return true;
        }

        public bool saveTwoPair()
        {
            if (twoPair != null)
            {
                return false;
            }

            twoPair = findPairScore(2);

            return true;
        }

        public bool saveToaK()
        {
            if (toak != null)
            {
                return false;
            }

            toak = findXOfAKindScore(3);

            return true;
        }

        public bool saveFoaK()
        {
            throw new NotImplementedException();
        }
    }
}
