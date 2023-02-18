namespace Testowe_Backend_Client.Common.ExchangeBuilders.Connections
{
    public interface ICreate:IConnection
    {
        public string InsertCommand { get; }
    }
}