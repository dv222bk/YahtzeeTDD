using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using YahtzeeTDD;

namespace YahtzeeTDDTest
{
    [TestClass]
    public class ScoreTests
    {
        private Mock<Dice>[] MockDiceSet = new Mock<Dice>[5];
        private Mock<YahtzeeSet> mock;
        private Score sut;

        public ScoreTests()
        {
            mock = new Mock<YahtzeeSet>(new Object[] { new Dice[5] });
            for (int i = 0; i < 5; i += 1)
            {
                MockDiceSet[i] = new Mock<Dice>(new Random());
            }
            mock.Object.DiceSet = new Dice[] 
            { 
                MockDiceSet[0].Object, 
                MockDiceSet[1].Object, 
                MockDiceSet[2].Object, 
                MockDiceSet[3].Object, 
                MockDiceSet[4].Object
            };
            sut = new Score(mock.Object);
        }

        public void SetupDice(int[] diceValues)
        {
            for (int i = 0; i < 5; i += 1)
            {
                MockDiceSet[i].SetupGet(m => m.Number).Returns(diceValues[i]);
            }
        }

        [TestMethod]
        public void SaveAcesShouldCountAllDiceWithOnesAndSaveSum()
        {
            SetupDice(new int[] { 1, 2, 3, 1, 4 });

            sut.saveAces();

            foreach(Mock<Dice> mockDice in MockDiceSet)
            {
                mockDice.VerifyGet(m => m.Number);
            }

            Assert.AreEqual(2, sut.aces);

            SetupDice(new int[] { 5, 5, 5, 5, 5 });

            sut.saveAces();

            Assert.AreEqual(0, sut.aces);
        }

        [TestMethod]
        public void SaveTwosShouldCountAllDiceWithTwosAndSaveSum()
        {
            SetupDice(new int[] { 2, 3, 2, 2, 4 });

            sut.saveTwos();

            foreach (Mock<Dice> mockDice in MockDiceSet)
            {
                mockDice.VerifyGet(m => m.Number);
            }

            Assert.AreEqual(6, sut.twos);

            SetupDice(new int[] { 5, 5, 5, 5, 5 });

            sut.saveTwos();

            Assert.AreEqual(0, sut.twos);
        }
    }
}
