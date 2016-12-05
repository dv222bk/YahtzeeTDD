using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YahtzeeTDD;

namespace YahtzeeTDDTest
{
    [TestClass]
    public class DiceFactoryTests
    {
        Random random = new Random();
        DiceFactory sut = new DiceFactory(new Random());

        public DiceFactoryTests()
        {
            sut = new DiceFactory(random);
        }

        [TestMethod]
        public void CreateDiceShouldReturnADiceObject()
        {
            var expected = new Dice(random);
            var actual = sut.CreateDice();

            Assert.AreEqual(expected, actual);
        }
    }
}
