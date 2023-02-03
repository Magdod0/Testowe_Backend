using Testowe_Backend_Client.VariousTimers.ExchangeBuilders;

namespace Testowe_Backend_Client.VariousTimers
{
    public interface ITimer
    {
        public void StartTimer();
        public void StopTimer();
    }
}
