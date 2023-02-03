using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testowe_Backend_Client.VariousTimers.ExchangeBuilders;
using Testowe_Backend_Client.VariousTimers;

namespace Testowe_Backend_Client.Wrappers
{
    /// <summary>
    /// Simple Wrap around Timer so that it can be whatever you want
    /// </summary>
    public class SimpleWrapper : IWrapper
    {
        private string message;
        private string key;
        private string connectionString;

        public SimpleWrapper(string v1, string v2, string connectionString)
        {
            message = v1;
            key = v2;
            this.connectionString = connectionString;
        }
        public ITimer GetTimer()
        {
            return new SimpleTimer(new MessageExchangeBuilder(message, key, connectionString));
        }
    }
}
