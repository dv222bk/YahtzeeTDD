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
        public void LoopShouldCallViewShowLogoAfterClearConsole()
        {
            int orderOfCalls = 0;
            MockView.Setup(m => m.ClearConsole()).Callback(() => Assert.AreEqual(orderOfCalls++, 0));
            MockView.Setup(m => m.ShowLogo()).Callback(() => Assert.AreEqual(orderOfCalls++, 1));
            
            sut.Loop();

            MockView.Verify(m => m.ClearConsole(), Times.Once);
            MockView.Verify(m => m.ShowLogo(), Times.Once);
        }

        [TestMethod]
        public void LoopShouldCallViewShowViewAfterShowLogo()
        {
            int orderOfCalls = 0;
            MockView.Setup(m => m.ShowLogo()).Callback(() => Assert.AreEqual(orderOfCalls++, 0));
            MockView.Setup(m => m.ShowView(MockLogic.Object.CurrentView)).Callback(() => Assert.AreEqual(orderOfCalls++, 1));

            sut.Loop();

            MockView.Verify(m => m.ShowLogo(), Times.Once);
            MockView.Verify(m => m.ShowView(MockLogic.Object.CurrentView), Times.Once);
        }

        [TestMethod]
        public void LoopShouldCallViewReadInputAfterShowView()
        {
            int orderOfCalls = 0;
            MockView.Setup(m => m.ShowView(MockLogic.Object.CurrentView)).Callback(() => Assert.AreEqual(orderOfCalls++, 0));
            MockView.Setup(m => m.ReadInput()).Returns("").Callback(() => Assert.AreEqual(orderOfCalls++, 1));

            sut.Loop();

            MockView.Verify(m => m.ShowView(MockLogic.Object.CurrentView), Times.Once);
            MockView.Verify(m => m.ReadInput(), Times.Once);
        }

        [TestMethod]
        public void LoopShouldCallReactToStandardInputWithInputAfterReadInput()
        {
            int orderOfCalls = 0;
            MockView.Setup(m => m.ReadInput()).Returns("input").Callback(() => Assert.AreEqual(orderOfCalls++, 0));
            MockLogic.Setup(m => m.ReactToStandardInput("input")).Callback(() => Assert.AreEqual(orderOfCalls++, 1));

            sut.Loop();

            MockView.Verify(m => m.ReadInput(), Times.Once);
            MockLogic.Verify(m => m.ReactToStandardInput("input"), Times.Once);
        }
    }
}
