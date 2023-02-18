using Microsoft.Data.SqlClient;
using Testowe_Backend_Client.Common.ExchangeBuilders.Connections;

namespace Testowe_Backend_Client.Common.ExchangeBuilders.DbManager
{
    public class SQLMessageManager : ICreateMessage
    {
        ICreate _connection;
        public SQLMessageManager(ICreate connection)
        {
            _connection = connection;
        }

        //public string Message { get => _message; set => _message = value; }

        public string Create(string message)
        { 
            string expression = _connection.InsertCommand;
            string Id = string.Empty;
            try
            {
                using SqlConnection sqlConnection = new SqlConnection(_connection.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(expression, sqlConnection);

                //Use Command with parameters to prevent sql-Injection of sql bad code
                sqlCommand.Parameters.AddWithValue("@text", message);

                sqlConnection.Open();

                var reader = sqlCommand.ExecuteReader();

                if (reader == null) throw new Exception("Reader not created!");

                while (reader.Read())
                {
                    var objectId = reader["ID"];
                    if (objectId == null) throw new Exception("No Identity!");

                    Id = objectId?.ToString() ?? throw new Exception("No Identity, but not null?!");
                }

            }
            catch
            {
                throw;
            }
            return Id;
        }
    }
}
