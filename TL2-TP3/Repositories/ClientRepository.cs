using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TL2_TP3.Models;

namespace TL2_TP3.Repositories
{
    public class ClientRepository : RepositoryJSON<Client>
    {
        public ClientRepository()
        {
            path = "Clients.JSON";
            List = ReadJSON();
        }

        public void AddClient(Client client)
        {
            client.Id = List.Count > 0 ? List.Last().Id + 1 : 1;
            List.Add(client);
            SaveJSON();
        }

        public void EditClient(Client client)
        {
            var oldClient = List.FindIndex(x => x.Id == client.Id);
            List[oldClient] = client;
            SaveJSON();
        }

        public void DeleteClient(int id)
        {
            List.RemoveAll(x => x.Id == id);

            SaveJSON();
        }
    }
}
