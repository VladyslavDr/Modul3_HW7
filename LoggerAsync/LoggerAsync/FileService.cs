using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerAsync
{
    public static class FileService
    {
        public static void RecordingToFile(string message)
        {
            var time = DateTime.Now;
            var path = "Backup\\" + $"{time.ToString("hh.mm.ss.fff dd.MM.yyyy")}.txt";

            using (var fstream = new FileStream(path, FileMode.Create))
            {
                byte[] buffer = Encoding.Default.GetBytes(message);
                fstream.Write(buffer, 0, buffer.Length);
            }
        }
    }
}
