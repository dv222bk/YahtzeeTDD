using System;
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
        public int? toak; // three of a kind
        public int? foak; // four of a kind
        public int? smallStraight;
        public int? largeStraight;
        public int? fullHouse;
        public int? chance;
        public int? yahtzee;

        public Score(YahtzeeSet yahtzeeSet)
        {
            YahtzeeSet = yahtzeeSet;
        }

        private int CountDiceNumberScore(int number)
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

        private Dice[] SortDices()
        {
            Dice[] sortedDice = (Dice[])YahtzeeSet.DiceSet.Clone();
            Array.Sort<Dice>(sortedDice,
                    new Comparison<Dice>(
                            (d1, d2) => d1.Number.Value.CompareTo(d2.Number.Value)
                    ));
            return sortedDice;
        }

        private int FindPairScore(int pairs)
        {
            Dice[] sortedDice = SortDices();
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

        private int FindXOfAKindScore(int amount)
        {
            Dice[] sortedDice = SortDices();
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

        private bool CheckForStraight(int start)
        {
            Dice[] sortedDice = SortDices();
            for (int i = 0; i < sortedDice.Length - 1; i += 1)
            {
                if (
                    (i == 0 && sortedDice[i].Number != start) || 
                    (sortedDice[i].Number != sortedDice[i + 1].Number - 1)
                   )
                {
                    return false;
                }
            }

            return true;
        }

        public bool SaveAces()
        {
            if (aces != null)
            {
                return false;
            }

            aces = CountDiceNumberScore(1);

            return true;
        }

        public bool SaveTwos()
        {
            if (twos != null)
            {
                return false;
            }

            twos = CountDiceNumberScore(2);

            return true;
        }

        public bool SaveThrees()
        {
            if (threes != null)
            {
                return false;
            }

            threes = CountDiceNumberScore(3);

            return true;
        }

        public bool SaveFours()
        {
            if (fours != null)
            {
                return false;
            }

            fours = CountDiceNumberScore(4);

            return true;
        }

        public bool SaveFives()
        {
            if (fives != null)
            {
                return false;
            }

            fives = CountDiceNumberScore(5);

            return true;
        }

        public bool SaveSixes()
        {
            if (sixes != null)
            {
                return false;
            }

            sixes = CountDiceNumberScore(6);

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

        public bool SaveOnePair()
        {
            if (onePair != null)
            {
                return false;
            }

            onePair = FindPairScore(1);

            return true;
        }

        public bool SaveTwoPair()
        {
            if (twoPair != null)
            {
                return false;
            }

            twoPair = FindPairScore(2);

            return true;
        }

        public bool SaveToaK()
        {
            if (toak != null)
            {
                return false;
            }

            toak = FindXOfAKindScore(3);

            return true;
        }

        public bool SaveFoaK()
        {
            if (foak != null)
            {
                return false;
            }

            foak = FindXOfAKindScore(4);

            return true;
        }

        public bool SaveSmallStraight()
        {
            if (smallStraight != null)
            {
                return false;
            }

            smallStraight = CheckForStraight(1) ? 15 : 0;

            return true;
        }

        public bool SaveLargeStraight()
        {
            if (largeStraight != null)
            {
                return false;
            }

            largeStraight = CheckForStraight(2) ? 20 : 0;

            return true;
        }

        public bool SaveFullHouse()
        {
            if (fullHouse != null)
            {
                return false;
            }

            Dice[] diceSet = (Dice[])YahtzeeSet.DiceSet.Clone();
            var numberGroups = diceSet.GroupBy(dice => dice.Number);

            int score = 0;
            if (numberGroups.Count() == 2)
            {
                foreach (var number in numberGroups)
                {
                    score += (int)(number.Key * number.Count());
                }
            }

            fullHouse = score;

            return true;
        }

        public bool SaveChance()
        {
            if (chance != null)
            {
                return false;
            }

            chance = YahtzeeSet.DiceSet.Sum(dice => dice.Number);

            return true;
        }

        public bool SaveYahtzee()
        {
            if (yahtzee != null)
            {
                return false;
            }

            yahtzee = FindXOfAKindScore(YahtzeeSet.DiceSet.Length) > 0 ? 50 : 0;

            return true;
        }

        public int TotalScore {
            get
            {
                int?[] totalScores = new int?[] { UpperScore, onePair, twoPair, toak, foak, smallStraight, 
                                                largeStraight, fullHouse, chance, yahtzee };
                return Convert.ToInt32(totalScores.Sum());
            }
        }

        public virtual void Reset()
        {
            var fields = this.GetType().GetFields();

            foreach (var field in fields)
            {
                if (field.FieldType == typeof(Nullable<Int32>))
                {
                    field.SetValue(this, null);
                }
            }
        }
    }
}
