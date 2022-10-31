using System;
using System.Text;

namespace LoggerAsync
{
    public class Program
    {
        private static readonly Starter _starter = new Starter();
        public static void Main(string[] args)
        {
            _starter.Run();
        }
    }
}
