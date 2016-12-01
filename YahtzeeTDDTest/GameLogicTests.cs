using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using YahtzeeTDD;

namespace YahtzeeTDDTest
{
    [TestClass]
    public class GameLogicTests
    {

        private Mock<Dice>[] MockDiceSet = new Mock<Dice>[5];
        private Mock<YahtzeeSet> MockYahtzeeSet;
        private Mock<YahtzeeView> MockView;
        private Mock<Score> MockScore;
        private GameLogic sut;
        private TestFunctions testFunctions = new TestFunctions();

        public GameLogicTests()
        {
            MockYahtzeeSet = new Mock<YahtzeeSet>(new Object[] { new Dice[5] });
            for (int i = 0; i < 5; i += 1)
            {
                MockDiceSet[i] = new Mock<Dice>(new Random());
            }
            MockYahtzeeSet.Object.DiceSet = new Dice[] 
            { 
                MockDiceSet[0].Object, 
                MockDiceSet[1].Object, 
                MockDiceSet[2].Object, 
                MockDiceSet[3].Object, 
                MockDiceSet[4].Object
            };
            testFunctions.SetupMockDice(MockDiceSet, new int[] { 1, 2, 3, 4, 5 }); // default dice setup in case values does not matter
            MockScore = new Mock<Score>(new YahtzeeSet(new Dice[5]));
            MockView = new Mock<YahtzeeView>(new YahtzeeSet(new Dice[5]), new Score(new YahtzeeSet(new Dice[5])), new UserConsole());
            sut = new GameLogic(MockYahtzeeSet.Object, MockScore.Object, MockView.Object);
        }

        public void TestNewGame()
        {
            MockYahtzeeSet.Verify(m => m.Reset(), Times.Once);
            MockScore.Verify(m => m.Reset(), Times.Once);
            Assert.AreEqual(State.Playing, sut.State);
            Assert.AreEqual(CurrentView.Roll, sut.CurrentView);
        }

        public void TestRollDice()
        {
            MockYahtzeeSet.Verify(m => m.RollUnsaved(), Times.Once);
        }
        public void TestSavingInput(int input)
        {
            sut.ReactToSavingInput(input);
            MockYahtzeeSet.Verify(m => m.Reset(), Times.Once);
            MockYahtzeeSet.Reset();
            sut.ReactToSavingInput(input);
            MockYahtzeeSet.Verify(m => m.Reset(), Times.Never);
        }


        [TestMethod]
        public void RollDicesShouldRollAllUnsavedDiceAndSetCurrentViewToRollIfTheUserCanStillRoll()
        {
            MockYahtzeeSet.SetupGet(m => m.CanRoll).Returns(true);
            sut.RollDices();

            TestRollDice();
            Assert.AreEqual(CurrentView.Roll, sut.CurrentView);
        }

        [TestMethod]
        public void RollDicesShouldRollAllUnsavedDiceAndSetCurrentViewToSaveScoreAndStateToSavingIfTheUserCannotRollAnymore()
        {
            MockYahtzeeSet.SetupSequence(m => m.CanRoll).Returns(true).Returns(false);
            sut.RollDices();

            TestRollDice();
            Assert.AreEqual(CurrentView.SaveScore, sut.CurrentView);
            Assert.AreEqual(State.Saving, sut.State);
        }

        [TestMethod]
        public void RollDicesShouldSetCurrentViewToSaveScoreAndStateToSavingIfTheUserCannotRollAnymore()
        {
            MockYahtzeeSet.SetupGet(m => m.CanRoll).Returns(false);
            sut.RollDices();

            MockYahtzeeSet.Verify(m => m.RollUnsaved(), Times.Never);
            Assert.AreEqual(CurrentView.SaveScore, sut.CurrentView);
            Assert.AreEqual(State.Saving, sut.State);
        }

        [TestMethod]
        public void NewGameShouldResetYahtzeeSetAndScoreAndSetStateToPlayingAndSetCurrentViewToRoll()
        {
            sut.NewGame();

            TestNewGame();
        }

        [TestMethod]
        public void ReactToStandardInputShouldSetContinueGameToFalseIfSentQ()
        {
            sut.ReactToStandardInput("Q");

            Assert.AreEqual(false, sut.continueGame);
        }

        [TestMethod]
        public void ReactToStandardInputShouldStartANewGameIfSentN()
        {
            sut.ReactToStandardInput("N");

            TestNewGame();
        }

        [TestMethod]
        public void ReactToPlayingInputShouldSetTheCurrentViewToCheckScoreIfSentC()
        {
            sut.ReactToPlayingInput("C");

            Assert.AreEqual(CurrentView.CheckScore, sut.CurrentView);
        }

        [TestMethod]
        public void ReactToPlayingInputShouldRollDicesIfSentR()
        {
            MockYahtzeeSet.SetupGet(m => m.CanRoll).Returns(true);
            sut.ReactToPlayingInput("R");

            TestRollDice();
        }

        [TestMethod]
        public void ReactToPlayingInputShouldChangeCurrentStateToSavingAndCurrentViewToSaveScoreIfSentA()
        {
            sut.ReactToPlayingInput("A");

            Assert.AreEqual(CurrentView.SaveScore, sut.CurrentView);
            Assert.AreEqual(State.Saving, sut.State);
        }

        [TestMethod]
        public void ReactToPlayingInputShouldChangeCurrentViewToRollIfSentH()
        {
            sut.ReactToPlayingInput("H");

            Assert.AreEqual(CurrentView.Roll, sut.CurrentView);
        }

        [TestMethod]
        public void ReactToSavingInputShouldSaveAcesAndResetYahtzeeSetIfSuccesfullIfSent1()
        {
            MockScore.SetupSequence(m => m.SaveAces()).Returns(true).Returns(false);
            TestSavingInput(1);

            MockScore.Verify(m => m.SaveAces(), Times.Exactly(2));
        }

        [TestMethod]
        public void RecatToSavingInputShouldSaveTwosAndResetYahtzeeSetIfSuccessfullIfSent2()
        {
            MockScore.SetupSequence(m => m.SaveTwos()).Returns(true).Returns(false);
            TestSavingInput(2);

            MockScore.Verify(m => m.SaveTwos(), Times.Exactly(2));
        }
    }
}
