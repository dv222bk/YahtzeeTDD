using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using YahtzeeTDD;

namespace YahtzeeTDDTest
{
    [TestClass]
    public class AnotherDiceSetTests
    {
        Mock<DiceFactory> mockFactory = new Mock<DiceFactory>(new Random());
        AnotherDiceSet sut;

        public AnotherDiceSetTests()
        {
            sut = new AnotherDiceSet(mockFactory.Object);
        }

        [TestMethod]
        public void RollAllShouldFillDiceSetWithNewDice()
        {
            Random random = new Random();
            Dice[] expected = new Dice[5];
            for (int i = 0; i < expected.Length; i += 1)
            {
                expected[i] = new Dice(random);
            }
            mockFactory.SetupSequence(m => m.CreateDice())
                .Returns(expected[0])
                .Returns(expected[1])
                .Returns(expected[2])
                .Returns(expected[3])
                .Returns(expected[4]);

            sut.RollAll();

            mockFactory.Verify(m => m.CreateDice(), Times.Exactly(5));
            CollectionAssert.AreEqual(expected, sut.DiceSet);
        }
        
        [TestMethod]
        public void RollAllShouldNotFillDiceSetIfCurrentRollEqualsMaxRoll()
        {
            sut.CurrentRoll = 3;
            sut.RollAll();
            mockFactory.Verify(m => m.CreateDice(), Times.Never);
            Assert.AreEqual(3, sut.CurrentRoll);
        }

        [TestMethod]
        public void RollAllShouldIncreaseCurrentRollIsLessThanMaxRolls()
        {
            sut.RollAll();
            Assert.AreEqual(2, sut.CurrentRoll);
        }
    }
}
