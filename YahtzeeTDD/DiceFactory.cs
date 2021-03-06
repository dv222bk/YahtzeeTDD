﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahtzeeTDD
{
    public class DiceFactory
    {
        Random Random;

        public DiceFactory(Random random)
        {
            Random = random;
        }

        public virtual Dice CreateDice()
        {
            return new Dice(Random);
        }
    }
}
