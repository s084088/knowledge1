using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;

namespace Util
{
    /// <summary>
    /// 日志帮助类
    /// </summary>
    public static class LogHelper
    {
        private static readonly ConcurrentQueue<LogModel> _que = new ConcurrentQueue<LogModel>();

        private static readonly ManualResetEvent _mre = new ManualResetEvent(false);

        private static Thread _thread = new Thread(new ThreadStart(WriteLog)) { IsBackground = true, Priority = ThreadPriority.BelowNormal };

        /// <summary>
        /// 写日志
        /// </summary>
        public static void WriteLog(string log)
        {
            _que.Enqueue(new LogModel
            {
                Message = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}:{log}\r\n",
                Path = "logs/",
                FileName = DateTime.Now.ToString("yyyy_MM_dd") + ".log"
            });
            _mre.Set();
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="log"></param>
        /// <param name="path"></param>
        public static void WriteLog(string log, string path)
        {
            _que.Enqueue(new LogModel
            {
                Message = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}:{log}\r\n",
                Path = path,
                FileName = DateTime.Now.ToString("yyyy_MM_dd") + ".log"
            });
            _mre.Set();
        }

        public static void WriteLog(string log, string path, string fileName)
        {
            _que.Enqueue(new LogModel
            {
                Message = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}:{log}\r\n",
                Path = path,
                FileName = fileName
            });
            _mre.Set();
        }

        /// <summary>
        /// 启动日志线程
        /// </summary>
        public static void Register()
        {
            try { _thread.Abort(); } catch { }
            _thread.Start();
        }

        private static void WriteLog()
        {
            while (true)
            {
                _mre.WaitOne();
                while (_que.Count > 0 && _que.TryDequeue(out LogModel log))
                {
                    string dirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, log.Path);
                    if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
                    string filePath = Path.Combine(dirPath, log.FileName);
                    if (!File.Exists(filePath)) File.Create(filePath).Close();
                    File.AppendAllText(filePath, log.Message);
                }
                _mre.Reset();
                Thread.Sleep(1);
            }
        }
    }

    public class LogModel
    {
        /// <summary>
        /// 目录
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }
    }
}