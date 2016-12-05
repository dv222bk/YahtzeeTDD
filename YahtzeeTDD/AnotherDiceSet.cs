using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YahtzeeTDD
{
    public class AnotherDiceSet
    {
        public Dice[] diceset;
        public DiceFactory DiceFactory;

        public AnotherDiceSet(DiceFactory diceFactory)
        {
            DiceFactory = diceFactory;
        }

        public void RollAll()
        {
            throw new NotImplementedException();
        }
    }
}
