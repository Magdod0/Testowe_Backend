using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Testowe_Backend_Client.VariousTimers.ExchangeBuilders.Exchanges;
using Testowe_Backend_Client.VariousTimers.ExchangeBuilders;

namespace Testowe_Backend_Client.VariousTimers
{
    public class SimpleTimer : ITimer
    {
        System.Timers.Timer _timer;
        IExchangeBuilder _exchangeBuilder;
        public SimpleTimer(IExchangeBuilder exchangeBuilder)
        {
            _timer = new System.Timers.Timer(15000);
            _timer.Elapsed += (sender, e) =>
            {
              PrintServerExchangeMessage();
            };
            _exchangeBuilder = exchangeBuilder;

        }
        private void PrintServerExchangeMessage()
        {
            try
            {
                var message = _exchangeBuilder
                    .Build()
                    .ServerExchange()
                    .ToString();

                Console.WriteLine(message);
            }
            catch(Exception ex)
            {
                Console.WriteLine("\nError happened:\n"+ex.Message);
            }
        }
        public void StartTimer()
        {
            // Fire the first time, otherwise it's 15 seconds wait.
            PrintServerExchangeMessage();

            _timer.Start();
        }

        public void StopTimer()
        {
            _timer.Stop();
        }
    }
}
