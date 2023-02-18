namespace Testowe_Backend_Client.Common
{
    public class UserSettings
    {
        public string DefaultServiceListenAddress { get; private set; }
        public int DefaultPort { get;private set; }
        public string Message { get; private set; }
        public string Key { get; private set; }
        public string ConnectionString { get;private set; }

        public UserSettings(string address, string message, string key, string connectionString)
        {
            var array = address.Split(':');
            DefaultServiceListenAddress = array[0];
            if (array.Length > 1 && int.TryParse(array[1], out var result))
                DefaultPort = result;

            Message = message;
            Key = key;
            ConnectionString = connectionString;
        }
    }
}