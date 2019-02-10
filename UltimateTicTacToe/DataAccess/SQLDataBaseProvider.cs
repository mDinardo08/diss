using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.DataAccess.DBOs;
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
                    "VALUES (@UserId, @Total, @Latest, @TotalMoves) ";
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
            RatingDBO row = getUserRow(UserId);
            if (row != null)
            {
                result.UserId = row.UserId;
                result.average = row.TotalScore / row.TotalMoves;
                result.latest = row.LatestScore;
            } else
            {
                throw new Exception();
            }
            return result;
        }

        public RatingDTO updateUser(int UserId, double LatestScore)
        {
            RatingDBO row = getUserRow(UserId);
            if (row != null)
            {
                using (SqlConnection connection = new SqlConnection(getConnectionString()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(null, connection);
                    double total = row.TotalScore + LatestScore;
                    int moves = row.TotalMoves + 1;
                    command.CommandText = "UPDATE RATINGS SET LatestScore = " + LatestScore + ", TotalScore = " + total + ", TotalMoves = " + moves + " WHERE UserId = " + UserId;
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            } else
            {
                throw new Exception();
            }
            return getUser(UserId);
        }

        private RatingDBO getUserRow(int UserId)
        {
            RatingDBO result = null;
            using (SqlConnection connection = new SqlConnection(getConnectionString()))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(null, connection);
                command.CommandText = "SELECT * FROM RATINGS WHERE UserId = " + UserId;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    result.LatestScore = (double)reader["LatestScore"];
                    result.TotalMoves = (int)reader["TotalMoves"];
                    result.TotalScore = (double)reader["TotalScore"];
                    result.UserId = (int)reader["UserId"];
                }
                connection.Close();
            }
            return result;
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
            using (StreamReader sr = new StreamReader("./DataAccess/connectionString.json"))
            {
                connectionString = JsonConvert.DeserializeObject<JObject>(sr.ReadToEnd())["connectionString"].Value<string>();
            }
            return connectionString;
        }
    }
}
