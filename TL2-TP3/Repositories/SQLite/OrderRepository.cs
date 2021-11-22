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
        // private readonly SqliteConnection conexion;
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
                State = Enum.TryParse(dataReader["state"].ToString(), out State state) ? state : State.ToConfirm,
            };
        }

        public List<Order> GetAll()
        {
            throw new NotImplementedException();
        }

        public Order GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Order data)
        {
            throw new NotImplementedException();
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
