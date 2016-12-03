﻿using System;
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

        [TestMethod]
        public void ShowLogoShouldPrintOutTheLogoToTheConsole()
        {
            int orderOfCalls = 0;
            MockConsole.Setup(m => m.WriteLine(Strings.LogoTop)).Callback(() => Assert.AreEqual(orderOfCalls++, 0));
            MockConsole.Setup(m => m.WriteLine(Strings.LogoText)).Callback(() => Assert.AreEqual(orderOfCalls++, 1));
            MockConsole.Setup(m => m.WriteLine(Strings.LogoBottom)).Callback(() => Assert.AreEqual(orderOfCalls++, 2));
            MockConsole.Setup(m => m.WriteLine("")).Callback(() => Assert.AreEqual(orderOfCalls++, 3));

            sut.ShowLogo();

            MockConsole.Verify(m => m.WriteLine(It.IsAny<String>()), Times.Exactly(4));
        }

        [TestMethod]
        public void ShowCommandsShouldShowTheCorrectSetOfCommandsForEachView()
        {
            int orderOfCalls = 0;
            MockConsole.Setup(m => m.WriteLine(Strings.StandardCommands)).Callback(() => orderOfCalls++);
            MockConsole.Setup(m => m.WriteLine(Strings.PlayingCommands)).Callback(() => Assert.IsTrue(orderOfCalls == 1 || orderOfCalls == 2));
            MockConsole.Setup(m => m.WriteLine(Strings.SaveScoreCommands)).Callback(() => Assert.AreEqual(orderOfCalls++, 3));
            MockConsole.Setup(m => m.WriteLine(Strings.SaveDieCommands)).Callback(() => Assert.AreEqual(orderOfCalls++, 5));

            for (int i = 0; i < Enum.GetNames(typeof(CurrentView)).Length; i += 1)
            {
                sut.ShowCommands((CurrentView)i);
            }

            MockConsole.Verify(m => m.WriteLine(Strings.CommandsHeader), Times.Exactly(Enum.GetNames(typeof(CurrentView)).Length));
            MockConsole.Verify(m => m.WriteLine(Strings.StandardCommands), Times.Exactly(Enum.GetNames(typeof(CurrentView)).Length));
            MockConsole.Verify(m => m.WriteLine(Strings.PlayingCommands), Times.Exactly(2));
            MockConsole.Verify(m => m.WriteLine(Strings.SaveScoreCommands), Times.Once);
            MockConsole.Verify(m => m.WriteLine(Strings.SaveDieCommands), Times.Once);
            MockConsole.Verify(m => m.WriteLine(""), Times.Exactly(Enum.GetNames(typeof(CurrentView)).Length));
        }
    }
}
