using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using YahtzeeTDD;

namespace YahtzeeTDDTest
{
    [TestClass]
    public class AnotherDiceSetTests
    {
        Mock<Dice>[] MockDiceSet = new Mock<Dice>[5];
        Dice[] MockDiceObjectSet = new Dice[5];
        Mock<DiceFactory> mockFactory = new Mock<DiceFactory>(new Random());
        AnotherDiceSet sut;

        public AnotherDiceSetTests()
        {
            for (int i = 0; i < 5; i += 1)
            {
                MockDiceSet[i] = new Mock<Dice>(new Random());
                MockDiceObjectSet[i] = MockDiceSet[i].Object;
            }
            sut = new AnotherDiceSet(mockFactory.Object);
        }

        public void SetupSutDiceSet()
        {
            sut.DiceSet = MockDiceObjectSet;
        }

        [TestMethod]
        public void RollAllShouldFillDiceSetWithNewDice()
        {
            mockFactory.SetupSequence(m => m.CreateDice())
                .Returns(MockDiceObjectSet[0])
                .Returns(MockDiceObjectSet[1])
                .Returns(MockDiceObjectSet[2])
                .Returns(MockDiceObjectSet[3])
                .Returns(MockDiceObjectSet[4]);

            sut.RollAll();

            mockFactory.Verify(m => m.CreateDice(), Times.Exactly(5));
            CollectionAssert.AreEqual(MockDiceObjectSet, sut.DiceSet);
        }
        
        [TestMethod]
        public void RollAllShouldNotDoAnythingIfCurrentRollEqualsMaxRoll()
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

        [TestMethod]
        public void UnsaveAllShouldUnsaveAllDice()
        {
            sut.DiceSet = MockDiceObjectSet;
            sut.UnsaveAll();
            foreach (Mock<Dice> mock in MockDiceSet)
            {
                mock.VerifySet(m => m.Saved = false);
            }
        }
    }
}
