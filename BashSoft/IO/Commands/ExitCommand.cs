using System;
using BashSoft.Attributes;

namespace BashSoft.IO.Commands
{
    [Alias("exit")]
    public class ExitCommand : Command
    {
        public ExitCommand(string input, string[] data) : base(input, data)
        {
        }

        public override void Execute()
        {
            Environment.Exit(0);
        }
    }
}
