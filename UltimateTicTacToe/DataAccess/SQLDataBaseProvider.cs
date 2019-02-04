using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTicTacToe.DataAccess
{
    class SQLDataBaseProvider : IDatabaseProvider
    {
        private string connectionString = "Server=tcp:ultimatetictactoe.database.windows.net,1433;Initial Catalog=UltimateTicTacToeDB;Persist Security Info=False;User ID=ypb14146;Password=Jq6%4PMXW1n84cdB;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public void createUser(int Uid, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand countUserCommand = new SqlCommand("SELECT COUNT(*) FROM dbo.GAMES", connection);
                int userId = countUserCommand.

                connection.x
                SqlCommand command = new SqlCommand(null, connection);
            }

            private int getNumberOfUsers()
            {
             
                string stmt = "SELECT COUNT(*) FROM dbo.USERS";
                int count = 0;

                using (SqlConnection connection = new SqlConnection("Data Source=DATASOURCE"))
                {
                    using (SqlCommand cmdCount = new SqlCommand(stmt, connection))
                    {
                        connection.Open();
                        count = (int)cmdCount.ExecuteScalar();
                    }
                }
                return count;
                
            }
        }
    }
}
