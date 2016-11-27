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
            sut = new Score(mock.Object);
        }

        [TestMethod]
        public void AcesShouldCountAllDiceWithOnesAndSaveSum()
        {
            MockDiceSet[0].SetupGet(m => m.Number).Returns(1);
            MockDiceSet[1].SetupGet(m => m.Number).Returns(2);
            MockDiceSet[2].SetupGet(m => m.Number).Returns(3);
            MockDiceSet[3].SetupGet(m => m.Number).Returns(1);
            MockDiceSet[4].SetupGet(m => m.Number).Returns(4);

            sut.saveAces();
            for (int i = 0; i < 5; i += 1)
            {
                mock.VerifyGet(m => m.DiceSet[i].Number);
            }
            Assert.AreEqual(2, sut.aces);
        }
    }
}
