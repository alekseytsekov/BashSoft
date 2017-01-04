using System;
using System.Collections.Generic;
using System.IO;
using BashSoft.Contracts;
using BashSoft.Exceptions;


namespace BashSoft
{
    public class IOManager : IDirectoyManager
    {
        public void TraverseDirectory(int depth)
        {

            OutputWriter.WriteEmptyLine();
            int initialIdentation = SessionData.currentPath.Split('\\').Length;

            Queue<string> subFolders = new Queue<string>();

            subFolders.Enqueue(SessionData.currentPath);

            while (subFolders.Count != 0)
            {
                string currentPath = subFolders.Dequeue();
                
                int identation = currentPath.Split('\\').Length - initialIdentation;
                
                //OutputWriter.WriteMessageOnNewLine(currentPath);
                //OutputWriter.WriteMessageOnNewLine(string.Format("{0}{1}", new string('-', identation), currentPath));
                OutputWriter.WriteMessageOnNewLine(string.Format("{0}", currentPath));
                

                try
                {
                    foreach (var file in Directory.GetFiles(currentPath))
                    {
                        int indexOfLastSlash = file.LastIndexOf('\\');
                        string fileName = file.Substring(indexOfLastSlash);
                        OutputWriter.WriteMessageOnNewLine(string.Format("{0}{1}", new string('-', indexOfLastSlash), fileName));

                    }


                    // show directories 
                    foreach (var directory in Directory.GetDirectories(currentPath))
                    {
                        int indexOfLastSlash = directory.LastIndexOf('\\');
                        string directoryName = directory.Substring(indexOfLastSlash);
                        OutputWriter.WriteMessageOnNewLine(string.Format("{0}{1}", new string('*', indexOfLastSlash), directoryName));
                    }

                    if (depth - identation < 1) /// or 0 if SHOW DIRECTORIES IS OFF
                    {
                        break;
                    }

                    foreach (var directoryPath in Directory.GetDirectories(currentPath))
                    {
                        subFolders.Enqueue(directoryPath);
                    }
                }
                catch (UnauthorizedAccessException)
                {

                    OutputWriter.DisplayException(ExceptionMessages.UnauthorizedAccessEXceptionMessage);
                }

            }

            ///// my way 

            //DirectoryInfo firstDirectory = new DirectoryInfo(path);
            ////var ican = firstDirectory.GetAccessControl();
            //if (!firstDirectory.ToString().Contains("$RECYCLE.BIN") && !firstDirectory.ToString().Contains("System Volume Information"))
            //{
            //    DirectoryInfo[] allDirectories = firstDirectory.GetDirectories();

            //    Console.WriteLine(path);

            //    foreach (var filess in firstDirectory.GetFiles())
            //    {
            //        //Console.WriteLine(firstDirectory.ToString() + "\\" + filess.Name);
            //        OutputWriter.WriteMessageOnNewLine(firstDirectory.ToString() + "\\" + filess.Name);
            //    }

            //    if (allDirectories.Count() > 0)
            //    {
            //        foreach (var dir in allDirectories)
            //        {
                        
            //            try
            //            {
            //                DirectoryInfo nextDir = new DirectoryInfo(path + "\\" + dir.ToString());

            //                IOManager.TraverseDirectory(path + "\\" + dir.ToString());
            //            }
            //            catch (Exception e)
            //            {
            //                OutputWriter.DisplayException("Access denied !!! File with special status !!!");
            //            }

            //        }
            //    }
            //}
            
        }

        public void CreateDirectoryInCurrentFolder(string newFolderName)
        {
            try
            {
                string path = Path.Combine(SessionData.currentPath, newFolderName);

                Directory.CreateDirectory(path);
            }
            catch (ArgumentException)
            {
                //throw new ArgumentException(ExceptionMessages.ForbiddenSymbolsIsContainedInName);
                throw new InvalidFileNameException();
            }

        }

        public void ChangeCurrentDirectoryRelative(string relativePath)
        {
            if (relativePath == "..")
            {
                try
                {
                    string currentPath = SessionData.currentPath;

                    int indexOfLastSlash = currentPath.LastIndexOf("\\");
                    string newPath = currentPath.Substring(0, indexOfLastSlash);
                    SessionData.currentPath = newPath;
                }
                catch (ArgumentOutOfRangeException)
                {
                    throw new InvalidPathException();
                }

            }
            else
            {
                string currentPath = SessionData.currentPath;
                currentPath = Path.Combine(currentPath, relativePath);

                this.ChangeCurrentDirectoryAbsolute(currentPath);
            }
        }
        
        public void ChangeCurrentDirectoryAbsolute(string absolutePath)
        {
            if (!Directory.Exists(absolutePath))
            {
                //throw new DirectoryNotFoundException(ExceptionMessages.InvalidPath);
                throw new InvalidPathException();
            }

            SessionData.currentPath = absolutePath;

        }
        
        
    }
}
