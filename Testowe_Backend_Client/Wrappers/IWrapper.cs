using Testowe_Backend_Client.VariousTimers;

namespace Testowe_Backend_Client.Wrappers
{
    public interface IWrapper
    {
        /// <summary>
        /// Get the Initialized timer
        /// </summary>
        /// <returns>Return Prefered timer</returns>
        public ITimer GetTimer();
    }
}