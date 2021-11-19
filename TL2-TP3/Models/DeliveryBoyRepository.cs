using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using TL2_TP3.Repositories.Interfaces;

namespace TL2_TP3.Models
{
    public class DeliveryBoyRepository : IRepository<DeliveryBoy>
    {
        private readonly string connectionString;
        // private readonly SqliteConnection conexion;
        public DeliveryBoyRepository(string connectionString)
        {
            this.connectionString = connectionString;
            // conexion = new SqliteConnection(connectionString);
        }

        public List<DeliveryBoy> GetAll()
        {
            List<DeliveryBoy> ListadoCadete = new();

            using (var conection = new SQLiteConnection(connectionString))
            {
                conection.Open();
                string SQLQuery = "SELECT * FROM DeliberyBoys";
                SQLiteCommand command = new SQLiteCommand(SQLQuery, conection);
                SQLiteDataReader DataReader = command.ExecuteReader();
                while (DataReader.Read())
                {
                    DeliveryBoy cadete = new()
                    {
                        Id = Convert.ToInt32(DataReader["id"]),
                        Name = DataReader["name"].ToString(),
                        Phone = DataReader["phone"].ToString(),
                        Address = DataReader["address"].ToString()
                    };
                    ListadoCadete.Add(cadete);
                }
                conection.Close();
            }

            return ListadoCadete;
        }

        public DeliveryBoy GetById(int id)
        {
            var cadete = new DeliveryBoy();

            using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
            {
                conexion.Open();

                string query = "SELECT * FROM Cadetes where cadeteID=@id";
                SQLiteCommand command = new SQLiteCommand(query, conexion);
                command.Parameters.AddWithValue("@id", id);

                var dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    cadete.Id = Convert.ToInt32(dataReader["id"]);
                    cadete.Name = dataReader["name"].ToString();
                    cadete.Phone = dataReader["phone"].ToString();
                    cadete.Address = dataReader["address"].ToString();
                }

                conexion.Close();
            }

            return cadete;
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
                        command.Parameters.AddWithValue("@name", data.Name);
                        command.Parameters.AddWithValue("@phone", data.Phone);
                        command.Parameters.AddWithValue("@address", data.Address);
                        conexion.Open();
                        command.ExecuteNonQuery();
                        conexion.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                var mensaje = "Mensaje de error: " + ex.Message;
            }
        }

        public void Update(DeliveryBoy data)
        {
            try
            {
                string query = @"
                                 UPDATE DeliveryBoys
                                 SET cadeteNombre = @name,
                                     cadeteTelefono = @phone,
                                     cadeteDireccion = @address
                                 WHERE cadeteID = @id
                               ";

                using (var conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(query, conexion))
                    {
                        command.Parameters.AddWithValue("@nombre", data.Name);
                        command.Parameters.AddWithValue("@telefono", data.Phone);
                        command.Parameters.AddWithValue("@direccion", data.Address);
                        command.Parameters.AddWithValue("@cadeteID", data.Id);
                        conexion.Open();
                        command.ExecuteNonQuery();
                        conexion.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                var mensaje = "Mensaje de error: " + ex.Message;
            }
        }

        public void Delete(int id)
        {
            string query = @"
                                 DELETE FROM DeliveryBoys
                                 WHERE id = @id
                               ";
        }
    }
}