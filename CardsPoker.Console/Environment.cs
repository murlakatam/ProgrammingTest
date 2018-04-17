using System;
using CardsPocker.Common;

namespace CardsPoker.Console
{
    public class Environment : IEnvironment
    {
        public void Exit()
        {
            System.Environment.Exit(0);
        }
    }
}
