using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahtzeeTDD
{
    public class UserConsole
    {
        public virtual string ReadLine()
        {
            return Console.ReadLine();
        }

        public virtual void Write(string output)
        {
            Console.Write(output);
        }

        public virtual void WriteLine(string output)
        {
            Console.WriteLine(output);
        }

        public virtual void Clear()
        {
            Console.Clear();
        }
    }
}
