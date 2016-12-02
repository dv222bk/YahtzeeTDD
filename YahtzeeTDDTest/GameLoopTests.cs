using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using YahtzeeTDD;

namespace YahtzeeTDDTest
{
    [TestClass]
    public class GameLoopTests
    {
        private Mock<YahtzeeView> MockView;
        private Mock<GameLogic> MockLogic;
        private GameLoop sut;
        private TestFunctions testFunctions = new TestFunctions();

        public GameLoopTests()
        {
            MockView = new Mock<YahtzeeView>(new YahtzeeSet(new Dice[5]), new Score(new YahtzeeSet(new Dice[5])), new UserConsole());
            MockLogic = new Mock<GameLogic>(new YahtzeeSet(new Dice[5]), new Score(new YahtzeeSet(new Dice[5])));
            sut = new GameLoop(MockView.Object, MockLogic.Object);
        }

        [TestMethod]
        public void LoopShouldCallViewClearConsole()
        {
            sut.Loop();

            MockView.Verify(m => m.ClearConsole(), Times.Once);
        }

        [TestMethod]
        public void LoopShouldCallViewShowLogo()
        {
            sut.Loop();

            MockView.Verify(m => m.ShowLogo(), Times.Once);
        }
    }
}
