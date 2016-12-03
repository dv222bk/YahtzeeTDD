using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using YahtzeeTDD;

namespace YahtzeeTDDTest
{
    [TestClass]
    public class YahtzeeViewTests
    {
        private Mock<Dice>[] MockDiceSet = new Mock<Dice>[5];
        private Mock<YahtzeeSet> MockYahtzeeSet;
        private Mock<Score> MockScore;
        private Mock<UserConsole> MockConsole;
        private YahtzeeView sut;
        private TestFunctions testFunctions = new TestFunctions();

        public YahtzeeViewTests()
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
            MockConsole = new Mock<UserConsole>();
            sut = new YahtzeeView(MockYahtzeeSet.Object, MockScore.Object, MockConsole.Object);
        }

        [TestMethod]
        public void ClearConsoleShouldCallUserConsoleClear()
        {
            sut.ClearConsole();

            MockConsole.Verify(m => m.Clear(), Times.Once);
        }
    }
}
