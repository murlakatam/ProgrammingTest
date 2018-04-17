using System;
using System.Collections.Generic;
using System.Text;
using CardsPocker.Common;

namespace CardsPoker.Common.Tests
{
    public class TestMenuOption : IMenuOption
    {
        public TestMenuOption(string title)
        {
            Title = title;
        }

        public TestMenuOption()
        {
        }

        public IMenuOption Parent { get; set; }
        public bool HasChildOptions { get; }
        public bool HasNoInput { get; }
        public string InputRequest { get; }
        public string Title { get; }
        public void RenderWithChildren(dynamic data)
        {
        }

        public void RenderOptionOnly(dynamic optionNumber)
        {
        }

        public void HandleInput(string input)
        {
        }
    }
}
