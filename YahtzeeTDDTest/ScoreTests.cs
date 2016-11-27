﻿using System;
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
        public void SaveThreesShouldCountAllDiceWithThreesAndSaveSum()
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

        // FOURS
        [TestMethod]
        public void SaveFoursShouldCountAllDiceWithFoursAndSaveSum()
        {
            SetupDice(new int[] { 4, 4, 2, 2, 4 });

            bool result = sut.saveFours();

            VerifyDiceNumberGet();

            Assert.IsTrue(result);
            Assert.AreEqual(12, sut.fours);
        }

        [TestMethod]
        public void SaveFoursShouldSaveZeroIfNoOnes()
        {
            SetupDice(new int[] { 5, 5, 5, 5, 5 });

            bool result = sut.saveFours();

            VerifyDiceNumberGet();

            Assert.IsTrue(result);
            Assert.AreEqual(0, sut.fours);
        }

        [TestMethod]
        public void SaveFoursShouldReturnFalseIfValueAlreadyExists()
        {
            bool result = sut.saveFours();

            Assert.IsTrue(result);

            result = sut.saveFours();

            Assert.IsFalse(result);
        }

        // FIVES
        [TestMethod]
        public void SaveFivesShouldCountAllDiceWithFivesAndSaveSum()
        {
            SetupDice(new int[] { 5, 5, 2, 5, 4 });

            bool result = sut.saveFives();

            VerifyDiceNumberGet();

            Assert.IsTrue(result);
            Assert.AreEqual(15, sut.fives);
        }

        [TestMethod]
        public void SaveFivesShouldSaveZeroIfNoOnes()
        {
            SetupDice(new int[] { 1, 1, 1, 1, 1 });

            bool result = sut.saveFives();

            VerifyDiceNumberGet();

            Assert.IsTrue(result);
            Assert.AreEqual(0, sut.fives);
        }

        [TestMethod]
        public void SaveFivesShouldReturnFalseIfValueAlreadyExists()
        {
            bool result = sut.saveFives();

            Assert.IsTrue(result);

            result = sut.saveFives();

            Assert.IsFalse(result);
        }

        // SIXES
        [TestMethod]
        public void SaveSixesShouldCountAllDiceWithSixesAndSaveSum()
        {
            SetupDice(new int[] { 6, 5, 2, 5, 4 });

            bool result = sut.saveSixes();

            VerifyDiceNumberGet();

            Assert.IsTrue(result);
            Assert.AreEqual(6, sut.sixes);
        }

        [TestMethod]
        public void SaveSixesShouldSaveZeroIfNoOnes()
        {
            SetupDice(new int[] { 1, 1, 1, 1, 1 });

            bool result = sut.saveSixes();

            VerifyDiceNumberGet();

            Assert.IsTrue(result);
            Assert.AreEqual(0, sut.sixes);
        }

        [TestMethod]
        public void SaveSixesShouldReturnFalseIfValueAlreadyExists()
        {
            bool result = sut.saveSixes();

            Assert.IsTrue(result);

            result = sut.saveSixes();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UpperBonusShouldReturn35IfUpperScoresEquals63OrMore()
        {
            FillUpperScore();

            int score = sut.UpperBonus;

            Assert.AreEqual(35, score);
        }
    }
}
