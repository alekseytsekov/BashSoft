using BashSoft.Attributes;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    [Alias("help")]
    public class GetHelpCommand : Command
    {
        public GetHelpCommand(string input, string[] data) : base(input, data)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 1)
            {
                throw new InvalidCommandException(this.Input);
            }

            this.DisplayHelp(this.Input, this.Data);
        }

        private void DisplayHelp(string input, string[] data)
        {
            OutputWriter.WriteMessageOnNewLine($"{new string('_', 127)}");
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -125}|", "mkdir 'path' - make directory"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -125}|", "ls 'depth' OR ls - traverse directory"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -125}|", "cmp 'path1' 'path2' - comparing two files"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -125}|", "cdrel .. OR cdrel 'FolderName' - change directory"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -125}|", "cdabs 'absolute path' - change directory"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -125}|", "readdb 'file name' - read students data base"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -125}|", "dropdb 'file name' - drop students data base"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -125}|", "filter {courseName} excelent/average/poor  take 2/5/all students - filterExcelent (the output is written on the console)"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -125}|", "order increasing students - order {courseName} ascending/descending take 20/10/all (the output is written on the console)"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -125}|", "download file - download: path of file (saved in current directory)"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -125}|", "download file asinchronously - downloadAsynch: path of file (save in the current directory)"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -125}|", "show course name - show: courseName (username) – user name may be omitted"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -125}|", "display students/courses ascending/descending -  display data entities"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -125}|", "get help – help"));
            OutputWriter.WriteMessageOnNewLine($"{new string('_', 127)}");
            OutputWriter.WriteEmptyLine();
        }
    }
}
