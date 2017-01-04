using System;
using System.Linq;
using System.Reflection;
using BashSoft.Attributes;
using BashSoft.Contracts;
using BashSoft.IO.Commands;

namespace BashSoft
{
    public  class CommandInterpreter : IInterpreter
    {
        private const int DefaultDirectoryDepth = 0;
        private IContentComparer judge;
        private IDatabase repository;
        private IDownloadManager downloadManager;
        private IDirectoyManager inputOutputManager;

        public CommandInterpreter(IContentComparer judge, IDatabase repository, IDownloadManager downloadManager, IDirectoyManager inputOutputManager)
        {
            this.judge = judge;
            this.repository = repository;
            this.downloadManager = downloadManager;
            this.inputOutputManager = inputOutputManager;
        }

        public void InterpreterCommand(string input)
        {
            //string[] data = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string[] data = input.Split();

            string commandName = data[0];

            try
            {
                IExecutable command = this.ParseCommand(input, commandName, data);
                command.Execute();
            }
            catch (Exception ex)
            {
                OutputWriter.DisplayException(ex.Message);
            }

            //commandName = commandName.ToLower();

            //try
            //{
            //    this.ParseCommand(input, commandName, data);
            //}
            //catch (DirectoryNotFoundException dnfe)
            //{
            //    OutputWriter.DisplayException(dnfe.Message);
            //}
            //catch (ArgumentOutOfRangeException aofre)
            //{
            //    OutputWriter.DisplayException(aofre.Message);
            //}
            //catch (ArgumentException ae)
            //{
            //    OutputWriter.DisplayException(ae.Message);
            //}
            //catch (Exception e)
            //{
            //    OutputWriter.DisplayException(e.Message);
            //}

        }

        private IExecutable ParseCommand(string input, string commandName, string[] data)
        {
            object[] parametersForConstruction = new object[] {input, data};

            Type typeOfcommand =
                Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .First(t => t.GetCustomAttributes(typeof(AliasAttribute))
                                .Where(a => a.Equals(commandName))
                                .ToArray()
                                .Length > 0);

            Type typeOfInterpreter = typeof(CommandInterpreter);

            Command exe = (Command) Activator.CreateInstance(typeOfcommand, parametersForConstruction);

            FieldInfo[] fieldsOfCommand = typeOfcommand.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            FieldInfo[] fieldsOfInterpreter = typeOfInterpreter.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

            foreach (var fieldOfCommand in fieldsOfCommand)
            {
                Attribute attr = fieldOfCommand.GetCustomAttribute(typeof(InjectAttribute));

                if (attr != null)
                {
                    if (fieldsOfInterpreter.Any(x=> x.FieldType == fieldOfCommand.FieldType))
                    {
                        fieldOfCommand.SetValue(exe, fieldsOfInterpreter.First(x=>x.FieldType == fieldOfCommand.FieldType).GetValue(this));
                    }
                }
            }

            return exe;
            //switch (commandName)
            //{
            //    case "open":

            //         return new OpenFileCommand(input, data);

            //        //TryOpenFile(input, data);
            //        //break;

            //    case "mkdir":

            //        return new MakeDirectoryCommand(input, data);

            //        //TryCreateDirectory(input, data);
            //        //break;

            //    case "ls":

            //        return new TraverseFoldersCommand(input, data);

            //    //TryTraverseFolders(input, data);
            //    //break;

            //    case "cmp":

            //        return new CompareFilesCommand(input, data);


            //    //TryCompareFiles(input, data);
            //    //break;

            //    case "cdrel":

            //        return new ChangeRelativePathCommand(input, data);

            //    //TryChangePathRelatively(input, data);
            //    //break;

            //    case "cdabs":

            //        return new ChangeAbsolutePathCommand(input, data);

            //    //TryChangePathAbsolute(input, data);
            //    //break;

            //    case "readdb":

            //        return new ReadDatabaseCommand(input, data);

            //    //TryReadDatabaseFromFile(input, data);
            //    //break;

            //    case "dropdb":
            //        return new DropDatabaseCommand(input, data);

            //    //TryDropDb(input, data);
            //    //break;

            //    case "help":

            //        return new GetHelpCommand(input, data);

            //    //TryGetHelp(input, data);
            //    //break;

            //    case "filter":

            //        return new PrintFilteredStudentsCommand(input, data);

            //    //TryFilterAndTake(input, data);
            //    //break;

            //    case "order":

            //        return new PrintOrderedStudentsCommand(input, data);

            //    //TryOrderAndTake(input, data);
            //    //break;


            //    //case "decOrder":
            //    ////TODO
            //    //break;

            //    case "download":

            //        return new DownloadFileCommand(input, data);

            //    //TryDownloadRequestedFile(input, data);
            //    //break;

            //    case "downloadAsynch":

            //        return new DownloadAsynchCommand(input, data);

            //    //TryDownloadRequestedFileAsync(input, data);
            //    //break;

            //    case "show":

            //        return new ShowCourseCommand(input, data);

            //    //TryShowWantedData(input, data);
            //    //break;

            //    case "display":
            //        return new DisplayCommand(input, data);

            //    case "exit":
            //        return new ExitCommand(input, data);

            //    default:
            //        throw new InvalidCommandException(input);
            //    //this.DisplayInvalidCommandMessage(input);
            //    //break;
            //}
        }


        //private void DisplayInvalidCommandMessage(string input)
        //{
        //    OutputWriter.DisplayException(string.Format("The command '{0}' is invalid", input));
        //}


        //private  void TryShowWantedData(string input, string[] data)
        //{
        //    if (data.Length == 2)
        //    {
        //        string courseName = data[1];

        //        this.repository.GetAllStudentsFromCourse(courseName);
        //    }
        //    else if (data.Length == 3)
        //    {
        //        string courseName = data[1];
        //        string studentName = data[2];

        //        this.repository.GetStudentScoresFromCourse(courseName, studentName);
        //    }
        //    else
        //    {
        //        this.DisplayInvalidCommandMessage(input);
        //    }
        //}

        //private  void TryGetHelp(string input, string[] data)
        //{
        //    OutputWriter.WriteMessageOnNewLine($"{new string('_', 127)}");
        //    OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -125}|", "make directory - mkdir: path "));
        //    OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -125}|", "traverse directory - ls: depth "));
        //    OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -125}|", "comparing files - cmp: path1 path2"));
        //    OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -125}|", "change directory - cdrel:relative path"));
        //    OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -125}|", "change directory - cdabs:absolute path"));
        //    OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -125}|", "read students data base - readdb: path"));
        //    OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -125}|", "drop students data base - dropdb: path"));
        //    OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -125}|", "filter {courseName} excelent/average/poor  take 2/5/all students - filterExcelent (the output is written on the console)"));
        //    OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -125}|", "order increasing students - order {courseName} ascending/descending take 20/10/all (the output is written on the console)"));
        //    OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -125}|", "download file - download: path of file (saved in current directory)"));
        //    OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -125}|", "download file asinchronously - downloadAsynch: path of file (save in the current directory)"));
        //    OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -125}|", "show course name - show: courseName (username) – user name may be omitted"));
        //    OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -125}|", "get help – help"));
        //    OutputWriter.WriteMessageOnNewLine($"{new string('_', 127)}");
        //    OutputWriter.WriteEmptyLine();
        //}

        //private  void TryReadDatabaseFromFile(string input, string[] data)
        //{
        //    string fileName = data[1];
        //    this.repository.LoadData(fileName);
        //}

        //private void TryDropDb(string input, string[] data)
        //{
        //    if (data.Length != 1)
        //    {
        //        this.DisplayInvalidCommandMessage(input);
        //        return;
        //    }

        //    this.repository.UnloadData();
        //    OutputWriter.WriteMessageOnNewLine("Database dropped!");
        //}

        //private  void TryChangePathAbsolute(string input, string[] data)
        //{
        //    string absolutePath = data[1];

        //    this.inputOutputManager.ChangeCurrentDirectoryAbsolute(absolutePath);
        //}

        //private  void TryChangePathRelatively(string input, string[] data)
        //{
        //    string relativePath = data[1];

        //    this.inputOutputManager.ChangeCurrentDirectoryRelative(relativePath);
        //}

        //private  void TryCompareFiles(string input, string[] data)
        //{
        //    if (data.Length == 3)
        //    {
        //        string outputPath = data[1];
        //        string expectedOutputPath = data[2];

        //        this.judge.CompareContent(outputPath, expectedOutputPath);
        //    }
        //    else
        //    {
        //        this.DisplayInvalidCommandMessage(input);
        //    }
        //}

        //private  void TryTraverseFolders(string input, string[] data)
        //{
        //    if (data.Length == 1)
        //    {
        //        this.inputOutputManager.TraverseDirectory(DefaultDirectoryDepth);
        //    }
        //    else if (data.Length == 2)
        //    {
        //        int depth;

        //        bool hasParsed = int.TryParse(data[1], out depth);

        //        if (hasParsed)
        //        {
        //            this.inputOutputManager.TraverseDirectory(depth);
        //        }
        //        else
        //        {
        //            OutputWriter.DisplayException(ExceptionMessages.UnableToParseNumber);
        //        }
        //    }
        //    else
        //    {
        //        DisplayInvalidCommandMessage(input);
        //    }
        //}

        //private  void TryCreateDirectory(string input, string[] data)
        //{
        //    if (data.Length == 2)
        //    {
        //        string folderName = data[1];
        //        this.inputOutputManager.CreateDirectoryInCurrentFolder(folderName);
        //    }
        //    else
        //    {
        //        DisplayInvalidCommandMessage(input);
        //    }
        //}

        //private  void TryOpenFile(string input, string[] data)
        //{

        //    //if (data.Length == 2)
        //    //{
        //    //    string fileName = data[1];
        //    //    Process.Start(SessionData.currentPath + "\\" + fileName);
        //    //}
        //    //else
        //    //{
        //    //    this.DisplayInvalidCommandMessage(input);
        //    //}

        //}

        //private  void TryFilterAndTake(string input, string[] data)
        //{
        //    if (data.Length == 5)
        //    {
        //        string courseName = data[1];
        //        string filter = data[2].ToLower();
        //        string takeCommand = data[3].ToLower();
        //        string takeQuantity = data[4].ToLower();

        //        TryParseParametersForFilterAndTake(takeCommand, takeQuantity, courseName, filter);
        //    }
        //    else
        //    {
        //        this.DisplayInvalidCommandMessage(input);
        //    }
        //}

        //private  void TryParseParametersForFilterAndTake(string takeCommand, string takeQuantity, string courseName, string filter)
        //{
        //    if (takeCommand == "take")
        //    {
        //        if (takeQuantity == "all")
        //        {
        //            this.repository.FilterAndTake(courseName, filter);
        //        }
        //        else
        //        {
        //            int studentsToTake;

        //            bool hasParsed = int.TryParse(takeQuantity, out studentsToTake);

        //            if (hasParsed)
        //            {
        //                this.repository.FilterAndTake(courseName, filter, studentsToTake);
        //            }
        //            else
        //            {
        //                OutputWriter.DisplayException(ExceptionMessages.InvalidTakeQuantityParameter);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        OutputWriter.DisplayException(ExceptionMessages.InvalidTakeQuantityParameter);
        //    }
        //}

        //private  void TryOrderAndTake(string input, string[] data)
        //{
        //    if (data.Length == 5)
        //    {
        //        string courseName = data[1];
        //        string filter = data[2].ToLower();
        //        string takeCommand = data[3].ToLower();
        //        string takeQuantity = data[4].ToLower();

        //        TryParseParametersForOrderAndTake(takeCommand, takeQuantity, courseName, filter);
        //    }
        //    else
        //    {
        //        this.DisplayInvalidCommandMessage(input);
        //    }
        //}

        //private  void TryParseParametersForOrderAndTake(string takeCommand, string takeQuantity, string courseName, string filter)
        //{
        //    if (takeCommand == "take")
        //    {
        //        if (takeQuantity == "all")
        //        {
        //            this.repository.OrderAndTake(courseName, filter);
        //        }
        //        else
        //        {
        //            int studentsToTake;

        //            bool hasParsed = int.TryParse(takeQuantity, out studentsToTake);

        //            if (hasParsed)
        //            {
        //                this.repository.OrderAndTake(courseName, filter, studentsToTake);
        //            }
        //            else
        //            {
        //                OutputWriter.DisplayException(ExceptionMessages.InvalidTakeQuantityParameter);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        OutputWriter.DisplayException(ExceptionMessages.InvalidTakeQuantityParameter);
        //    }
        //}

        //private  void TryDownloadRequestedFileAsync(string input, string[] data)
        //{
        //    if (data.Length == 2)
        //    {
        //        string url = data[1];
        //        this.downloadManager.DownloadAsync(url);
        //    }
        //    else
        //    {
        //        this.DisplayInvalidCommandMessage(input);
        //    }
        //}

        //private  void TryDownloadRequestedFile(string input, string[] data)
        //{
        //    if (data.Length == 2)
        //    {
        //        string url = data[1];
        //        this.downloadManager.Download(url);
        //    }
        //    else
        //    {
        //        this.DisplayInvalidCommandMessage(input);
        //    }
        //}
    }
}
