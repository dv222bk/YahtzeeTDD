using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using YahtzeeTDD;

namespace YahtzeeTDDTest
{
    [TestClass]
    public class ScoreTests
    {
        private Mock<Dice>[] MockDiceSet = new Mock<Dice>[5];
        private Mock<YahtzeeSet> mock;
        private Score sut;
        private TestFunctions testFunctions = new TestFunctions();

        public ScoreTests()
        {
            mock = new Mock<YahtzeeSet>(new Object[] { new Dice[5] });
            for (int i = 0; i < 5; i += 1)
            {
                MockDiceSet[i] = new Mock<Dice>(new Random());
            }
            mock.Object.DiceSet = new Dice[] 
            { 
                MockDiceSet[0].Object, 
                MockDiceSet[1].Object, 
                MockDiceSet[2].Object, 
                MockDiceSet[3].Object, 
                MockDiceSet[4].Object
            };
            testFunctions.SetupMockDice(MockDiceSet, new int[] { 1, 2, 3, 4, 5 }); // default dice setup in case values does not matter
            sut = new Score(mock.Object);
        }

        private void VerifyDiceNumberGet()
        {
            foreach (Mock<Dice> mockDice in MockDiceSet)
            {
                mockDice.VerifyGet(m => m.Number);
            }
        }

        private void FillUpperScore()
        {
            sut.sixes = 30;
            sut.fives = 25;
            sut.fours = 20;
            sut.threes = 15;
            sut.twos = 10;
            sut.aces = 5;
        }

        private void FillLowerScore()
        {
            sut.onePair = 12;
            sut.twoPair = 22;
            sut.toak = 18;
            sut.foak = 24;
            sut.smallStraight = 15;
            sut.largeStraight = 20;
            sut.fullHouse = 28;
            sut.chance = 30;
            sut.yahtzee = 50;
        }

        private void FillScore()
        {
            FillUpperScore();
            FillLowerScore();
        }

        // ACES
        [TestMethod]
        public void SaveAcesShouldCountAllDiceWithOnesAndSaveSum()
        {
            testFunctions.SetupMockDice(MockDiceSet, new int[] { 1, 2, 3, 1, 4 });
            Assert.IsTrue(sut.SaveAces());

            VerifyDiceNumberGet();

            Assert.AreEqual(2, sut.aces);
        }

        [TestMethod]
        public void SaveAcesShouldSaveZeroIfNoOnes()
        {
            testFunctions.SetupMockDice(MockDiceSet, new int[] { 5, 5, 5, 5, 5 });
            Assert.IsTrue(sut.SaveAces());

            VerifyDiceNumberGet();

            Assert.AreEqual(0, sut.aces);
        }

        [TestMethod]
        public void SaveAcesShouldReturnFalseIfValueAlreadyExists()
        {
            Assert.IsTrue(sut.SaveAces());
            Assert.IsFalse(sut.SaveAces());
        }

        // TWOS
        [TestMethod]
        public void SaveTwosShouldCountAllDiceWithTwosAndSaveSum()
        {
            testFunctions.SetupMockDice(MockDiceSet, new int[] { 2, 3, 2, 2, 4 });
            Assert.IsTrue(sut.SaveTwos());

            VerifyDiceNumberGet();

            Assert.AreEqual(6, sut.twos);
        }

        [TestMethod]
        public void SaveTwosShouldSaveZeroIfNoTwos()
        {
            testFunctions.SetupMockDice(MockDiceSet, new int[] { 5, 5, 5, 5, 5 });
            Assert.IsTrue(sut.SaveTwos());

            VerifyDiceNumberGet();

            Assert.AreEqual(0, sut.twos);
        }

        [TestMethod]
        public void SaveTwosShouldReturnFalseIfValueAlreadyExists()
        {
            Assert.IsTrue(sut.SaveTwos());
            Assert.IsFalse(sut.SaveTwos());
        }

        // THREES
        [TestMethod]
        public void SaveThreesShouldCountAllDiceWithThreesAndSaveSum()
        {
            testFunctions.SetupMockDice(MockDiceSet, new int[] { 3, 3, 2, 2, 4 });
            Assert.IsTrue(sut.SaveThrees());

            VerifyDiceNumberGet();

            Assert.AreEqual(6, sut.threes);
        }

        [TestMethod]
        public void SaveThreesShouldSaveZeroIfNoThrees()
        {
            testFunctions.SetupMockDice(MockDiceSet, new int[] { 5, 5, 5, 5, 5 });
            Assert.IsTrue(sut.SaveThrees());

            VerifyDiceNumberGet();

            Assert.AreEqual(0, sut.threes);
        }

        [TestMethod]
        public void SaveThreesShouldReturnFalseIfValueAlreadyExists()
        {
            Assert.IsTrue(sut.SaveThrees());
            Assert.IsFalse(sut.SaveThrees());
        }

        // FOURS
        [TestMethod]
        public void SaveFoursShouldCountAllDiceWithFoursAndSaveSum()
        {
            testFunctions.SetupMockDice(MockDiceSet, new int[] { 4, 4, 2, 2, 4 });
            Assert.IsTrue(sut.SaveFours());

            VerifyDiceNumberGet();

            Assert.AreEqual(12, sut.fours);
        }

        [TestMethod]
        public void SaveFoursShouldSaveZeroIfNoFours()
        {
            testFunctions.SetupMockDice(MockDiceSet, new int[] { 5, 5, 5, 5, 5 });
            Assert.IsTrue(sut.SaveFours());

            VerifyDiceNumberGet();

            Assert.AreEqual(0, sut.fours);
        }

        [TestMethod]
        public void SaveFoursShouldReturnFalseIfValueAlreadyExists()
        {
            Assert.IsTrue(sut.SaveFours());
            Assert.IsFalse(sut.SaveFours());
        }

        // FIVES
        [TestMethod]
        public void SaveFivesShouldCountAllDiceWithFivesAndSaveSum()
        {
            testFunctions.SetupMockDice(MockDiceSet, new int[] { 5, 5, 2, 5, 4 });
            Assert.IsTrue(sut.SaveFives());

            VerifyDiceNumberGet();

            Assert.AreEqual(15, sut.fives);
        }

        [TestMethod]
        public void SaveFivesShouldSaveZeroIfNoFives()
        {
            testFunctions.SetupMockDice(MockDiceSet, new int[] { 1, 1, 1, 1, 1 });
            Assert.IsTrue(sut.SaveFives());

            VerifyDiceNumberGet();

            Assert.AreEqual(0, sut.fives);
        }

        [TestMethod]
        public void SaveFivesShouldReturnFalseIfValueAlreadyExists()
        {
            Assert.IsTrue(sut.SaveFives());
            Assert.IsFalse(sut.SaveFives());
        }

        // SIXES
        [TestMethod]
        public void SaveSixesShouldCountAllDiceWithSixesAndSaveSum()
        {
            testFunctions.SetupMockDice(MockDiceSet, new int[] { 6, 5, 2, 5, 4 });
            Assert.IsTrue(sut.SaveSixes());

            VerifyDiceNumberGet();

            Assert.AreEqual(6, sut.sixes);
        }

        [TestMethod]
        public void SaveSixesShouldSaveZeroIfNoSixes()
        {
            testFunctions.SetupMockDice(MockDiceSet, new int[] { 1, 1, 1, 1, 1 });
            Assert.IsTrue(sut.SaveSixes());

            VerifyDiceNumberGet();

            Assert.AreEqual(0, sut.sixes);
        }

        [TestMethod]
        public void SaveSixesShouldReturnFalseIfValueAlreadyExists()
        {
            Assert.IsTrue(sut.SaveSixes());
            Assert.IsFalse(sut.SaveSixes());
        }

        // UPPER BONUS
        [TestMethod]
        public void UpperBonusShouldReturn50IfUpperScoreEquals63OrMore()
        {
            FillUpperScore();
            sut.threes = null;

            Assert.AreEqual(50, sut.UpperBonus);
        }

        [TestMethod]
        public void UpperBonusShouldReturnNullIfUpperScoreEquals62OrLess()
        {
            Assert.IsNull(sut.UpperBonus);
        }

        // UPPER SCORE
        [TestMethod]
        public void UpperScoreShouldReturnTheTotalUpperScorePlusBonus()
        {
            FillUpperScore();

            Assert.AreEqual(155, sut.UpperScore);
        }

        [TestMethod]
        public void UpperScoreShouldReturnZeroIfNoScore()
        {
            Assert.AreEqual(0, sut.UpperScore);
        }

        [TestMethod]
        public void UpperScoreShouldReturnTotalEvenIfOnlyOneValueIsSet()
        {
            sut.threes = 6;

            Assert.AreEqual(6, sut.UpperScore);
        }

        // ONE PAIR
        [TestMethod]
        public void SaveOnePairShouldCountTheHighestPairOfDiceAndSaveSum()
        {
            testFunctions.SetupMockDice(MockDiceSet, new int[] { 6, 5, 2, 5, 6 });
            Assert.IsTrue(sut.SaveOnePair());

            VerifyDiceNumberGet();

            Assert.AreEqual(12, sut.onePair);
        }

        [TestMethod]
        public void SaveOnePairShouldSaveZeroIfNoPair()
        {
            Assert.IsTrue(sut.SaveOnePair());

            VerifyDiceNumberGet();

            Assert.AreEqual(0, sut.onePair);
        }

        [TestMethod]
        public void SaveOnePairShouldReturnFalseIfValueAlreadyExists()
        {
            Assert.IsTrue(sut.SaveOnePair());
            Assert.IsFalse(sut.SaveOnePair());
        }

        // TWO PAIR
        [TestMethod]
        public void SaveTwoPairShouldCountTwoPairsOfDiceAndSaveSum()
        {
            testFunctions.SetupMockDice(MockDiceSet, new int[] { 6, 5, 2, 5, 6 });
            Assert.IsTrue(sut.SaveTwoPair());

            VerifyDiceNumberGet();

            Assert.AreEqual(22, sut.twoPair);
        }

        [TestMethod]
        public void SaveTwoPairShouldSaveZeroIfNotEnoughPair()
        {
            testFunctions.SetupMockDice(MockDiceSet, new int[] { 6, 5, 2, 3, 6 });
            Assert.IsTrue(sut.SaveTwoPair());

            VerifyDiceNumberGet();

            Assert.AreEqual(0, sut.twoPair);
        }

        [TestMethod]
        public void SaveTwoPairShouldReturnFalseIfValueAlreadyExists()
        {
            Assert.IsTrue(sut.SaveTwoPair());
            Assert.IsFalse(sut.SaveTwoPair());
        }

        // THREE OF A KIND
        [TestMethod]
        public void SaveToaKShouldCountThreeDiceWithTheSameValueAndSaveSum()
        {
            testFunctions.SetupMockDice(MockDiceSet, new int[] { 5, 2, 5, 3, 5 });
            Assert.IsTrue(sut.SaveToaK());

            VerifyDiceNumberGet();

            Assert.AreEqual(15, sut.toak);
        }

        [TestMethod]
        public void SaveToaKShouldSaveZeroIfNotEnoughIdenticalDice()
        {
            testFunctions.SetupMockDice(MockDiceSet, new int[] { 6, 5, 2, 3, 6 });
            Assert.IsTrue(sut.SaveToaK());

            VerifyDiceNumberGet();

            Assert.AreEqual(0, sut.toak);
        }

        [TestMethod]
        public void SaveToaKShouldReturnFalseIfValueAlreadyExists()
        {
            Assert.IsTrue(sut.SaveToaK());
            Assert.IsFalse(sut.SaveToaK());
        }

        // FOUR OF A KIND
        [TestMethod]
        public void SaveFoaKShouldCountFourDiceWithTheSameValueAndSaveSum()
        {
            testFunctions.SetupMockDice(MockDiceSet, new int[] { 5, 2, 5, 5, 5 });
            Assert.IsTrue(sut.SaveFoaK());

            VerifyDiceNumberGet();

            Assert.AreEqual(20, sut.foak);
        }

        [TestMethod]
        public void SaveFoaKShouldSaveZeroIfNotEnoughIdenticalDice()
        {
            testFunctions.SetupMockDice(MockDiceSet, new int[] { 6, 6, 2, 3, 6 });
            Assert.IsTrue(sut.SaveFoaK());

            VerifyDiceNumberGet();

            Assert.AreEqual(0, sut.foak);
        }

        [TestMethod]
        public void SaveFoaKShouldReturnFalseIfValueAlreadyExists()
        {
            Assert.IsTrue(sut.SaveFoaK());
            Assert.IsFalse(sut.SaveFoaK());
        }

        // SMALL STRAIGHT
        [TestMethod]
        public void SaveSmallStraightShouldCheckForSmallStraightThenSave15()
        {
            testFunctions.SetupMockDice(MockDiceSet, new int[] { 1, 3, 2, 5, 4 });
            Assert.IsTrue(sut.SaveSmallStraight());

            VerifyDiceNumberGet();

            Assert.AreEqual(15, sut.smallStraight);
        }

        [TestMethod]
        public void SaveSmallStraightShouldSaveZeroIfNoSmallStraightExists()
        {
            testFunctions.SetupMockDice(MockDiceSet, new int[] { 1, 2, 3, 3, 6 });
            Assert.IsTrue(sut.SaveSmallStraight());

            VerifyDiceNumberGet();
            Assert.AreEqual(0, sut.smallStraight);
        }

        [TestMethod]
        public void SaveSmallStraightShouldReturnFalseIfValueAlreadyExists()
        {
            Assert.IsTrue(sut.SaveSmallStraight());
            Assert.IsFalse(sut.SaveSmallStraight());
        }

        // LARGE STRAIGHT
        [TestMethod]
        public void SaveLargeStraightShouldCheckForLargeStraightThenSave15()
        {
            testFunctions.SetupMockDice(MockDiceSet, new int[] { 6, 3, 2, 5, 4 });
            Assert.IsTrue(sut.SaveLargeStraight());

            VerifyDiceNumberGet();

            Assert.AreEqual(20, sut.largeStraight);
        }

        [TestMethod]
        public void SaveLargeStraightShouldSaveZeroIfNoLargeStraightExists()
        {
            testFunctions.SetupMockDice(MockDiceSet, new int[] { 1, 1, 2, 4, 6 });
            Assert.IsTrue(sut.SaveLargeStraight());

            VerifyDiceNumberGet();

            Assert.AreEqual(0, sut.largeStraight);
        }

        [TestMethod]
        public void SaveLargeStraightShouldReturnFalseIfValueAlreadyExists()
        {
            Assert.IsTrue(sut.SaveLargeStraight());
            Assert.IsFalse(sut.SaveLargeStraight());
        }

        // FULL HOUSE
        [TestMethod]
        public void SaveFullHouseShouldCheckForFullHouseThenSaveSum()
        {
            testFunctions.SetupMockDice(MockDiceSet, new int[] { 2, 2, 3, 2, 3 });
            Assert.IsTrue(sut.SaveFullHouse());

            VerifyDiceNumberGet();

            Assert.AreEqual(12, sut.fullHouse);
        }

        [TestMethod]
        public void SaveFullHouseShouldSaveZeroIfNoFullHouseExists()
        {
            testFunctions.SetupMockDice(MockDiceSet, new int[] { 2, 3, 5, 2, 3 });
            Assert.IsTrue(sut.SaveFullHouse());

            VerifyDiceNumberGet();

            Assert.AreEqual(0, sut.fullHouse);
        }

        [TestMethod]
        public void SaveFullHouseShouldReturnFalseIfValueAlreadyExists()
        {
            Assert.IsTrue(sut.SaveFullHouse());
            Assert.IsFalse(sut.SaveFullHouse());
        }

        // CHANCE
        [TestMethod]
        public void SaveChanceShouldCountAllDiceThenSaveSum()
        {
            Assert.IsTrue(sut.SaveChance());

            VerifyDiceNumberGet();

            Assert.AreEqual(15, sut.chance);
        }

        [TestMethod]
        public void SaveChanceShouldReturnFalseIfValueAlreadyExists()
        {
            Assert.IsTrue(sut.SaveChance());
            Assert.IsFalse(sut.SaveChance());
        }

        // YAHTZEE
        [TestMethod]
        public void SaveYahtzeeShouldCheckIfAllDiceHaveTheSameValueThenSave50()
        {
            testFunctions.SetupMockDice(MockDiceSet, new int[] { 1, 1, 1, 1, 1 });
            Assert.IsTrue(sut.SaveYahtzee());

            VerifyDiceNumberGet();

            Assert.AreEqual(50, sut.yahtzee);
        }

        [TestMethod]
        public void SaveYahtzeeShouldSaveZeroIfNotAllDiceHaveTheSameValue()
        {
            testFunctions.SetupMockDice(MockDiceSet, new int[] { 1, 1, 2, 1, 1 });
            Assert.IsTrue(sut.SaveYahtzee());

            VerifyDiceNumberGet();

            Assert.AreEqual(0, sut.yahtzee);
        }

        [TestMethod]
        public void SaveYahtzeeShouldReturnFalseIfValueAlreadyExists()
        {
            Assert.IsTrue(sut.SaveYahtzee());
            Assert.IsFalse(sut.SaveYahtzee());
        }

        // TOTAL SCORE
        [TestMethod]
        public void TotalScoreShouldReturnTheTotalScorePlusBonus()
        {
            FillScore();

            Assert.AreEqual(374, sut.TotalScore);
        }

        [TestMethod]
        public void TotalScoreShouldReturnZeroIfNoScore()
        {
            Assert.AreEqual(0, sut.TotalScore);
        }

        [TestMethod]
        public void TotalScoreShouldReturnTotalEvenIfOnlyOneValueIsSet()
        {
            sut.threes = 6;

            Assert.AreEqual(6, sut.TotalScore);
        }

        // RESET SCORE
        [TestMethod]
        public void ResetScoreShouldResetAllValuesToNull()
        {
            FillScore();

            sut.Reset();

            // Get each field from the sut, check it's value, and make sure all relevant values are null
            var fields = sut.GetType().GetFields();
            
            foreach (var field in fields) 
            {
                if (field.FieldType == typeof(Nullable<Int32>))
                {
                    Assert.IsNull(field.GetValue(sut));
                }
                else
                {
                    Assert.IsNotNull(field.GetValue(sut));
                }
            }
        }

        // IS FULL
        [TestMethod]
        public void IsFullShouldReturnTrueIfAllScoreValuesAreSet()
        {
            FillScore();

            Assert.IsTrue(sut.IsFull);
        }

        [TestMethod]
        public void IsFullShouldReturnFalseIfNotAllScoreValuesAreSet()
        {
            FillScore();

            sut.fullHouse = null;

            Assert.IsFalse(sut.IsFull);
        }
    }
}
