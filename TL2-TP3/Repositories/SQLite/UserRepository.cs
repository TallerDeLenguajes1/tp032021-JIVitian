using NLog;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using TL2_TP3.Models;
using TL2_TP3.Repositories.Interfaces;

namespace TL2_TP3.Repositories.SQLite
{
    public class UserRepository
    {
        private readonly string connectionString;
        private readonly Logger logger;

        public UserRepository(string connectionString, Logger logger)
        {
            this.connectionString = connectionString;
            this.logger = logger;

        }

        private User FindUser(string user, string password)
        {
            User currentUser = null;

            try
            {
                using (var conection = new SQLiteConnection(connectionString))
                {
                    conection.Open();
                    string SQLQuery = "SELECT * FROM users WHERE user=@user AND password=@password AND active=1";
                    SQLiteCommand command = new SQLiteCommand(SQLQuery, conection);
                    command.Parameters.AddWithValue("@use", user);
                    command.Parameters.AddWithValue("@password", password);
                    SQLiteDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                        currentUser = new User()
                        {
                            IdUser = Convert.ToInt32(dataReader["id"]),
                            Name = dataReader["name"].ToString(),
                            Password = dataReader["password"].ToString(),
                            Email = dataReader["email"].ToString(),
                            Role = Enum.TryParse(dataReader["role"].ToString(), out Role role)
                                   ? role
                                   : Role.user
                        };
                    conection.Close();
                }

            }
            catch (Exception ex)
            {
                logger.Error($"Error Message: {ex.Message}. Stack Trace: {ex.StackTrace}");
            }

            return currentUser;
        }

        private bool CreateUser(string user, string email, string password)
        {
            bool result = false;

            try
            {
                using (var conexion = new SQLiteConnection(connectionString))
                {
                    conexion.Open();
                    string query = @"
                                 INSERT INTO
                                 users (user, email, password)
                                 VALUES (@user, @email, @password)
                               ";
                    using (SQLiteCommand command = new SQLiteCommand(query, conexion))
                    {
                        conexion.Open();
                        command.Parameters.AddWithValue("@user", user);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@password", password);
                        result = command.ExecuteNonQuery() > 0;
                        conexion.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error($"Error Message: {ex.Message}. Stack Trace: {ex.StackTrace}");
            }

            return result;
        }
    }
}
