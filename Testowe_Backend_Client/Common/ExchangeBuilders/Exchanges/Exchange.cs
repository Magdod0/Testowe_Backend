using CommonResources;
using Grpc.Core;
using Grpc.Net.Client;
using System.Net.Http.Json;
using System.Threading;
using Testowe_GrpcService;

namespace Testowe_Backend_Client.Common.ExchangeBuilders.Exchanges
{
    public class Exchange : IExchange
    {

        private string _key;
        private string _encryptedMessage;
        private string _id;
        public HttpClient host;
        private string Address { get; set; }
        private int Port { get; set; }
        //public Transmission(KeyToMessage key)
        //{
        //    _keyToMessage = key;
        //}

        public Exchange(string address, int defaultPort, string key, string id, string encrypted)
        {
            this._key = key;
            this._id = id;
            _encryptedMessage= encrypted;
            host = new HttpClient();
            Address = address;
            Port = defaultPort;
        }
        //public TransmitObject Transmit()
        //{
        //    throw new NotImplementedException();
        //}
        public async Task<FullMessage> ServerExchange()
        {
            try
            {
                //using var channel = GrpcChannel.ForAddress("https://localhost:5001");
                //var client = new Decryptor.DecryptorClient(channel);
                //Request request = new Request { Id = _id, Key = _key };
                //Response response = client.Decrypt(request);

                //return new FullMessage(_key, response.Status, _encryptedMessage, response.Message);
                using var statusResponse = await host.GetAsync($"{Address}:{Port}");
                var statusOfResponse = await statusResponse.Content.ReadFromJsonAsync<CommonStatusCode>();
                if (statusOfResponse == null || statusOfResponse.Code != 3303)
                    throw new Exception("No Answer from Server!");

                var gMessage = new GetMessage() { Key = _key, Id = _id };
                JsonContent content = JsonContent.Create(gMessage);

                var postResponse = await host.PostAsync($"{Address}:{Port}",content);
                var postMessage = await postResponse.Content.ReadFromJsonAsync<PostMessage>();
                if (postMessage == null)
                    throw new Exception("No Answer from Decryptor!");
                var fullMessage = new FullMessage(postMessage.Key, statusOfResponse.Status, postMessage.EncryptedMessage, postMessage.Message);
                return fullMessage;

            }
            catch
            {
                throw;
            }
        }
    }
}
