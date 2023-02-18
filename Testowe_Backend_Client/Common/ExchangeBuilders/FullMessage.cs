
namespace Testowe_Backend_Client.Common.ExchangeBuilders
{
    public record class FullMessage
    {
        public FullMessage(string key, string status, string encryptedMessage, string message)
        {
            Key = key;
            Message = message;
            Status = status;
            EncryptedMessage = encryptedMessage;
        }
        public string Key { get; init; } = null!;
        public string EncryptedMessage { get; init; } = null!;
        public string Message { get; init; } = null!;
        public string Status { get; init; } = null!;

        public override string ToString() =>
             $"\nStatus of the message:{Status}.\n Encrypted Message:{EncryptedMessage}, Key:{Key}, Message:{Message}";
        
    }
}
