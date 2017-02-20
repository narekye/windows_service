using System.ServiceProcess;
using System.Threading;

namespace FileWatcherService
{
    public partial class Service1 : ServiceBase
    {
        private Logger logger;
        public Service1()
        {
            ServiceName = "Service1";
            InitializeComponent();
            CanStop = true;
            CanPauseAndContinue = true;
            AutoLog = true;
        }

        protected override void OnStart(string[] args)
        {
            logger = new Logger();
            Thread loggerThread = new Thread(logger.Start);
            loggerThread.Start();
        }

        protected override void OnStop()
        {
            logger.Stop();
            Thread.Sleep(1000);
        }
    }
}
