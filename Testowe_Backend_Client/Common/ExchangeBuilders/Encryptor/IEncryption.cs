namespace Testowe_Backend_Client.Common.ExchangeBuilders.Encryptor
{
    public interface IEncryption
    {
        /// <summary>
        /// Ecrypts message with key
        /// </summary>
        /// <returns></returns>
        /// 
        public byte[] Encrypt(string clearText, string key);
    }
}
