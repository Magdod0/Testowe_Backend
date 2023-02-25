using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Testowe_Backend_Client.Common.ExchangeBuilders.Exchanges;
using Testowe_Backend_Client.Common.ExchangeBuilders;

namespace Testowe_Backend_Client.Common.Timers
{
    public class SimpleTimer : ITimer
    {
        System.Timers.Timer _timer;
        // Setting up the exchange configurations
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
        /// <summary>
        /// Results of exchange
        /// </summary>
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
            catch (Exception ex)
            {
                Console.WriteLine("\nError happened:\n" + ex.Message);
            }
        }
        // Make the exchange and start 15 seconds Timer
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
