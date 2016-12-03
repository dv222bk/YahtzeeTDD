using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YahtzeeTDD
{
    public class YahtzeeView
    {
        private YahtzeeSet YahtzeeSet;
        private Score Score;
        private UserConsole UserConsole;

        public YahtzeeView(YahtzeeSet yahtzeeSet, Score score, UserConsole userConsole)
        {
            YahtzeeSet = yahtzeeSet;
            Score = score;
            UserConsole = userConsole;
        }

        public virtual void ClearConsole()
        {
            UserConsole.Clear();
        }

        public virtual void ShowLogo()
        {
            UserConsole.WriteLine(Strings.LogoTop);
            UserConsole.WriteLine(Strings.LogoText);
            UserConsole.WriteLine(Strings.LogoBottom);
        }

        public virtual void ShowView(CurrentView currentView)
        {
            throw new NotImplementedException();
        }

        public virtual string ReadInput()
        {
            throw new NotImplementedException();
        }
    }
}
