using BashSoft.Contracts;
using BashSoft.Network;

namespace BashSoft
{
    class BashSoftProgram
    {
        static void Main()
        {

            //Data.InitializeData();
            //Data.GetAllStudentsFromCourse("Balkanska");

            //IOManager.CreateDirectoryInCurrentFolder("Ivan");
            //IOManager.ChangeCurrentDirectoryAbsolute(@"D:\Download");
            //IOManager.TraverseDirectory(5);
            //InputReader.StartReadingCommands();

            IContentComparer tester = new Tester();
            IDownloadManager downloadManager = new DownloadManager();
            IDirectoyManager ioManager = new IOManager();
            IDatabase repo = new StudentRepository(new RepositorySorter(), new RepositoryFilter());
            IInterpreter currentInterpreter = new CommandInterpreter(tester,repo,downloadManager,ioManager);
            IReader reader = new InputReader(currentInterpreter);

            reader.StartReadingCommands();


        }
    }
}
