using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahtzeeTDD
{
    public class Dice
    {
        private Random random;
        private int? _number;

        public Dice(Random random)
        {
            this.random = random;
        }

        public void Roll()
        {
            Number = random.Next(1, 6);
        }

        public int? Number
        {
            get
            {
                if (_number == null)
                {
                    Roll();
                }
                return _number;
            }
            set
            {
                if (value < 1 || value > 6)
                {
                    throw new ArgumentOutOfRangeException(Strings.DiceNumberOutOfRange);
                }

                _number = value;
            }
        }
    }
}
