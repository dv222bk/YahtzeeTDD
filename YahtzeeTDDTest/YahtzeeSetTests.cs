using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using YahtzeeTDD;

namespace YahtzeeTDDTest
{
    [TestClass]
    public class YahtzeeSetTests
    {
        private Mock<Dice>[] MockDiceSet = new Mock<Dice>[5];
        private Dice[] DiceSet = new Dice[5];
        private YahtzeeSet sut;

        public YahtzeeSetTests()
        {
            for(int i = 0; i < 5; i += 1) {
                MockDiceSet[i] = new Mock<Dice>(new Random());
                DiceSet[i] = MockDiceSet[i].Object;
            }
            sut = new YahtzeeSet(DiceSet);
        }

        [TestMethod]
        public void RollAllShouldUnsaveAndRollAllDice()
        {
            sut.RollAll();
            foreach (Mock<Dice> mock in MockDiceSet)
            {
                mock.VerifySet(m => m.Saved = false);
                mock.Verify(m => m.Roll(), Times.Once);
            }
        }

        [TestMethod]
        public void UnsaveAllShouldUnsaveAllDice()
        {
            sut.UnsaveAll();
            foreach (Mock<Dice> mock in MockDiceSet)
            {
                mock.VerifySet(m => m.Saved = false);
            }
        }
    }
}
