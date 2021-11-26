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
    public class OrderRepository : IRepository<Order>
    {
        private readonly string connectionString;
        private readonly Logger logger;

        public OrderRepository(string connectionString, Logger logger)
        {
            this.connectionString = connectionString;
            this.logger = logger;
        }

        private Order SetOrder(SQLiteDataReader dataReader)
        {
            return new Order()
            {
                Number = Convert.ToInt32(dataReader["number"]),
                Observation = dataReader["observation"].ToString(),
                State = Enum.TryParse(dataReader["state"].ToString(), out State state)
                        ? state
                        : State.ToConfirm
            };
        }

        // TODO: Hacer esta función un predicado para hacer lo que se quiera con los datos recopilados
        private Order SetOrderWithClient(SQLiteDataReader dataReader)
        {
            return new Order()
            {
                Number = Convert.ToInt32(dataReader["number"]),
                Observation = dataReader["observation"].ToString(),
                State = Enum.TryParse(dataReader["state"].ToString(), out State state)
                        ? state
                        : State.ToConfirm,
                Client = new Client()
                {
                    Id = Convert.ToInt32(dataReader["id"]),
                    Name = dataReader["name"].ToString(),
                    Address = dataReader["address"].ToString(),
                    Phone = dataReader["phone"].ToString()
                }
            };
        }

        public List<Order> GetAll()
        {
            List<Order> ordersList = new();

            try
            {
                using (var conection = new SQLiteConnection(connectionString))
                {
                    conection.Open();
                    string SQLQuery = @"
                                        SELECT * 
                                        FROM Orders o INNER JOIN Clients c
                                        ON o.clientId = c.id
                                        WHERE o.active=1 AND c.active=1
                                      ";
                    SQLiteCommand command = new SQLiteCommand(SQLQuery, conection);
                    SQLiteDataReader DataReader = command.ExecuteReader();
                    while (DataReader.Read())
                        ordersList.Add(SetOrderWithClient(DataReader));
                    conection.Close();
                }

            }
            catch (Exception ex)
            {
                logger.Error("Error Message: " + ex.Message);
            }

            return ordersList;
        }

        public Order GetById(int id)
        {
            var order = new Order();

            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    conexion.Open();

                    string query = "SELECT * FROM Orders WHERE number=@id";
                    SQLiteCommand command = new SQLiteCommand(query, conexion);
                    command.Parameters.AddWithValue("@id", id);

                    var dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        order = SetOrder(dataReader);
                    }

                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error Message: " + ex.Message);
            }

            return order;
        }

        public void Insert(Order data)
        {
            try
            {
                string query = @"
                               INSERT INTO
                               Orders (observation, state)
                               VALUES (@observation, @state)
                               ";

                using (var conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(query, conexion))
                    {
                        conexion.Open();
                        command.Parameters.AddWithValue("@observation", data.Observation);
                        command.Parameters.AddWithValue("@state", data.State);
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

        public void Update(Order data)
        {
            try
            {
                string query = @"
                                 UPDATE Orders
                                 SET observation = @observation,
                                     state = @state
                                 WHERE number = @id
                               ";

                using (var conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(query, conexion))
                    {
                        conexion.Open();
                        command.Parameters.AddWithValue("@observation", data.Observation);
                        command.Parameters.AddWithValue("@state", data.State);
                        command.Parameters.AddWithValue("@id", data.Number);
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
                            UPDATE Orders
                            SET active = 0
                            WHERE number = @id
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

        public void SetClient(int clientId, int orderId)
        {
            string query = @"
                            UPDATE Orders
                            SET clientId=@clientId
                            WHERE number=@id
                           ";

            try
            {
                using (var conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(query, conexion))
                    {
                        conexion.Open();
                        command.Parameters.AddWithValue("@clientId", clientId);
                        command.Parameters.AddWithValue("@id", orderId);
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

        public void SetDeliveryBoy(int deliveryBoyId, int orderId)
        {
            string query = @"
                            UPDATE Orders
                            SET deliveryBoyId = @deliveryBoyId
                            WHERE number=@id
                           ";

            try
            {
                using (var conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(query, conexion))
                    {
                        conexion.Open();
                        command.Parameters.AddWithValue("@deliveryBoyId", deliveryBoyId);
                        command.Parameters.AddWithValue("@id", orderId);
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

        /**
         * Get the index of the last register in the DB.
         * In case of not find any register, returns -1.
         * 
         * @return index
         */
        public int getLastIndex()
        {
            int index = -1;

            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    conexion.Open();

                    string query = "SELECT number FROM Orders ORDER BY number DESC LIMIT 1";
                    SQLiteCommand command = new SQLiteCommand(query, conexion);

                    var dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        index = Convert.ToInt32(dataReader["number"]);
                    }

                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error Message: " + ex.Message);
            }

            return index;
        }
    }
}
