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
        
        // TODO: Hacer esta función un predicado para hacer lo que se quiera con los datos recopilados
        private Order SetOrder(SQLiteDataReader dataReader)
        {
            return new Order()
            {
                Number = Convert.ToInt32(dataReader["id"]),
                Observation = dataReader["observation"].ToString(),
                State = Enum.TryParse(dataReader["state"].ToString(), out State state) 
                        ? state 
                        : State.ToConfirm,
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
                    string SQLQuery = "SELECT * FROM Orders";
                    SQLiteCommand command = new SQLiteCommand(SQLQuery, conection);
                    SQLiteDataReader DataReader = command.ExecuteReader();
                    while (DataReader.Read())
                        ordersList.Add(SetOrder(DataReader));
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

                    string query = "SELECT * FROM Orders WHERE id=@id";
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
                string query = @"INSERT INTO
                                 Orders (observations, state, clientId, deliberyBoyId)
                                 VALUES (@id, @phone, @address)";

                using (var conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(query, conexion))
                    {
                        conexion.Open();
                        command.Parameters.AddWithValue("@name", data.Observation);
                        command.Parameters.AddWithValue("@phone", data.State);
                        command.Parameters.AddWithValue("@address", data.Client.Id);
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
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
