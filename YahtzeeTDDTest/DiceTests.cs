using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using YahtzeeTDD;

namespace YahtzeeTDDTest
{
    [TestClass]
    public class DiceTests
    {
        private Dice sut = new Dice();

        [TestMethod]
        public void RollShouldGenerateValidRandomNumbers()
        {
            var mock = new Mock<Random>();

            sut.Roll();
            mock.Verify(m => m.Next(1, 6), Times.Once());
        }
    }
}
