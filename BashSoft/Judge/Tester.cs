using System;
using System.IO;
using BashSoft.Contracts;
using BashSoft.Exceptions;


namespace BashSoft
{
    public class Tester : IContentComparer
    {
        public void CompareContent(string userOutputPath, string expectedOutputPath)
        {
            OutputWriter.WriteMessageOnNewLine("Reading files...");

            try
            {
                string mismatchPath = this.GetMismatchPath(expectedOutputPath);

                string[] actualOutputLines = File.ReadAllLines(userOutputPath);
                string[] expectedOutputLines = File.ReadAllLines(expectedOutputPath);

                bool hasMismatch;

                string[] mismatches = this.GetLinesWithPossibleMismatches(actualOutputLines, expectedOutputLines, out hasMismatch);

                this.PrintOutput(mismatches, hasMismatch, mismatchPath);

                OutputWriter.WriteMessageOnNewLine("Files read!");
                
            }
            catch (IOException)
            {
                //throw new IOException(ExceptionMessages.InvalidPath);
                throw new InvalidPathException();
            }
        }

        private void PrintOutput(string[] mismatches, bool hasMismatch, string mismatchPath)
        {
            Console.WriteLine();
            if (hasMismatch)
            {
                foreach (var mismatch in mismatches)
                {
                    OutputWriter.WriteMessageOnNewLine(mismatch);

                    try
                    {
                        File.WriteAllLines(mismatchPath, mismatches);
                    }
                    catch (DirectoryNotFoundException)
                    {
                        //OutputWriter.DisplayException(ExceptionMessages.InvalidPath);
                        throw new InvalidPathException();
                    }
                    
                }
                return; ///// ??????
            }
            else
            {
                OutputWriter.WriteMessageOnNewLine("Files are identical. There are no mismatches.");
            }
        }

        private string[] GetLinesWithPossibleMismatches(string[] actualOutputLines, string[] expectedOutputLines, out bool hasMismatch)
        {
            hasMismatch = false;
            bool noLine = false;

            int minOutputLines = actualOutputLines.Length;
            if (actualOutputLines.Length != expectedOutputLines.Length)
            {
                hasMismatch = true;

                minOutputLines = Math.Min(actualOutputLines.Length, expectedOutputLines.Length);

                OutputWriter.DisplayException(ExceptionMessages.ComprasionOfFilesWithDiffrentSizes);
            }

            string output = string.Empty;

            string[] mismatches = new string[actualOutputLines.Length];

            OutputWriter.WriteMessageOnNewLine("Comparing files...");


            //// actualOutputLines.Length;
            for (int i = 0; i < minOutputLines; i++)
            {
                string actualLine = actualOutputLines[i];
                string expectedLine = string.Empty;

                if (i < expectedOutputLines.Length)
                {
                    expectedLine = expectedOutputLines[i];
                }
                else
                {
                    noLine = true;
                }
                

                if (!actualLine.Equals(expectedLine) || noLine)
                {
                    output = string.Format(@"Mismatch at line {0} -- expected :""{1}"", actual: ""{2}"" ", i, actualLine, expectedLine);
                    output += Environment.NewLine;
                    hasMismatch = true;
                }
                else
                {
                    output = actualLine;
                    output += Environment.NewLine;
                }

                mismatches[i] = output;
            }

            return mismatches;

        }
        
        private string GetMismatchPath(string expectedOutputPath)
        {
            int indexOf = expectedOutputPath.LastIndexOf('\\');
            string directoryPath = expectedOutputPath.Substring(0, indexOf);

            string finalPath = Path.Combine(directoryPath, @"Mismatches.txt");

            return finalPath;
        }

    }
}
