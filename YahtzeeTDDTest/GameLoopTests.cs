﻿using System;
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
            MockLogic.Object.continueGame = false; // Disable looping
        }

        [TestMethod]
        public void LoopShouldCallAllNecessaryMethods()
        {
            sut.Loop();

            MockView.Verify(m => m.ClearConsole(), Times.Once);
            MockView.Verify(m => m.ShowLogo(), Times.Once);
            MockView.Verify(m => m.ShowView(MockLogic.Object.CurrentView), Times.Once);
            MockView.Verify(m => m.ShowCommands(MockLogic.Object.CurrentView), Times.Once);
            MockView.Verify(m => m.ReadInput(), Times.Once);
        }


        [TestMethod]
        public void LoopShouldCallMethodsInTheProperOrder()
        {
            int orderOfCalls = 0;
            MockView.Setup(m => m.ClearConsole()).Callback(() => Assert.AreEqual(orderOfCalls++, 0));
            MockView.Setup(m => m.ShowLogo()).Callback(() => Assert.AreEqual(orderOfCalls++, 1));
            MockView.Setup(m => m.ShowView(MockLogic.Object.CurrentView)).Callback(() => Assert.AreEqual(orderOfCalls++, 2));
            MockView.Setup(m => m.ShowCommands(MockLogic.Object.CurrentView)).Callback(() => Assert.AreEqual(orderOfCalls++, 3));
            MockView.Setup(m => m.ReadInput()).Returns(It.IsAny<String>()).Callback(() => Assert.AreEqual(orderOfCalls++, 4));
            MockLogic.Setup(m => m.ReactToStandardInput(It.IsAny<String>())).Callback(() => Assert.AreEqual(orderOfCalls++, 5));

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

        [TestMethod]
        public void LoopShouldOnlyRunOnceIfContinueGameIsFalse()
        {
            sut.Loop();

            MockLogic.Verify(m => m.ReactToStandardInput(It.IsAny<String>()), Times.Once);
        }

        [TestMethod]
        public void LoopShouldAsLongAsContinueGameIsTrue()
        {
            int loops = 0;
            // Loop 4 times
            MockView.Setup(m => m.ReadInput())
                .Returns(It.IsAny<String>())
                .Callback(() => MockLogic.Object.continueGame = loops++ == 3 ? false : true);

            sut.Loop();

            MockView.Verify(m => m.ReadInput(), Times.Exactly(4));
        }
    }
}
