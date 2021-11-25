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
    public class ClientRepository : IRepository<Client>
    {
        private readonly string connectionString;
        private readonly Logger logger;

        public ClientRepository(string connectionString, Logger logger)
        {
            this.connectionString = connectionString;
            this.logger = logger;

        }

        // TODO: Hacer esta función un predicado para hacer lo que se quiera con los datos recopilados
        private Client SetClient(SQLiteDataReader dataReader)
        {
            return new Client()
            {
                Id = Convert.ToInt32(dataReader["id"]),
                Name = dataReader["name"].ToString(),
                Address = dataReader["address"].ToString(),
                Phone = dataReader["phone"].ToString()
            };
        }

        public List<Client> GetAll()
        {
            List<Client> clientsList = new();

            try
            {
                using (var conection = new SQLiteConnection(connectionString))
                {
                    conection.Open();
                    string SQLQuery = "SELECT * FROM Clients WHERE active=1";
                    SQLiteCommand command = new SQLiteCommand(SQLQuery, conection);
                    SQLiteDataReader DataReader = command.ExecuteReader();
                    while (DataReader.Read())
                        clientsList.Add(SetClient(DataReader));
                    conection.Close();
                }

            }
            catch (Exception ex)
            {
                logger.Error($"Error Message: {ex.Message}. Stack Trace: {ex.StackTrace}");
            }

            return clientsList;
        }

        public Client GetById(int id)
        {
            Client client = new();

            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    conexion.Open();

                    string query = "SELECT * FROM Clients WHERE id=@id AND active=1";
                    SQLiteCommand command = new SQLiteCommand(query, conexion);
                    command.Parameters.AddWithValue("@id", id);

                    var dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        client = SetClient(dataReader);
                    }

                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error($"Error Message: {ex.Message}. Stack Trace: {ex.StackTrace}");
            }

            return client;
        }

        public void Insert(Client data)
        {
            try
            {
                string query = @"
                                 INSERT INTO
                                 Clients (name, phone, address)
                                 VALUES (@name, @phone, @address)
                               ";

                using (var conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(query, conexion))
                    {
                        conexion.Open();
                        command.Parameters.AddWithValue("@name", data.Name);
                        command.Parameters.AddWithValue("@phone", data.Phone);
                        command.Parameters.AddWithValue("@address", data.Address);
                        command.ExecuteNonQuery();
                        conexion.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error($"Error Message: {ex.Message}. Stack Trace: {ex.StackTrace}");
            }
        }

        public void Update(Client data)
        {
            try
            {
                string query = @"
                                 UPDATE Clients
                                 SET name = @name,
                                     phone = @phone,
                                     address = @address
                                 WHERE id = @id
                               ";

                using (var conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(query, conexion))
                    {
                        conexion.Open();
                        command.Parameters.AddWithValue("@name", data.Name);
                        command.Parameters.AddWithValue("@phone", data.Phone);
                        command.Parameters.AddWithValue("@address", data.Address);
                        command.Parameters.AddWithValue("@id", data.Id);
                        command.ExecuteNonQuery();
                        conexion.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                logger.Error($"Error Message: {ex.Message}. Stack Trace: {ex.StackTrace}");
            }
        }

        public void Delete(int id)
        {
            string query = @"
                            UPDATE Clients
                            SET active = 0
                            WHERE id = @id
                           ";

            try
            {
                using (var conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(query, conexion))
                    {
                        conexion.Open();
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                        conexion.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error($"Error Message: {ex.Message}. Stack Trace: {ex.StackTrace}");
            }
        }
    }
}
