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

        public void FillUpperScore()
        {
            sut.sixes = 30;
            sut.fives = 25;
            sut.fours = 20;
            sut.threes = 15;
            sut.twos = 10;
            sut.aces = 5;
        }

        // ACES
        [TestMethod]
        public void SaveAcesShouldCountAllDiceWithOnesAndSaveSum()
        {
            SetupDice(new int[] { 1, 2, 3, 1, 4 });

            bool result = sut.SaveAces();

            VerifyDiceNumberGet();

            Assert.IsTrue(result);
            Assert.AreEqual(2, sut.aces);
        }

        [TestMethod]
        public void SaveAcesShouldSaveZeroIfNoOnes()
        {
            SetupDice(new int[] { 5, 5, 5, 5, 5 });

            bool result = sut.SaveAces();

            VerifyDiceNumberGet();

            Assert.IsTrue(result);
            Assert.AreEqual(0, sut.aces);
        }

        [TestMethod]
        public void SaveAcesShouldReturnFalseIfValueAlreadyExists()
        {
            bool result = sut.SaveAces();

            Assert.IsTrue(result);

            result = sut.SaveAces();

            Assert.IsFalse(result);
        }

        // TWOS
        [TestMethod]
        public void SaveTwosShouldCountAllDiceWithTwosAndSaveSum()
        {
            SetupDice(new int[] { 2, 3, 2, 2, 4 });

            bool result = sut.SaveTwos();

            VerifyDiceNumberGet();

            Assert.IsTrue(result);
            Assert.AreEqual(6, sut.twos);
        }

        [TestMethod]
        public void SaveTwosShouldSaveZeroIfNoTwos()
        {
            SetupDice(new int[] { 5, 5, 5, 5, 5 });

            bool result = sut.SaveTwos();

            VerifyDiceNumberGet();

            Assert.IsTrue(result);
            Assert.AreEqual(0, sut.twos);
        }

        [TestMethod]
        public void SaveTwosShouldReturnFalseIfValueAlreadyExists()
        {
            bool result = sut.SaveTwos();

            Assert.IsTrue(result);

            result = sut.SaveTwos();

            Assert.IsFalse(result);
        }

        // THREES
        [TestMethod]
        public void SaveThreesShouldCountAllDiceWithThreesAndSaveSum()
        {
            SetupDice(new int[] { 3, 3, 2, 2, 4 });

            bool result = sut.SaveThrees();

            VerifyDiceNumberGet();

            Assert.IsTrue(result);
            Assert.AreEqual(6, sut.threes);
        }

        [TestMethod]
        public void SaveThreesShouldSaveZeroIfNoThrees()
        {
            SetupDice(new int[] { 5, 5, 5, 5, 5 });

            bool result = sut.SaveThrees();

            VerifyDiceNumberGet();

            Assert.IsTrue(result);
            Assert.AreEqual(0, sut.threes);
        }

        [TestMethod]
        public void SaveThreesShouldReturnFalseIfValueAlreadyExists()
        {
            bool result = sut.SaveThrees();

            Assert.IsTrue(result);

            result = sut.SaveThrees();

            Assert.IsFalse(result);
        }

        // FOURS
        [TestMethod]
        public void SaveFoursShouldCountAllDiceWithFoursAndSaveSum()
        {
            SetupDice(new int[] { 4, 4, 2, 2, 4 });

            bool result = sut.SaveFours();

            VerifyDiceNumberGet();

            Assert.IsTrue(result);
            Assert.AreEqual(12, sut.fours);
        }

        [TestMethod]
        public void SaveFoursShouldSaveZeroIfNoFours()
        {
            SetupDice(new int[] { 5, 5, 5, 5, 5 });

            bool result = sut.SaveFours();

            VerifyDiceNumberGet();

            Assert.IsTrue(result);
            Assert.AreEqual(0, sut.fours);
        }

        [TestMethod]
        public void SaveFoursShouldReturnFalseIfValueAlreadyExists()
        {
            bool result = sut.SaveFours();

            Assert.IsTrue(result);

            result = sut.SaveFours();

            Assert.IsFalse(result);
        }

        // FIVES
        [TestMethod]
        public void SaveFivesShouldCountAllDiceWithFivesAndSaveSum()
        {
            SetupDice(new int[] { 5, 5, 2, 5, 4 });

            bool result = sut.SaveFives();

            VerifyDiceNumberGet();

            Assert.IsTrue(result);
            Assert.AreEqual(15, sut.fives);
        }

        [TestMethod]
        public void SaveFivesShouldSaveZeroIfNoFives()
        {
            SetupDice(new int[] { 1, 1, 1, 1, 1 });

            bool result = sut.SaveFives();

            VerifyDiceNumberGet();

            Assert.IsTrue(result);
            Assert.AreEqual(0, sut.fives);
        }

        [TestMethod]
        public void SaveFivesShouldReturnFalseIfValueAlreadyExists()
        {
            bool result = sut.SaveFives();

            Assert.IsTrue(result);

            result = sut.SaveFives();

            Assert.IsFalse(result);
        }

        // SIXES
        [TestMethod]
        public void SaveSixesShouldCountAllDiceWithSixesAndSaveSum()
        {
            SetupDice(new int[] { 6, 5, 2, 5, 4 });

            bool result = sut.SaveSixes();

            VerifyDiceNumberGet();

            Assert.IsTrue(result);
            Assert.AreEqual(6, sut.sixes);
        }

        [TestMethod]
        public void SaveSixesShouldSaveZeroIfNoSixes()
        {
            SetupDice(new int[] { 1, 1, 1, 1, 1 });

            bool result = sut.SaveSixes();

            VerifyDiceNumberGet();

            Assert.IsTrue(result);
            Assert.AreEqual(0, sut.sixes);
        }

        [TestMethod]
        public void SaveSixesShouldReturnFalseIfValueAlreadyExists()
        {
            bool result = sut.SaveSixes();

            Assert.IsTrue(result);

            result = sut.SaveSixes();

            Assert.IsFalse(result);
        }

        // UPPER BONUS
        [TestMethod]
        public void UpperBonusShouldReturn50IfUpperScoreEquals63OrMore()
        {
            FillUpperScore();
            sut.threes = null;

            int? score = sut.UpperBonus;

            Assert.AreEqual(50, score);
        }

        [TestMethod]
        public void UpperBonusShouldReturnNullIfUpperScoreEquals62OrLess()
        {
            int? score = sut.UpperBonus;
            Assert.IsNull(score);
        }

        // UPPER SCORE
        [TestMethod]
        public void UpperScoreShouldReturnTheTotalUpperScorePlusBonus()
        {
            FillUpperScore();

            int score = sut.UpperScore;

            Assert.AreEqual(155, score);
        }

        [TestMethod]
        public void UpperScoreShouldReturnZeroIfNoScore()
        {
            int score = sut.UpperScore;

            Assert.AreEqual(0, score);
        }

        [TestMethod]
        public void UpperScoreShouldReturnTotalEvenIfOnlyOneValueIsSet()
        {
            sut.threes = 6;

            int score = sut.UpperScore;

            Assert.AreEqual(6, score);
        }

        // ONE PAIR
        [TestMethod]
        public void SaveOnePairShouldCountTheHighestPairOfDiceAndSaveSum()
        {
            SetupDice(new int[] { 6, 5, 2, 5, 6 });

            bool result = sut.SaveOnePair();

            VerifyDiceNumberGet();

            Assert.IsTrue(result);
            Assert.AreEqual(12, sut.onePair);
        }

        [TestMethod]
        public void SaveOnePairShouldSaveZeroIfNoPair()
        {
            bool result = sut.SaveOnePair();

            VerifyDiceNumberGet();

            Assert.IsTrue(result);
            Assert.AreEqual(0, sut.onePair);
        }

        [TestMethod]
        public void SaveOnePairShouldReturnFalseIfValueAlreadyExists()
        {
            bool result = sut.SaveOnePair();

            Assert.IsTrue(result);

            result = sut.SaveOnePair();

            Assert.IsFalse(result);
        }

        // TWO PAIR
        [TestMethod]
        public void SaveTwoPairShouldCountTwoPairsOfDiceAndSaveSum()
        {
            SetupDice(new int[] { 6, 5, 2, 5, 6 });

            bool result = sut.SaveTwoPair();

            VerifyDiceNumberGet();

            Assert.IsTrue(result);
            Assert.AreEqual(22, sut.twoPair);
        }

        [TestMethod]
        public void SaveTwoPairShouldSaveZeroIfNotEnoughPair()
        {
            SetupDice(new int[] { 6, 5, 2, 3, 6 });
            bool result = sut.SaveTwoPair();

            VerifyDiceNumberGet();

            Assert.IsTrue(result);
            Assert.AreEqual(0, sut.twoPair);
        }

        [TestMethod]
        public void SaveTwoPairShouldReturnFalseIfValueAlreadyExists()
        {
            bool result = sut.SaveTwoPair();

            Assert.IsTrue(result);

            result = sut.SaveTwoPair();

            Assert.IsFalse(result);
        }

        // THREE OF A KIND
        [TestMethod]
        public void SaveToaKShouldCountThreeDiceWithTheSameValueAndSaveSum()
        {
            SetupDice(new int[] { 5, 2, 5, 3, 5 });
            bool result = sut.SaveToaK();

            VerifyDiceNumberGet();

            Assert.IsTrue(result);
            Assert.AreEqual(15, sut.toak);
        }

        [TestMethod]
        public void SaveToaKShouldSaveZeroIfNotEnoughIdenticalDice()
        {
            SetupDice(new int[] { 6, 5, 2, 3, 6 });
            bool result = sut.SaveToaK();

            VerifyDiceNumberGet();

            Assert.IsTrue(result);
            Assert.AreEqual(0, sut.toak);
        }

        [TestMethod]
        public void SaveToaKShouldReturnFalseIfValueAlreadyExists()
        {
            bool result = sut.SaveToaK();

            Assert.IsTrue(result);

            result = sut.SaveToaK();

            Assert.IsFalse(result);
        }

        // FOUR OF A KIND
        [TestMethod]
        public void SaveFoaKShouldCountFourDiceWithTheSameValueAndSaveSum()
        {
            SetupDice(new int[] { 5, 2, 5, 5, 5 });
            bool result = sut.SaveFoaK();

            VerifyDiceNumberGet();

            Assert.IsTrue(result);
            Assert.AreEqual(20, sut.foak);
        }

        [TestMethod]
        public void SaveFoaKShouldSaveZeroIfNotEnoughIdenticalDice()
        {
            SetupDice(new int[] { 6, 6, 2, 3, 6 });
            bool result = sut.SaveFoaK();

            VerifyDiceNumberGet();

            Assert.IsTrue(result);
            Assert.AreEqual(0, sut.foak);
        }

        [TestMethod]
        public void SaveFoaKShouldReturnFalseIfValueAlreadyExists()
        {
            bool result = sut.SaveFoaK();

            Assert.IsTrue(result);

            result = sut.SaveFoaK();

            Assert.IsFalse(result);
        }

        // SMALL STRAIGHT
        [TestMethod]
        public void SaveSmallStraightShouldCheckForSmallStraightThanSave15()
        {
            SetupDice(new int[] { 1, 3, 2, 5, 4 });
            bool result = sut.SaveSmallStraight();

            VerifyDiceNumberGet();

            Assert.IsTrue(result);
            Assert.AreEqual(15, sut.smallStraight);
        }

        [TestMethod]
        public void SaveSmallStraightShouldSaveZeroIfNoSmallStraightExists()
        {
            SetupDice(new int[] { 1, 3, 2, 4, 6 });
            bool result = sut.SaveSmallStraight();

            VerifyDiceNumberGet();

            Assert.IsTrue(result);
            Assert.AreEqual(0, sut.smallStraight);
        }

        [TestMethod]
        public void SaveSmallStraightShouldReturnFalseIfValueAlreadyExists()
        {
            bool result = sut.SaveSmallStraight();

            Assert.IsTrue(result);

            result = sut.SaveSmallStraight();

            Assert.IsFalse(result);
        }
    }
}
