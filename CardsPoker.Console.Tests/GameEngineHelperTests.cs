using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsPocker.Common;
using CardsPoker.Console.Helpers;
using CardsPoker.Console.Menu;
using CardsPoker.Engine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CardsPoker.Console.Tests
{
    [TestClass]
    public class GameEngineHelperTests
    {
        [TestMethod]
        public void SuccessOn_GameEngineHelper_Round1()
        {
            var console = new Mock<IConsole>();
            var engine = new Mock<GameEngine>();

            engine.Setup(x => x.CurrentRound).Returns(1);
            engine.Setup(x => x.RoundsToBePlayed).Returns(2);

            GameEngineHelper.PromptForAKeyStrokeToContinue(engine.Object, console.Object);

            console.Verify(x => x.WriteLine(Constants.PressAnyKeyToContinue), Times.Once);
        }

        [TestMethod]
        public void SuccessOn_GameEngineHelper_EndRound()
        {
            var console = new Mock<IConsole>();
            var engine = new Mock<GameEngine>();

            engine.Setup(x => x.CurrentRound).Returns(2);
            engine.Setup(x => x.RoundsToBePlayed).Returns(2);

            GameEngineHelper.PromptForAKeyStrokeToContinue(engine.Object, console.Object);

            console.Verify(x => x.WriteLine(Constants.PressAnyKeyToEnd), Times.Once);
        }

        [TestMethod]
        public void FailsOn_GameEngineHelper_Nulls()
        {
            GameEngineHelper.PromptForAKeyStrokeToContinue(null, null);
        }


    }
}
