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
            mock.Setup(m => m.Next(1, 7)).Returns(1);
        }
        
        [TestMethod]
        public void RollShouldGenerateValidRandomNumbers()
        {
            sut.Roll();
            mock.Verify(m => m.Next(1, 7), Times.Once());
        }

        // Due to how ExpectedException works, I cannot combine the following three tests
        // Had I used a different unittest framework, I could've checked for exceptions in the code instead using
        // something like an assert.
        // I could've also used two try/catches, but that seems like even more of a waste.

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

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void NumbersShouldNotAcceptNullAsAValidValue()
        {
            sut.Number = null;
        }
    }
}
