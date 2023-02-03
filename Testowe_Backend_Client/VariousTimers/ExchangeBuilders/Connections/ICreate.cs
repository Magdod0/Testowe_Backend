namespace Testowe_Backend_Client.VariousTimers.ExchangeBuilders.Connections
{
    public interface ICreate:IConnection
    {
        public string InsertCommand { get; }
    }
}