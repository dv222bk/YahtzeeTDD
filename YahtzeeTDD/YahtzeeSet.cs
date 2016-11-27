using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YahtzeeTDD
{
    public class YahtzeeSet
    {
        public Dice[] DiceSet;
        public const int MaxRolls = 3;
        public int CurrentRoll = 1;

        public YahtzeeSet(Dice[] diceSet)
        {
            DiceSet = diceSet;
        }

        public void RollAll()
        {
            if (CurrentRoll <= MaxRolls)
            {
                UnsaveAll();
                foreach (Dice dice in DiceSet)
                {
                    dice.Roll();
                }
                CurrentRoll += 1;
            } 
        }

        public void UnsaveAll()
        {
            foreach (Dice dice in DiceSet)
            {
                dice.Saved = false;
            }
        }

        public void RollUnsaved()
        {
            foreach (Dice dice in DiceSet)
            {
                if (!dice.Saved)
                {
                    dice.Roll();
                }
            }
        }
    }
}
