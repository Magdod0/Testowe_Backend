using Testowe_Backend_Client.Common.ExchangeBuilders.Connections;
using Testowe_Backend_Client.Common.ExchangeBuilders.DbManager;
using Testowe_Backend_Client.Common.ExchangeBuilders.Encryptor;
using Testowe_Backend_Client.Common.ExchangeBuilders.Exchanges;

namespace Testowe_Backend_Client.Common.ExchangeBuilders
{
    public class MessageExchangeBuilder : BaseExecution, IExchangeBuilder
    {
        private UserSettings userSettings;
        public MessageExchangeBuilder(UserSettings settings)
        {
            userSettings = settings;
        }

        //public override string Name => nameof(MessageExchangeBuilder);

        //public override void Execute()
        //{
            
        //}
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
                var encryptedMessage = encryptor.Encrypt(userSettings.Message, userSettings.Key);
                // Converting from bytes to string
                string encrypted = Convert.ToBase64String(encryptedMessage);
                // Inserting encrypted message to database and getting ID of the item in return
                var id = sqlManager.Create(encrypted);

                // Creating new Exchange with Service
                return new Exchange(userSettings.Key, id, encrypted);

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEncryption CreateEncryption()
        {
            var encryptions = new IEncryption[]
            {
                new Encryption()
            };
            return QuestionManager.Choose(encryptions, e => e.GetType().Name, "Choose Encryptor:");
        }


        public ICreateMessage GetProvider()
        {
            var providers = new ICreateMessage[]
            {
                new SQLMessageManager(new MessageConnection(userSettings.ConnectionString))
            };
            return QuestionManager.Choose(providers, e=> e.GetType().Name, "Choose Provider:");
        }


    }
}
