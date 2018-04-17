using CardsPocker.Common;
using CardsPoker.Common.Tests;
using CardsPoker.Console.Helpers;
using CardsPoker.Engine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CardsPoker.Console.Tests
{
    [TestClass]
    public class GameSettingHelperTests
    {
        [TestMethod]
        public void SuccessOn_ValidateInputAndAssignValueFor_Players_ValidWithDisplayName()
        {
            var settings = new GameSettings();
            var result = GameSettingHelper.ValidateInputAndAssignValueFor(settings, g => g.PlayersAmount, "1", 0, 10);

            Assert.IsNull(result);
            Assert.IsTrue(settings.PlayersAmount == 1);
        }

        [TestMethod]
        public void FailsOn_ValidateInputAndAssignValueFor_Players_HigherThanMax()
        {
            var settings = new Mock<GameSettings>();
            var result = GameSettingHelper.ValidateInputAndAssignValueFor(settings.Object, g => g.PlayersAmount, "10", 0, 1);

            Assert.IsTrue(result == $"Amount of players can't be higher than 1");
            settings.Verify(x => x.PlayersAmount, Times.Never);
        }

        [TestMethod]
        public void FailsOn_ValidateInputAndAssignValueFor_Players_LowerThanMin()
        {
            var settings = new Mock<GameSettings>();
            var result = GameSettingHelper.ValidateInputAndAssignValueFor(settings.Object, g => g.PlayersAmount, "0", 1, 2);

            Assert.IsTrue(result == $"Amount of players can't be lower than 1");
            settings.Verify(x => x.PlayersAmount, Times.Never);
        }

        [TestMethod]
        public void FailsOn_ValidateInputAndAssignValueFor_Players_NAN()
        {
            var settings = new Mock<GameSettings>();
            var result = GameSettingHelper.ValidateInputAndAssignValueFor(settings.Object, g => g.PlayersAmount, "asdf", 1, 2);

            Assert.IsTrue(result == "Can't parse Amount of players. Please try again");
            settings.Verify(x => x.PlayersAmount, Times.Never);
        }

        [TestMethod]
        public void FailsOn_ValidateInputAndAssignValueFor_Players_NAN_PropWithDisplayName()
        {
            var context = new TestWithAndWithoutDisplayName();
            var result = GameSettingHelper.ValidateInputAndAssignValueFor(context, g => g.PropertyWithDisplayNameTest, "asdf", 1, 2);

            Assert.IsTrue(result == "Can't parse Test. Please try again");
        }

        [TestMethod]
        public void FailsOn_ValidateInputAndAssignValueFor_Players_NAN_PropWithoutDisplayName()
        {
            var context = new TestWithAndWithoutDisplayName();
            var result = GameSettingHelper.ValidateInputAndAssignValueFor(context, g => g.PropertyWithoutDisplayName, "asdf", 1, 2);

            Assert.IsTrue(result == "Can't parse PropertyWithoutDisplayName. Please try again");
        }


        [TestMethod]
        public void FailsOn_MainMenuCommand_Nulls()
        {
            var result = GameSettingHelper.ValidateInputAndAssignValueFor<GameSettings, int>(null, null, null, 0, 0);
            Assert.IsTrue(result == Constants.UnhandledError);
        }
    }
}
