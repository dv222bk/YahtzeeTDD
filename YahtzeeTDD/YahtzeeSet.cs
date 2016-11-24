using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YahtzeeTDD
{
    public class YahtzeeSet
    {
        public Dice[] DiceSet;

        public YahtzeeSet(Dice[] diceSet)
        {
            DiceSet = diceSet;
        }

        public void RollAll()
        {
            foreach (Dice dice in DiceSet)
            {
                dice.Roll();
            }
        }

        public void UnsaveAll()
        {
            foreach (Dice dice in DiceSet)
            {
                dice.Saved = false;
            }
        }
    }
}
