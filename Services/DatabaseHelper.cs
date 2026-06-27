using MySql.Data.MySqlClient;

namespace part.Database
{
    public class DatabaseHelper
    {
        public static string ConnectionString =
            "server=localhost;database=LockWiseDB;uid=root;pwd=vutlhariC01!;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
} 