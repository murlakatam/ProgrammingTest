using System;
using System.Collections.Generic;
using System.Linq;
using CardsPocker.Common;

namespace CardsPoker.Console.Menu
{
    public abstract class MenuOption : IMenuOption
    {
        protected readonly IConsole Console;

        protected MenuOption(string title, IConsole console) 
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentNullException(nameof(title), Constants.TitleIsRequried);
            }

            Console = console ?? throw new ArgumentNullException(nameof(console));
            Title = title;
        }

        protected MenuOption(string title, string inputRequest, IConsole console) : this(title, console)
        {
            InputRequest = inputRequest;
        }

        internal readonly List<IMenuOption> ChildOptions = new List<IMenuOption>();

        public string Title { get; set; }
        public string InputRequest { get; set; }

        public IMenuOption Parent { get; set; }

        public bool HasChildOptions => ChildOptions.Count > 0;

        public bool HasNoInput { get; protected set; }

        public void AddChildOption(IMenuOption option)
        {
            ChildOptions.Add(option);
            option.Parent = this;
        }


        public virtual void HandleInput(string input)
        {
            Parent?.RenderWithChildren(string.Empty);
        }

        public virtual void RenderWithChildren(dynamic data) { }

        public virtual void RenderOptionOnly(dynamic optionNumber)
        {
            var optionNumberStr = (Object.ReferenceEquals(null, optionNumber) ? string.Empty : optionNumber.ToString());
            Console.WriteLine("Option " + optionNumberStr + Console.SpaceBetweenTitleAndDesc + Title);
        }

        public virtual void RenderChildOptions()
        {
            for (int i = 0; i < ChildOptions.Count; i++)
            {
                ChildOptions[i].RenderOptionOnly(i + 1);
            }
        }

        internal void SelectOptionByUserInput()
        {
            var selectedOption = PromptUserAndValidateInputForOptionIndexFormat();

            var option = ChildOptions[(int) selectedOption - 1];
            if (option.HasChildOptions || option.HasNoInput)
            {
                option.RenderWithChildren(string.Empty);
            }
            else
            {
                PromptForOptionValue(option);
            }
        }

        internal uint PromptUserAndValidateInputForOptionIndexFormat()
        {
            Console.Write(Constants.InputPrompt);
            uint selectedOption = 0;
            while (selectedOption == 0)
            {
                var optionSelection = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(optionSelection))
                {
                    PrintWrongOption();
                    continue;
                }

                optionSelection = optionSelection.Trim().Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();

                if (!uint.TryParse(optionSelection, out selectedOption))
                {
                    PrintWrongOption();
                    continue;
                }

                if (selectedOption > ChildOptions.Count)
                {
                    PrintWrongOption();
                    //clearing out wrong value
                    selectedOption = 0;
                }
            }

            return selectedOption;
        }

        internal void PromptForOptionValue(IMenuOption option)
        {
            Console.Clear();
            Console.WriteLine(option.InputRequest ?? option.Title);

            Console.Write(Constants.InputPrompt);
            option.HandleInput(Console.ReadLine());
        }

        internal void PrintWrongOption()
        {
            Console.WriteLine(Constants.InvalidInputOption);
        }
    }
}
