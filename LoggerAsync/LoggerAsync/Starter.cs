using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoggerAsync
{
    public class Starter
    {
        private Actions _actions = new Actions();
        private LoggerSingleton _logger = LoggerSingleton.GetInstance();

        private event Func<string, Task> Func;

        public void Run()
        {
            _logger.BackupHendler += () =>
            {
                FileService.RecordingToFile(_logger.DatabaseLogs.ToString());
            };

            Func += async (message) =>
            {
                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        switch (new Random().Next(1, 2))
                        {
                            case 1:
                                Console.WriteLine(message);
                                _actions.Method1();
                                break;
                            case 2:
                                _actions.Method2();
                                break;
                            case 3:
                                _actions.Method3();
                                break;
                        }
                    }
                    catch (BusinessException bEx)
                    {
                        _logger.Log(LogType.Warning, $"Action got this custom Exception : {bEx.Message}");
                    }
                    catch (Exception ex)
                    {
                        _logger.Log(LogType.Warning, $"Action failed by reason : {ex}");
                    }

                    await Task.Delay(100);
                }
            };

            Qwerty(Func, Func);
        }

        public void Qwerty(Func<string, Task> func1, Func<string, Task> func2)
        {
            Task.WhenAll(func1.Invoke("func1"), func2.Invoke("func2")).GetAwaiter().GetResult();
        }

        public async void Qwerty(Action action1, Action action2)
        {
            Console.WriteLine("1");
            await Task.WhenAll(new Task(action1), new Task(action2));
            await Task.Delay(2000);
            Console.WriteLine("2");
        }
    }
}
