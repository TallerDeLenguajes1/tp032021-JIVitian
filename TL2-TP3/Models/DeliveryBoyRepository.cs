using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using TL2_TP3.Repositories.Interfaces;
using NLog;

namespace TL2_TP3.Models
{
    public class DeliveryBoyRepository : IRepository<DeliveryBoy>
    {
        private readonly string connectionString;
        private readonly Logger logger;
        // private readonly SqliteConnection conexion;
        public DeliveryBoyRepository(string connectionString, Logger nlog)
        {
            this.connectionString = connectionString;
            // conexion = new SqliteConnection(connectionString);
        }

        // TODO: Hacer esta función un predicado para hacer lo que se quiera con los datos recopilados
        private DeliveryBoy SetDeliveryBoy(SQLiteDataReader dataReader)
        {
            return new DeliveryBoy()
            {
                Id = Convert.ToInt32(dataReader["id"]),
                Name = dataReader["name"].ToString(),
                Phone = dataReader["phone"].ToString(),
                Address = dataReader["address"].ToString()
            };
        }

        public List<DeliveryBoy> GetAll()
        {
            List<DeliveryBoy> ListadoCadete = new();

            try
            {
            using (var conection = new SQLiteConnection(connectionString))
            {
                conection.Open();
                string SQLQuery = "SELECT * FROM DeliveryBoys";
                SQLiteCommand command = new SQLiteCommand(SQLQuery, conection);
                SQLiteDataReader DataReader = command.ExecuteReader();
                while (DataReader.Read())
                    ListadoCadete.Add(SetDeliveryBoy(DataReader));
                conection.Close();
                }

            }
            catch (Exception ex)
            {
                logger.Error("Error Message: " + ex.Message);
            }

            return ListadoCadete;
        }

        public DeliveryBoy GetById(int id)
        {
            var deliveryBoy = new DeliveryBoy();

            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    conexion.Open();

                    string query = "SELECT * FROM DeliveryBoys WHERE id=@id";
                    SQLiteCommand command = new SQLiteCommand(query, conexion);
                    command.Parameters.AddWithValue("@id", id);

                    var dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        deliveryBoy = SetDeliveryBoy(dataReader);
                    }

                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error Message: " + ex.Message);
            }

            return deliveryBoy;
        }   

        public void Insert(DeliveryBoy data)
        {
            try
            {
                string query = @"INSERT INTO
                                 DeliveryBoys (name, phone, address)
                                 VALUES (@name, @phone, @address)";

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
                logger.Error("Error Message: " + ex.Message);
            }
        }

        public void Update(DeliveryBoy data)
        {
            try
            {
                string query = @"
                                 UPDATE DeliveryBoys
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
                logger.Error("Error Message: " + ex.Message);
            }
        }

        public void Delete(int id)
        {
            string query = @"
                            DELETE FROM DeliveryBoys
                            WHERE id = @id
                        ";
            
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
    }
}