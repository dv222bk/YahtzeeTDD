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
        public int Number;

        public Dice(Random random)
        {
            this.random = random;
        }

        public void Roll()
        {
            Number = random.Next(1, 6);
        }
    }
}
