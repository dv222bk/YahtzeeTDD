﻿using System;
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

        public void AssertRollMethodsDoNothing()
        {
            foreach (Mock<Dice> mock in MockDiceSet)
            {
                mock.VerifySet(m => m.Saved = false, Times.Never);
                mock.Verify(m => m.Roll(), Times.Never);
            }
            Assert.AreEqual(3, sut.CurrentRoll);
        }

        public void AssertRollAll()
        {
            foreach (Mock<Dice> mock in MockDiceSet)
            {
                mock.VerifySet(m => m.Saved = false);
                mock.Verify(m => m.Roll(), Times.Once);
            }
        }

        [TestMethod]
        public void RollAllShouldUnsaveAndRollAllDices()
        {
            sut.RollAll();
            AssertRollAll();
        }

        [TestMethod]
        public void RollAllShouldIncreaseCurrentRollByOne()
        {
            sut.RollAll();
            Assert.AreEqual(2, sut.CurrentRoll);
        }

        [TestMethod]
        public void RollAllShouldNotDoAnythingIfCurrentRollEqualsMaxRolls()
        {
            sut.CurrentRoll = 3;
            sut.RollAll();
            AssertRollMethodsDoNothing();
        }

        [TestMethod]
        public void UnsaveAllShouldUnsaveAllDices()
        {
            sut.UnsaveAll();
            foreach (Mock<Dice> mock in MockDiceSet)
            {
                mock.VerifySet(m => m.Saved = false);
            }
        }

        [TestMethod]
        public void RollUnsavedShouldRollAllUnsavedDices()
        {
            for (int i = 0; i < 5; i += 2)
            {
                MockDiceSet[i].SetupGet(m => m.Saved).Returns(true);
            }

            sut.RollUnsaved();

            for (int i = 1; i < 5; i += 2)
            {
                MockDiceSet[i].Verify(m => m.Roll(), Times.Once);
            }

            for (int i = 0; i < 5; i += 2)
            {
                MockDiceSet[i].Verify(m => m.Roll(), Times.Never);
            }
        }

        [TestMethod]
        public void RollUnsavedShouldIncreaseCurrentRollByOne()
        {
            sut.RollUnsaved();
            Assert.AreEqual(2, sut.CurrentRoll);
        }

        [TestMethod]
        public void RollUnsavedShouldNotDoAnythingIfCurrentRollEqualsMaxRolls()
        {
            sut.CurrentRoll = 3;
            sut.RollUnsaved();
            AssertRollMethodsDoNothing();
        }

        [TestMethod]
        public void ResetShouldResetCurrentRollAndRerollAllDices()
        {
            sut.CurrentRoll = 3;
            sut.Reset();
            AssertRollAll();
            Assert.AreEqual(1, sut.CurrentRoll);
        }

        [TestMethod]
        public void CanRollShouldReturnTrueIfThePlayerCanRollTheDices()
        {
            Assert.IsTrue(sut.CanRoll);
        }

        [TestMethod]
        public void CanRollShouldReturnFalseIfThePlayerCannotRollTheDices()
        {
            sut.CurrentRoll = 3;
            Assert.IsFalse(sut.CanRoll);
        }
    }
}
