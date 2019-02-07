using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.Models.DTOs;

namespace UltimateTicTacToe.DataAccess
{
    class SQLDataBaseProvider : IDatabaseProvider
    {
         public int createUser()
        {
            int UId;
            using (SqlConnection connection = new SqlConnection(getConnectionString()))
            {
                UId = getNumberOfUsers();
                SqlParameter userId = new SqlParameter("@UserId", SqlDbType.Int, UId);
                SqlParameter total = new SqlParameter("@TotalScore", SqlDbType.Float, 0);
                SqlParameter moves = new SqlParameter("@TotalMoves", SqlDbType.Int, 0);
                SqlParameter latest = new SqlParameter("@Latest", SqlDbType.Float, 0);
                connection.Open();
                SqlCommand command = new SqlCommand(null, connection);
                command.CommandText = "INSERT INTO RATINGS (UserId, TotalScore, LatestScore, TotalMoves)" +
                    "VALUES (@UserId, @Total, @Latest, @TotalMoves)";
                command.Parameters.Add(userId);
                command.Parameters.Add(total);
                command.Parameters.Add(latest);
                command.Parameters.Add(moves);
                command.Prepare();
                command.ExecuteNonQuery();
                connection.Close();
            }
            return UId;
        }

        public RatingDTO getUser(int UserId)
        {
            RatingDTO result = new RatingDTO();
            using (SqlDataReader reader = getUserRow(UserId))
            {
                if (reader.Read())
                {
                    result.UserId = (int)reader["UserId"];
                    result.average = ((double)reader["TotalScore"]) / ((int)reader["totalMoves"]);
                    result.latest = (double)reader["LatestScore"];
                }
            }
            return result;
        }

        public RatingDTO updateUser(int UserId, double LatestScore)
        { 
            using (SqlDataReader reader = getUserRow(UserId))
            {
                if (reader.Read())
                {
                    using (SqlConnection connection = new SqlConnection(getConnectionString()))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(null, connection);
                        double total = ((double)reader["TotalScore"]) + LatestScore;
                        int moves = (int)reader["TotalMoves"] + 1;
                        command.CommandText = "UPDATE RATINGS SET LatestScore = "+ LatestScore +", TotalScore = "+ total +", TotalMoves = "+ moves +" WHERE UserId = " +UserId;
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            return getUser(UserId);
        }

        private SqlDataReader getUserRow(int UserId)
        {
            SqlDataReader reader;
            using (SqlConnection connection = new SqlConnection(getConnectionString()))
            {
                SqlParameter userId = new SqlParameter("@UserId", SqlDbType.Int, UserId);
                connection.Open();
                SqlCommand command = new SqlCommand(null, connection);
                command.CommandText = "SELECT * FROM RATINGS WHERE UserId = @UserId";
                command.Parameters.Add(userId);
                command.Prepare();
                reader = command.ExecuteReader();
                connection.Close();
            }
            return reader;
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
