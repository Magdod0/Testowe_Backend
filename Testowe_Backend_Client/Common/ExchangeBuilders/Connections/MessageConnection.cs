namespace Testowe_Backend_Client.Common.ExchangeBuilders.Connections
{
    public class MessageConnection : ICreate
    {
        private string _connectionString;
        public MessageConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string ConnectionString => _connectionString;

        public string InsertCommand => "INSERT INTO Messages(EncryptedText) output INSERTED.ID VALUES(@text)";
    }
}
