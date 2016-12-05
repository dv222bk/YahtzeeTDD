using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YahtzeeTDD;

namespace YahtzeeTDDTest
{
    [TestClass]
    public class DiceFactoryTests
    {
        DiceFactory sut = new DiceFactory(new Random());

        [TestMethod]
        public void CreateDiceShouldReturnADiceObject()
        {
            var expected = new Dice(new Random());
            var actual = sut.CreateDice();

            Assert.AreEqual(expected.GetType(), actual.GetType());
        }
    }
}
