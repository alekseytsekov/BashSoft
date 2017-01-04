﻿using BashSoft.Attributes;
using BashSoft.Contracts;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    [Alias("cmp")]
    public class CompareFilesCommand : Command
    {
        [InjectAttribute]
        private IContentComparer judge;
        
        public CompareFilesCommand(string input, string[] data) : base(input, data)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 3)
            {
                throw new InvalidCommandException(this.Input);
            }

            string outputPath = this.Data[1];
            string expectedOutputPath = this.Data[2];

            this.judge.CompareContent(outputPath, expectedOutputPath);
            
        }
    }
}
