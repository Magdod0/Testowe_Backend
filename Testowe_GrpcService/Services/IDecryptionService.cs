namespace Testowe_GRPC.Services
{
    public interface IDecryptionService
    {
        public Task<Testowe_GrpcService.Response> Decrypt(string message, string key);
    }
}
