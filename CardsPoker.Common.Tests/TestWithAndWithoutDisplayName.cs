using System;
using System.ComponentModel;

namespace CardsPoker.Common.Tests
{
    public class TestWithAndWithoutDisplayName
    {
        [DisplayName("Test")]
        public int PropertyWithDisplayNameTest { get; set; }

        public int PropertyWithoutDisplayName { get; set; }
    }
}
