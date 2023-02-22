using CommonResources;

namespace Testowe_GRPC.Services
{
    public interface IDecryptionService
    {
        public Task<PostMessage> Decrypt(string message, string key);
    }
}
