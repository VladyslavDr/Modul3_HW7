using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoggerAsync
{
    public class LoggerSingleton
    {
        private static readonly LoggerSingleton _instance = new LoggerSingleton();
        private StringBuilder _textLog = new StringBuilder();
        private int _countLog = 1;
        private static Semaphore _sem = new Semaphore(1, 1);
        private LoggerSingleton()
        {
        }

        public event Action BackupHendler;
        public StringBuilder DatabaseLogs => _textLog;

        public static LoggerSingleton GetInstance()
        {
            return _instance;
        }

        public void Log(LogType logType, string textLog)
        {
            var log = $"{DateTime.Now}: {logType}: {textLog}{Environment.NewLine}";

            _sem.WaitOne();
            var currentCount = _countLog++;
            _sem.Release();

            _textLog.Append($"{currentCount}) " + log);

            Console.WriteLine($"count = {currentCount}");

            if (currentCount % 10 == 0)
            {
                Console.WriteLine("Backup");
                BackupHendler.Invoke();
            }

            Console.WriteLine(log);
        }
    }
}
