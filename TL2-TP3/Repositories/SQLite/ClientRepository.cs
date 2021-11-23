using AutoMapper;
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
        private readonly IMapper mapper;

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
                Name = dataReader["observation"].ToString(),
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
                logger.Error("Error Message: " + ex.Message);
            }

            return clientsList;
        }

        public Client GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Client data)
        {
            throw new NotImplementedException();
        }

        public void Update(Client data)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
