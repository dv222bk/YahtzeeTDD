﻿using System;
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
    }
}