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

        public Dice(Random random)
        {
            this.random = random;
        }

        public void Roll()
        {
            random.Next(1, 6);
        }
    }
}
