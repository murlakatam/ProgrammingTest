using System;
using System.Collections.Generic;
using CardsPocker.Common;
using CardsPoker.Common.Tests;
using CardsPoker.Console;
using CardsPoker.Console.Menu;
using CardsPoker.Engine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CardsPoker.Tests
{
    [TestClass]
    public class MainMenuTests
    {
        [TestMethod]
        public void SuccessOn_MainMenuCommand_Created()
        {
            var console = new Mock<IConsole>();
            var settings = new Mock<GameSettings>();
            var engine = new Mock<GameEngine>();
            var options = new List<IMenuOption>
            {
                new Mock<IMenuOption>().Object,
                new Mock<IMenuOption>().Object,
                new Mock<IMenuOption>().Object
            };
            var context = new MainMenuOption("test", "details", options, settings.Object, engine.Object, console.Object);

            console.Verify(x => x.SetupConsole(), Times.Once);
            Assert.IsTrue(context.HasChildOptions);
            Assert.IsTrue(context.ChildOptions.Count == 3);
            Assert.IsTrue(context.Title == "test");
            Assert.IsTrue(context._details == "details");
            Assert.IsNull(context.Parent);
            Assert.IsNull(context.InputRequest);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Throws_MainMenuCommand_WithNoTitle()
        {
            var console = new Mock<IConsole>();
            var settings = new Mock<GameSettings>();
            var engine = new Mock<GameEngine>();
            var context = new MainMenuOption(null, "details", null, settings.Object, engine.Object, console.Object);
        }

        [TestMethod]
        public void SuccessOn_MainMenuCommand_ShowHeaders()
        {
            var console = new TestConsole();
            var settings = new Mock<GameSettings>();
            settings.Setup(x => x.PokerCards).Returns(() => 2);
            var engine = new Mock<GameEngine>();
            var context = new MainMenuOption("test2", "details to appear in console output", new List<IMenuOption>(),  settings.Object, engine.Object, console);
            context.ShowHeaders();
            Assert.IsTrue(context.Title == "test2");
            var consoleOutput = console.Output.ToString();
            Assert.IsTrue(consoleOutput.Contains("details to appear in console output"));
        }

        [TestMethod]
        public void SuccessOn_MainMenuCommand_PromptUserAndValidateInput()
        {
            var console = new Mock<IConsole>();
            var settings = new Mock<GameSettings>();
            var engine = new Mock<GameEngine>();
            var options = new List<IMenuOption>
            {
                new Mock<IMenuOption>().Object,
                new Mock<IMenuOption>().Object,
                new Mock<IMenuOption>().Object
            };
            console.SetupSequence(x => x.ReadLine()).Returns(null).Returns("-123 asdf").Returns("5").Returns("1");

            var context = new MainMenuOption("test", "details", options, settings.Object, engine.Object, console.Object);

            var result = context.PromptUserAndValidateInputForOptionIndexFormat();

            console.Verify(x => x.WriteLine(It.Is<string>(message => message.Equals(Constants.InvalidInputOption))), Times.Exactly(3));
            Assert.IsTrue(result == 1);
        }

        [TestMethod]
        public void SuccessOn_MainMenuCommand_SelectOptionByUserInput_HasChildOptions()
        {
            var console = new Mock<IConsole>();
            var settings = new Mock<GameSettings>();
            var engine = new Mock<GameEngine>();
            var menuOption = new Mock<IMenuOption>();

            menuOption.Setup(x => x.HasChildOptions).Returns(true);

            var options = new List<IMenuOption>
            {
                menuOption.Object
            };

            console.Setup(x => x.ReadLine()).Returns("1");

            var context = new MainMenuOption("test", "details", options, settings.Object, engine.Object, console.Object);

            context.SelectOptionByUserInput();

            console.Verify(x => x.WriteLine(It.Is<string>(message => message.Equals(Constants.InvalidInputOption))), Times.Never);
            menuOption.Verify(x => x.RenderWithChildren(It.Is<string>(p => p == string.Empty)), Times.Once);

        }

        [TestMethod]
        public void SuccessOn_MainMenuCommand_SelectOptionByUserInput_HasNoInput()
        {
            var console = new Mock<IConsole>();
            var settings = new Mock<GameSettings>();
            var engine = new Mock<GameEngine>();
            var menuOption = new Mock<IMenuOption>();

            menuOption.Setup(x => x.HasNoInput).Returns(true);

            var options = new List<IMenuOption>
            {
                menuOption.Object
            };

            console.Setup(x => x.ReadLine()).Returns("1");

            var context = new MainMenuOption("test", "details", options, settings.Object, engine.Object, console.Object);

            context.SelectOptionByUserInput();

            console.Verify(x => x.WriteLine(It.Is<string>(message => message.Equals(Constants.InvalidInputOption))), Times.Never);
            menuOption.Verify(x => x.RenderWithChildren(It.Is<string>(p => p == string.Empty)), Times.Once);

        }

        [TestMethod]
        public void SuccessOn_MainMenuCommand_SelectOptionByUserInput_PromptForOptionValue()
        {
            var console = new Mock<IConsole>();
            var settings = new Mock<GameSettings>();
            var engine = new Mock<GameEngine>();
            var menuOption = new Mock<IMenuOption>();

            var testInput = "TestInput";
            menuOption.Setup(x => x.InputRequest).Returns("Give me test input");

            var options = new List<IMenuOption>
            {
                menuOption.Object
            };

            console.SetupSequence(x => x.ReadLine()).Returns("1").Returns(testInput);

            var context = new MainMenuOption("test", "details", options, settings.Object, engine.Object, console.Object);

            context.SelectOptionByUserInput();

            console.Verify(x => x.WriteLine(It.Is<string>(message => message.Equals("Give me test input"))), Times.Once);
            console.Verify(x => x.Write(It.Is<string>(message => message.Equals(Constants.InputPrompt))), Times.Exactly(2));
            menuOption.Verify(x => x.HandleInput(It.Is<string>(p => p == testInput)), Times.Once);
        }

        [TestMethod]
        public void SuccessOn_MainMenuCommand_RenderChildOptions()
        {
            var console = new Mock<IConsole>();
            var settings = new Mock<GameSettings>();
            var engine = new Mock<GameEngine>();
            var menuOption = new Mock<IMenuOption>();

            var options = new List<IMenuOption>
            {
                menuOption.Object
            };


            var context = new MainMenuOption("test", "details", options, settings.Object, engine.Object, console.Object);

            context.RenderChildOptions();

            menuOption.Verify(x => x.RenderOptionOnly(It.Is<int>(p => p == 1)), Times.Once);
        }

        [TestMethod]
        public void SuccessOn_MainMenuCommand_RenderOptionOnly_ValidOption()
        {
            var console = new Mock<IConsole>();
            var settings = new Mock<GameSettings>();
            var engine = new Mock<GameEngine>();

            console.Setup(x => x.SpaceBetweenTitleAndDesc).Returns(" space ");

            var testOptionNumber = 1;

            var context = new MainMenuOption("title", "details", new List<IMenuOption>(), settings.Object, engine.Object, console.Object);

            context.RenderOptionOnly(testOptionNumber);

            console.Verify(x => x.WriteLine(It.Is<string>(p => p == "Option 1 space title")), Times.Once);
        }

        [TestMethod]
        public void FailsOn_MainMenuCommand_RenderOptionOnly_Nulls()
        {
            var console = new Mock<IConsole>();
            var settings = new Mock<GameSettings>();
            var engine = new Mock<GameEngine>();

            console.Setup(x => x.SpaceBetweenTitleAndDesc).Returns((string)null);


            var context = new MainMenuOption("title", "details", new List<IMenuOption>(), settings.Object, engine.Object, console.Object);

            context.RenderOptionOnly(null);

            console.Verify(x => x.WriteLine(It.Is<string>(p => p == "Option title")), Times.Once);
        }
    }
}
