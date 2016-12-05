using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YahtzeeTDD
{
    public class AnotherDiceSet
    {
        public Dice[] DiceSet;
        public DiceFactory DiceFactory;
        public const int MaxRolls = 3;
        public int CurrentRoll = 1;

        public AnotherDiceSet(DiceFactory diceFactory)
        {
            DiceFactory = diceFactory;
        }

        public void RollAll()
        {
            if (CurrentRoll != MaxRolls)
            {
                DiceSet = new Dice[5];
                for (int i = 0; i < DiceSet.Length; i += 1)
                {
                    DiceSet[i] = DiceFactory.CreateDice();
                }
                CurrentRoll += 1;
            }
        }

        public void UnsaveAll()
        {
            throw new NotImplementedException();
        }
    }
}
