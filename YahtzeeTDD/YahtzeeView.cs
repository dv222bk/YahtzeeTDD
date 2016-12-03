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
            UserConsole.WriteLine("");
        }

        public virtual void ShowView(CurrentView currentView)
        {
            throw new NotImplementedException();
        }

        public virtual string ReadInput()
        {
            throw new NotImplementedException();
        }

        public void ShowCommands(CurrentView currentView)
        {
            UserConsole.WriteLine(Strings.CommandsHeader);

            switch (currentView)
            {
                case CurrentView.Roll:
                case CurrentView.CheckScore:
                    UserConsole.WriteLine(Strings.PlayingCommands);
                    break;
                case CurrentView.SaveScore:
                    UserConsole.WriteLine(Strings.SaveScoreCommands);
                    break;
                case CurrentView.SaveDie:
                    UserConsole.WriteLine(Strings.SaveDieCommands);
                    break;
            }

            UserConsole.WriteLine(Strings.StandardCommands);
            UserConsole.WriteLine("");
        }
    }
}
