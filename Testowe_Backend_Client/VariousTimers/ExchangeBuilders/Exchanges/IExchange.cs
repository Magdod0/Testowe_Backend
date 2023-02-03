
namespace Testowe_Backend_Client.VariousTimers.ExchangeBuilders.Exchanges
{
    public interface IExchange
    {
        /// <summary>
        /// Send object with key and Id to the server
        /// </summary>
        /// <returns>record class object with key, status and message</returns>
        public FullMessage ServerExchange();

    }
}
