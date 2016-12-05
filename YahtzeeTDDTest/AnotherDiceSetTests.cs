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
            sut.RollAll();

            Random random = new Random();
            Dice[] expected = new Dice[5];
            for (int i = 0; i < expected.Length; i += 1)
            {
                expected[i] = new Dice(random);
            }

            mockFactory.Verify(m => m.CreateDice(), Times.Exactly(5));
            Assert.AreEqual(expected, sut.diceset);
        }
    }
}
