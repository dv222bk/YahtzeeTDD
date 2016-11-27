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
            SetupDice(new int[] { 1, 2, 3, 4, 5 }); // default dice setup in case values does not matter
            sut = new Score(mock.Object);
        }

        public void SetupDice(int[] diceValues)
        {
            for (int i = 0; i < 5; i += 1)
            {
                MockDiceSet[i].SetupGet(m => m.Number).Returns(diceValues[i]);
            }
        }

        public void VerifyDiceNumberGet()
        {
            foreach (Mock<Dice> mockDice in MockDiceSet)
            {
                mockDice.VerifyGet(m => m.Number);
            }
        }

        // ACES
        [TestMethod]
        public void SaveAcesShouldCountAllDiceWithOnesAndSaveSum()
        {
            SetupDice(new int[] { 1, 2, 3, 1, 4 });

            bool result = sut.saveAces();

            VerifyDiceNumberGet();

            Assert.IsTrue(result);
            Assert.AreEqual(2, sut.aces);
        }

        [TestMethod]
        public void SaveAcesShouldSaveZeroIfNoOnes()
        {
            SetupDice(new int[] { 5, 5, 5, 5, 5 });

            bool result = sut.saveAces();

            VerifyDiceNumberGet();

            Assert.IsTrue(result);
            Assert.AreEqual(0, sut.aces);
        }

        [TestMethod]
        public void SaveAcesShouldReturnFalseIfValueAlreadyExists()
        {
            bool result = sut.saveAces();

            Assert.IsTrue(result);

            result = sut.saveAces();

            Assert.IsFalse(result);
        }

        // TWOS
        [TestMethod]
        public void SaveTwosShouldCountAllDiceWithTwosAndSaveSum()
        {
            SetupDice(new int[] { 2, 3, 2, 2, 4 });

            bool result = sut.saveTwos();

            VerifyDiceNumberGet();

            Assert.IsTrue(result);
            Assert.AreEqual(6, sut.twos);
        }

        [TestMethod]
        public void SaveTwosShouldSaveZeroIfNoOnes()
        {
            SetupDice(new int[] { 5, 5, 5, 5, 5 });

            bool result = sut.saveTwos();

            VerifyDiceNumberGet();

            Assert.IsTrue(result);
            Assert.AreEqual(0, sut.twos);
        }

        [TestMethod]
        public void SaveTwosShouldReturnFalseIfValueAlreadyExists()
        {
            bool result = sut.saveTwos();

            Assert.IsTrue(result);

            result = sut.saveTwos();

            Assert.IsFalse(result);
        }

        // THREES
        [TestMethod]
        public void SaveThreesShouldCountAllDiceWithTwosAndSaveSum()
        {
            SetupDice(new int[] { 3, 3, 2, 2, 4 });

            bool result = sut.saveThrees();

            VerifyDiceNumberGet();

            Assert.IsTrue(result);
            Assert.AreEqual(6, sut.threes);
        }

        [TestMethod]
        public void SaveThreesShouldSaveZeroIfNoOnes()
        {
            SetupDice(new int[] { 5, 5, 5, 5, 5 });

            bool result = sut.saveThrees();

            VerifyDiceNumberGet();

            Assert.IsTrue(result);
            Assert.AreEqual(0, sut.threes);
        }

        [TestMethod]
        public void SaveThreesShouldReturnFalseIfValueAlreadyExists()
        {
            bool result = sut.saveThrees();

            Assert.IsTrue(result);

            result = sut.saveThrees();

            Assert.IsFalse(result);
        }
    }
}
