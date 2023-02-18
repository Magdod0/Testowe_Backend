using Testowe_Backend_Client.Common.ExchangeBuilders.DbManager;
using Testowe_Backend_Client.Common.ExchangeBuilders.Encryptor;
using Testowe_Backend_Client.Common.ExchangeBuilders.Exchanges;

namespace Testowe_Backend_Client.Common.ExchangeBuilders
{
    public interface IExchangeBuilder
    {
        /// <summary>
        /// Build Class for exchange with server
        /// </summary>
        /// <returns></returns>
        public IExchange Build();
        /// <summary>
        /// Provides Database Management
        /// </summary>
        /// <returns></returns>
        public ICreateMessage GetProvider();
        /// <summary>
        /// Create Encryption class for message
        /// </summary>
        /// <returns></returns>
        public IEncryption CreateEncryption();
    }
}
