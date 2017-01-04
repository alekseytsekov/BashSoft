using System.Net;
using System.Threading.Tasks;
using BashSoft.Contracts;
using BashSoft.Exceptions;

namespace BashSoft.Network
{
    public class DownloadManager : IDownloadManager
    {
        private WebClient webClient;

        public DownloadManager()
        {
            webClient = new WebClient();
        }

        public void Download(string fileUrl)
        {
            WebClient webClient = new WebClient();

            try
            {
                OutputWriter.WriteMessageOnNewLine("Started downloading:");

                string fileName = ExtractNameOfFile(fileUrl);
                string pathToDownload = SessionData.currentPath + "/" + fileName;

                webClient.DownloadFile(fileUrl, pathToDownload);

                OutputWriter.WriteMessageOnNewLine("Download complete.");
            }
            catch (WebException ex)
            {
                OutputWriter.DisplayException(ex.Message);
            }
        }

        private string ExtractNameOfFile(string fileURL)
        {
            int indexOfLastBackSlash = fileURL.LastIndexOf("/");

            if (indexOfLastBackSlash != -1)
            {
                
            }
            else
            {
                //throw new WebException(ExceptionMessages.InvalidPath);
                throw new InvalidPathException();
            }

            return fileURL.Substring(indexOfLastBackSlash + 1);
        }

        public void DownloadAsync(string fileURL)
        {
            Task.Run(() => Download(fileURL));
        }
    }
}
