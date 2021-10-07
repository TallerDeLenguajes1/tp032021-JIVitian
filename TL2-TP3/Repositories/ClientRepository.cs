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

        public void AddClient(IFormCollection collection)
        {
            List.Add(new Client { Id = List.Count() > 0 ? List.Last().Id + 1 : 1, Name = collection["Name"], Address = collection["Address"], Phone = collection["Phone"] });
            SaveJSON();
        }

        public void EditClient(int id, IFormCollection collection)
        {
            var client = List.Find(x => x.Id == id);
            client.Name = collection["Name"];
            client.Address = collection["Address"];
            client.Phone = collection["Phone"];

            SaveJSON();
        }

        public void DeleteClient(int id)
        {
            List.RemoveAll(x => x.Id == id);

            SaveJSON();
        }
    }
}
