using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahtzeeTDD
{
    public class Dice
    {
        private Random Random;
        private int? _number;

        public Dice(Random random)
        {
            Random = random;
        }

        public virtual void Roll()
        {
            Number = Random.Next(1, 7);
        }

        public virtual int? Number
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
                if (value < 1 || value > 6 || value == null)
                {
                    throw new ArgumentOutOfRangeException(Strings.DiceNumberOutOfRange);
                }

                _number = value;
            }
        }

        public virtual bool Saved { get; set; }
    }
}
