using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerAsync
{
    public class Actions
    {
        private LoggerSingleton _logger = LoggerSingleton.GetInstance();

        public Actions()
        {
        }

        public bool Method1()
        {
            _logger.Log(LogType.Info, $"Start Method: Method1");
            return true;
        }

        public void Method2()
        {
            _logger.Log(LogType.Warning, "Skipped logic in method");
            throw new BusinessException("Skipped logic in method");
        }

        public void Method3()
        {
            _logger.Log(LogType.Error, "I broke a logic");
            throw new Exception("I broke a logic");
        }
    }
}
