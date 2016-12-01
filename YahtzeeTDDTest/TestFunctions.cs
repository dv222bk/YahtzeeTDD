using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using YahtzeeTDD;

namespace YahtzeeTDDTest
{
    public class TestFunctions
    {
        public void SetupMockDice(Mock<Dice>[] dices, int[] diceValues)
        {
            for (int i = 0; i < 5; i += 1)
            {
                dices[i].SetupGet(m => m.Number).Returns(diceValues[i]);
            }
        }

        public void FillMockScore(Mock<Score> score)
        {
            score.SetupGet(m => m.sixes).Returns(30);
            score.SetupGet(m => m.fives).Returns(25);
            score.SetupGet(m => m.fours).Returns(20);
            score.SetupGet(m => m.threes).Returns(15);
            score.SetupGet(m => m.twos).Returns(10);
            score.SetupGet(m => m.aces).Returns(5);
            score.SetupGet(m => m.onePair).Returns(12);
            score.SetupGet(m => m.twoPair).Returns(22);
            score.SetupGet(m => m.toak).Returns(18);
            score.SetupGet(m => m.foak).Returns(24);
            score.SetupGet(m => m.smallStraight).Returns(15);
            score.SetupGet(m => m.largeStraight).Returns(20);
            score.SetupGet(m => m.fullHouse).Returns(28);
            score.SetupGet(m => m.chance).Returns(30);
            score.SetupGet(m => m.yahtzee).Returns(50);
            score.SetupGet(m => m.UpperBonus).Returns(50);
            score.SetupGet(m => m.UpperScore).Returns(155);
            score.SetupGet(m => m.TotalScore).Returns(374);
        }
    }
}
