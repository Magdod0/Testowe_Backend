using Testowe_Backend_Client.Common.ExchangeBuilders.Connections;
using Testowe_Backend_Client.Common.ExchangeBuilders.DbManager;
using Testowe_Backend_Client.Common.ExchangeBuilders.Encryptor;
using Testowe_Backend_Client.Common.ExchangeBuilders.Exchanges;

namespace Testowe_Backend_Client.Common.ExchangeBuilders
{
    public class MessageExchangeBuilder : IExchangeBuilder
    {
        UserSettings userSetting;
        public MessageExchangeBuilder(UserSettings settings)
        {
            userSetting = settings;
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
                var encryptedMessage = encryptor.Encrypt(userSetting.Message, userSetting.Key);
                // Converting from bytes to string
                string encrypted = Convert.ToBase64String(encryptedMessage);
                // Inserting encrypted message to database and getting ID of the item in return
                var id = sqlManager.Create(encrypted);

                // Creating new Exchange with Service
                return new Exchange(userSetting.DefaultServiceListenAddress, userSetting.DefaultPort, userSetting.Key, id, encrypted);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEncryption CreateEncryption()
        {
            var Encryptors = new IEncryption[]
            {
                 new Encryption()
            };
            return QuestionManager.Choose(Encryptors, e => e.GetType().Name, "Choose Encryptor:");
        }

        public ICreateMessage GetProvider()
        {
            var conn = userSetting.ConnectionString;
            var tableManagers = new ICreate[]
            {
                new MessageConnection(conn)
            };
            var currentTableConnection = QuestionManager.Choose(tableManagers, e => e.GetType().Name, header: "Choose Table:");
            var providers = new ICreateMessage[]
            {
                new SQLMessageManager(currentTableConnection)
            };
            return QuestionManager.Choose(providers, e => e.GetType().Name, header: "Choose Provider:"); ;
        }


    }
}
