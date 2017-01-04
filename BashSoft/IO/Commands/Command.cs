using System;
using BashSoft.Contracts;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    public abstract class Command : IExecutable
    {
        protected const int DefaultDirectoryDepth = 0;

        private string input;
        private string[] data;

        //private IContentComparer judge;
        //private IDatabase repository;
        //private IDownloadManager downloadManager;
        //private IDirectoyManager inputOutputManager;

        public Command(string input, string[] data)
        {
            this.Input = input;
            this.Data = data;
            //this.judge = judge;
            //this.repository = repository;
            //this.downloadManager = downloadManager;
            //this.inputOutputManager = ioManager;
        }

        public string Input
        {
            get { return this.input; }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidStringException();
                }

                this.input = value;
            }
        }

        public string[] Data
        {
            get { return this.data; }
            private set
            {
                if (value == null || value.Length == 0)
                {
                    throw new NullReferenceException();
                }

                this.data = value;
            }
        }

        public abstract void Execute();

        //protected IContentComparer Judge
        //{
        //    get { return this.judge; }
        //}

        //protected IDatabase Repository
        //{
        //    get { return this.repository; }
        //}

        //protected IDownloadManager DownloadManager
        //{
        //    get { return this.downloadManager; }
        //}

        //protected IDirectoyManager InputOutputManager
        //{
        //    get { return this.inputOutputManager; }
        //}
    }
}
