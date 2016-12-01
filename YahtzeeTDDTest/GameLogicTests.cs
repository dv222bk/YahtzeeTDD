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

        [TestMethod]
        public void RollDicesShouldRollAllUnsavedDiceIfStateIsPlayingAndSetCurrentViewToRollIfTheUserCanStillRoll()
        {
            sut.State = State.Playing;
            MockYahtzeeSet.SetupGet(m => m.CanThrow).Returns(true);
            sut.RollDices();

            MockYahtzeeSet.Verify(m => m.RollUnsaved(), Times.Once);
            Assert.AreEqual(CurrentView.Roll, sut.CurrentView);
        }

        [TestMethod]
        public void RollDicesShouldRollAllUnsavedDiceIfStateIsPlayingAndSetCurrentViewToSaveScoreAndStateToSavingIfTheUserCannotRollAnyMore()
        {
            sut.State = State.Playing;
            MockYahtzeeSet.SetupSequence(m => m.CanThrow).Returns(true).Returns(false);
            sut.RollDices();

            MockYahtzeeSet.Verify(m => m.RollUnsaved(), Times.Once);
            Assert.AreEqual(CurrentView.SaveScore, sut.CurrentView);
            Assert.AreEqual(State.Saving, sut.State);
        }
    }
}
