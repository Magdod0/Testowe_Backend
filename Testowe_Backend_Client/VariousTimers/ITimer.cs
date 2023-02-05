using Testowe_Backend_Client.VariousTimers.ExchangeBuilders;

namespace Testowe_Backend_Client.VariousTimers
{
    public interface ITimer
    {
        /// <summary>
        /// Make the exchange and start Timer
        /// </summary>
        public void StartTimer();
        /// <summary>
        /// Stop Timer
        /// </summary>
        public void StopTimer();
    }
}
