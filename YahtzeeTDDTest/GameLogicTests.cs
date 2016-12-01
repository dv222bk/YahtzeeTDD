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
        public void TestSaveScoreInput(int input)
        {
            Assert.IsTrue(sut.SaveScore(input));
            Assert.IsFalse(sut.SaveScore(input));
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
        public void ReactToPlayingInputShouldChangeCurrentViewToSaveDieAndStateToSaveDieIfSentS()
        {
            sut.ReactToPlayingInput("S");

            Assert.AreEqual(CurrentView.SaveDie, sut.CurrentView);
            Assert.AreEqual(State.SaveDie, sut.State);
        }

        [TestMethod]
        public void SaveScoreShouldReturnFalseIfGivenAnInvalidValue()
        {
            Assert.IsFalse(sut.SaveScore(999));
        }

        [TestMethod]
        public void SaveScoreShouldSaveAcesAndReturnScoreBoolReturnValueIfSent1()
        {
            MockScore.SetupSequence(m => m.SaveAces()).Returns(true).Returns(false);

            TestSaveScoreInput(1);

            MockScore.Verify(m => m.SaveAces(), Times.Exactly(2));
        }

        [TestMethod]
        public void SaveScoreShouldSaveTwosAndReturnScoreBoolReturnValueIfSent2()
        {
            MockScore.SetupSequence(m => m.SaveTwos()).Returns(true).Returns(false);

            TestSaveScoreInput(2);

            MockScore.Verify(m => m.SaveTwos(), Times.Exactly(2));
        }

        [TestMethod]
        public void SaveScoreShouldSaveThreesAndReturnScoreBoolReturnValueIfSent3()
        {
            MockScore.SetupSequence(m => m.SaveThrees()).Returns(true).Returns(false);

            TestSaveScoreInput(3);

            MockScore.Verify(m => m.SaveThrees(), Times.Exactly(2));
        }

        [TestMethod]
        public void SaveScoreShouldSaveFoursAndReturnScoreBoolReturnValueIfSent4()
        {
            MockScore.SetupSequence(m => m.SaveFours()).Returns(true).Returns(false);

            TestSaveScoreInput(4);

            MockScore.Verify(m => m.SaveFours(), Times.Exactly(2));
        }

        [TestMethod]
        public void SaveScoreShouldSaveFivesAndReturnScoreBoolReturnValueIfSent5()
        {
            MockScore.SetupSequence(m => m.SaveFives()).Returns(true).Returns(false);

            TestSaveScoreInput(5);

            MockScore.Verify(m => m.SaveFives(), Times.Exactly(2));
        }

        [TestMethod]
        public void SaveScoreShouldSaveSixesAndReturnScoreBoolReturnValueIfSent6()
        {
            MockScore.SetupSequence(m => m.SaveSixes()).Returns(true).Returns(false);

            TestSaveScoreInput(6);

            MockScore.Verify(m => m.SaveSixes(), Times.Exactly(2));
        }

        [TestMethod]
        public void SaveScoreShouldSaveOnePairAndReturnScoreBoolReturnValueIfSent7()
        {
            MockScore.SetupSequence(m => m.SaveOnePair()).Returns(true).Returns(false);

            TestSaveScoreInput(7);

            MockScore.Verify(m => m.SaveOnePair(), Times.Exactly(2));
        }

        [TestMethod]
        public void SaveScoreShouldSaveTwoPairAndReturnScoreBoolReturnValueIfSent8()
        {
            MockScore.SetupSequence(m => m.SaveTwoPair()).Returns(true).Returns(false);

            TestSaveScoreInput(8);

            MockScore.Verify(m => m.SaveTwoPair(), Times.Exactly(2));
        }

        [TestMethod]
        public void SaveScoreShouldSaveToaKAndReturnScoreBoolReturnValueIfSent9()
        {
            MockScore.SetupSequence(m => m.SaveToaK()).Returns(true).Returns(false);

            TestSaveScoreInput(9);

            MockScore.Verify(m => m.SaveToaK(), Times.Exactly(2));
        }

        [TestMethod]
        public void SaveScoreShouldSaveFoaKAndReturnScoreBoolReturnValueIfSen10()
        {
            MockScore.SetupSequence(m => m.SaveFoaK()).Returns(true).Returns(false);

            TestSaveScoreInput(10);

            MockScore.Verify(m => m.SaveFoaK(), Times.Exactly(2));
        }

        [TestMethod]
        public void SaveScoreShouldSaveSmallStraightAndReturnScoreBoolReturnValueIfSent11()
        {
            MockScore.SetupSequence(m => m.SaveSmallStraight()).Returns(true).Returns(false);

            TestSaveScoreInput(11);

            MockScore.Verify(m => m.SaveSmallStraight(), Times.Exactly(2));
        }

        [TestMethod]
        public void SaveScoreShouldSaveLargeStraightAndReturnScoreBoolReturnValueIfSent12()
        {
            MockScore.SetupSequence(m => m.SaveLargeStraight()).Returns(true).Returns(false);

            TestSaveScoreInput(12);

            MockScore.Verify(m => m.SaveLargeStraight(), Times.Exactly(2));
        }

        [TestMethod]
        public void SaveScoreShouldSaveFullHouseAndReturnScoreBoolReturnValueIfSent13()
        {
            MockScore.SetupSequence(m => m.SaveFullHouse()).Returns(true).Returns(false);

            TestSaveScoreInput(13);

            MockScore.Verify(m => m.SaveFullHouse(), Times.Exactly(2));
        }

        [TestMethod]
        public void SaveScoreShouldSaveChanceAndReturnScoreBoolReturnValueIfSent14()
        {
            MockScore.SetupSequence(m => m.SaveChance()).Returns(true).Returns(false);

            TestSaveScoreInput(14);

            MockScore.Verify(m => m.SaveChance(), Times.Exactly(2));
        }

        [TestMethod]
        public void SaveScoreShouldSaveTwosAndReturnScoreBoolReturnValueIfSent15()
        {
            MockScore.SetupSequence(m => m.SaveYahtzee()).Returns(true).Returns(false);

            TestSaveScoreInput(15);

            MockScore.Verify(m => m.SaveYahtzee(), Times.Exactly(2));
        }

        [TestMethod]
        public void ReactToSavingInputShouldCallSaveScoreAndResetYahtzeeSetAndSetCurrentViewToRollAndSetStateToPlayingIfSuccessfullAndIfScoreIsNotFull()
        {
            MockScore.Setup(m => m.SaveAces()).Returns(true);
            MockScore.SetupGet(m => m.IsFull).Returns(false);
            sut.ReactToSavingInput("1");

            MockScore.Verify(m => m.SaveAces(), Times.Once);
            MockScore.VerifyGet(m => m.IsFull, Times.Once);
            MockYahtzeeSet.Verify(m => m.Reset(), Times.Once);
            Assert.AreEqual(CurrentView.Roll, sut.CurrentView);
            Assert.AreEqual(State.Playing, sut.State);
        }

        [TestMethod]
        public void ReactToSavingInputShouldCallSaveScoreAndChangeCurrentViewToFinishAndStateToStartIfSuccessfullAndIfScoreIsFull()
        {
            MockScore.Setup(m => m.SaveAces()).Returns(true);
            MockScore.SetupGet(m => m.IsFull).Returns(true);
            sut.ReactToSavingInput("1");

            MockScore.Verify(m => m.SaveAces(), Times.Once);
            MockScore.VerifyGet(m => m.IsFull, Times.Once);
            MockYahtzeeSet.Verify(m => m.Reset(), Times.Never);
            Assert.AreEqual(CurrentView.Finish, sut.CurrentView);
            Assert.AreEqual(State.Start, sut.State);
        }
    }
}
