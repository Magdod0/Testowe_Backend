using Grpc.Core;
using Grpc.Net.Client;
using System.Threading;
using Testowe_GrpcService;

namespace Testowe_Backend_Client.VariousTimers.ExchangeBuilders.Exchanges
{
    public class Exchange : IExchange
    {

        private string _key;
        private string _encryptedMessage;
        private string _id;

        //public Transmission(KeyToMessage key)
        //{
        //    _keyToMessage = key;
        //}

        public Exchange(string key, string id, string encrypted)
        {
            this._key = key;
            this._id = id;
            _encryptedMessage= encrypted;
        }
        //public TransmitObject Transmit()
        //{
        //    throw new NotImplementedException();
        //}
        public FullMessage ServerExchange()
        {
            try
            {
                using var channel = GrpcChannel.ForAddress("https://localhost:5001");
                var client = new Decryptor.DecryptorClient(channel);
                Request request = new Request { Id = _id, Key = _key };
                Response response = client.Decrypt(request);

                return new FullMessage(_key, response.Status, _encryptedMessage, response.Message);
            }
            catch
            {
                throw;
            }
        }
    }
}
