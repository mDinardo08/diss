using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTicTacToe.DataAccess
{
    class SQLDataBaseProvider : IDatabaseProvider
    {
         public void createUser()
        {
            using (SqlConnection connection = new SqlConnection(getConnectionString()))
            {
                SqlParameter userId = new SqlParameter("@UserId", SqlDbType.Int, getNumberOfUsers());
                SqlParameter average = new SqlParameter("@Average", SqlDbType.Int, 0);
                SqlParameter latest = new SqlParameter("@Latest", SqlDbType.Int, 0);
                SqlCommand command = new SqlCommand(null, connection);
                command.CommandText = "INSERT INTO RATINGS (UserId, AverageScore, LatestScore)" +
                    "VALUES (@UserId, @Average, @Latest)";
                command.Parameters.Add(userId);
                command.Parameters.Add(average);
                command.Parameters.Add(latest);
                command.Prepare();
                command.ExecuteNonQuery();
            }
        }

        private int getNumberOfUsers()
        {

            string stmt = "SELECT COUNT(*) FROM RATINGS";
            int count = 0;

            using (SqlConnection connection = new SqlConnection(getConnectionString()))
            {
                using (SqlCommand cmdCount = new SqlCommand(stmt, connection))
                {
                    connection.Open();
                    count = (int)cmdCount.ExecuteScalar();
                    connection.Close();
                }
            }
            return count;

        }

        private string getConnectionString()
        {
            string connectionString = "";
            using (StreamReader sr = new StreamReader("./connectionString.josn"))
            {
                connectionString = JsonConvert.DeserializeObject<string>(sr.ReadToEnd());
            }
            return connectionString;
        }
    }
}
