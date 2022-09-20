using System.Data.SqlClient;

namespace SerraLinhasAereas.Infra.Data.DataBase
{
    public static class DBSettings
    {
        private const string _connectionString = @"data source=.\SQLEXPRESS;initial catalog=Serra;uid=sa;pwd=123;";
        private static SqlConnection _connection;

        public static SqlConnection Conexao()
        {
            _connection = new SqlConnection(_connectionString);

            return _connection;
        }
    }
}
