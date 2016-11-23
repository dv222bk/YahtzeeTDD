using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using YahtzeeTDD;

namespace YahtzeeTDDTest
{
    [TestClass]
    public class DiceTests
    {
        private static Mock<Random> mock = new Mock<Random>();
        private Dice sut = new Dice(mock.Object);

        public DiceTests()
        {
            mock.Setup(m => m.Next(1, 6)).Returns(1);
        }
        
        [TestMethod]
        public void RollShouldGenerateValidRandomNumbers()
        {
            sut.Roll();
            mock.Verify(m => m.Next(1, 6), Times.Once());
        }

        [TestMethod]
        public void NumberShouldSaveValidNumbers()
        {
            for(int i = 1; i <= 6; i += 1) {
                sut.Number = i;
                Assert.AreEqual(i, sut.Number);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void NumberShouldThrowExceptionIfGivenIllegalLowNumber()
        {
            sut.Number = 0;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void NumberShouldThrowExceptionIfGivenIllegalHighNumber()
        {
            sut.Number = 7;
        }

        [TestMethod]
        public void NumberShouldContainValidValueWithoutCallingRollFirst()
        {
            sut = new Dice(mock.Object);
            Assert.AreEqual(1, sut.Number);
        }
    }
}
