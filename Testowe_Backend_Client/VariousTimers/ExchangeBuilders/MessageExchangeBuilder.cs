using Testowe_Backend_Client.VariousTimers.ExchangeBuilders.Connections;
using Testowe_Backend_Client.VariousTimers.ExchangeBuilders.DbManager;
using Testowe_Backend_Client.VariousTimers.ExchangeBuilders.Encryptor;
using Testowe_Backend_Client.VariousTimers.ExchangeBuilders.Exchanges;

namespace Testowe_Backend_Client.VariousTimers.ExchangeBuilders
{
    public class MessageExchangeBuilder : IExchangeBuilder
    {
        private string _message;
        private string _key;
        ICreate connection;
        public MessageExchangeBuilder(string message, string key, string connectionString)
        {
            _message = message;
            _key = key;
            connection = new MessageConnection(connectionString);
        }
        /// <summary>
        /// Creating Class for exchange with Service
        /// </summary>
        /// <returns></returns>
        public IExchange Build()
        {
            try
            {
                //Creating the Encryptor
                var encryptor = CreateEncryption();
                //Creating new connection to Sql database
                var sqlManager = GetProvider();

                // Encrypting message with key
                var encryptedMessage = encryptor.Encrypt(_message, _key);
                // Converting from bytes to string
                string encrypted = Convert.ToBase64String(encryptedMessage);
                // Inserting encrypted message to database and getting ID of the item in return
                var id = sqlManager.Create(encrypted);

                // Creating new Exchange with Service
                return new Exchange(_key, id, encrypted);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEncryption CreateEncryption()
        {
            return new Encryption();
        }

        public ICreateMessage GetProvider()
        {
            return new SQLMessageManager(connection);
        }


    }
}
