using System;
using System.IO;
using System.Threading;
namespace FileWatcherService
{
    class Logger
    {
        FileSystemWatcher watcher;
        object obj = new object();
        bool enabled = true;
        public Logger()
        {
            // you can change this directory to your own !!
            watcher = new FileSystemWatcher("D:\\Temp");
            watcher.Deleted += Watcher_Deleted;
            watcher.Created += Watcher_Created;
            watcher.Changed += Watcher_Changed;
            watcher.Renamed += Watcher_Renamed;
        }

        public void Start()
        {
            watcher.EnableRaisingEvents = true;
            while (enabled)
            {
                Thread.Sleep(1000);
            }
        }
        public void Stop()
        {
            watcher.EnableRaisingEvents = false;
            enabled = false;
        }
        // on rename files
        private void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            string fileEvent = "Name changed to " + e.FullPath;
            string filePath = e.OldFullPath;
            RecordEntry(fileEvent, filePath);
        }
        // on changing files
        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "changed";
            string filePath = e.FullPath;
            RecordEntry(fileEvent, filePath);
        }
        // on creating files
        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "created";
            string filePath = e.FullPath;
            RecordEntry(fileEvent, filePath);
        }
        // on deleting files
        private void Watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "deleted";
            string filePath = e.FullPath;
            RecordEntry(fileEvent, filePath);
        }

        private void RecordEntry(string fileEvent, string filePath)
        {
            // locking that object because many users can create, change, rename etc.
            lock (obj)
            {
                using (StreamWriter writer = new StreamWriter("D:\\templog.txt", true))
                {
                    writer.WriteLine(string.Format("{0} file {1} was {2}",
                    DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"), filePath, fileEvent));
                    writer.Flush();
                }
            }
        }
    }
}

