using Testowe_Backend_Client.VariousTimers.ExchangeBuilders.Connections;

namespace Testowe_Backend_Client.VariousTimers.ExchangeBuilders.DbManager
{
    public interface ICreateMessage
    {
        /// <summary>
        /// Create Encrypted Message in DB
        /// </summary>
        /// <returns>Key</returns>
        public string Create(string message);
    }
}
