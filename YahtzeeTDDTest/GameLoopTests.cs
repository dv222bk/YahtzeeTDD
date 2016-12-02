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

        [TestMethod]
        public void LoopShouldCallViewShowView()
        {
            sut.Loop();

            MockView.Verify(m => m.ShowView(MockLogic.Object.CurrentView), Times.Once);
        }

        [TestMethod]
        public void LoopShouldCallViewReadInput()
        {
            sut.Loop();

            MockView.Verify(m => m.ReadInput(), Times.Once);
        }

        [TestMethod]
        public void LoopShouldCallMethodsInTheProperOrder()
        {
            int orderOfCalls = 0;
            MockView.Setup(m => m.ClearConsole()).Callback(() => Assert.AreEqual(orderOfCalls++, 0));
            MockView.Setup(m => m.ShowLogo()).Callback(() => Assert.AreEqual(orderOfCalls++, 1));
            MockView.Setup(m => m.ShowView(MockLogic.Object.CurrentView)).Callback(() => Assert.AreEqual(orderOfCalls++, 2));
            MockView.Setup(m => m.ReadInput()).Returns(It.IsAny<String>()).Callback(() => Assert.AreEqual(orderOfCalls++, 3));
            MockLogic.Setup(m => m.ReactToStandardInput(It.IsAny<String>())).Callback(() => Assert.AreEqual(orderOfCalls++, 4));

            sut.Loop();
        }

        [TestMethod]
        public void LoopShouldCallTheProperReactToMethodAfterReactToStandardInput()
        {
            int orderOfCalls = 0;
            MockLogic.Setup(m => m.ReactToStandardInput(It.IsAny<String>())).Callback(() => orderOfCalls++);
            MockLogic.Setup(m => m.ReactToPlayingInput(It.IsAny<String>())).Callback(() => Assert.AreEqual(orderOfCalls++, 2));
            MockLogic.Setup(m => m.ReactToSaveDieInput(It.IsAny<String>())).Callback(() => Assert.AreEqual(orderOfCalls++, 4));
            MockLogic.Setup(m => m.ReactToSavingInput(It.IsAny<String>())).Callback(() => Assert.AreEqual(orderOfCalls++, 6));
            
            for (int i = 0; i < Enum.GetNames(typeof(State)).Length; i += 1)
            {
                MockLogic.Object.State = (State)i;
                sut.Loop();
            }

            MockLogic.Verify(m => m.ReactToStandardInput(It.IsAny<String>()), Times.Exactly(4));
            MockLogic.Verify(m => m.ReactToPlayingInput(It.IsAny<String>()), Times.Once);
            MockLogic.Verify(m => m.ReactToSaveDieInput(It.IsAny<String>()), Times.Once);
            MockLogic.Verify(m => m.ReactToSavingInput(It.IsAny<String>()), Times.Once);
        }
    }
}
