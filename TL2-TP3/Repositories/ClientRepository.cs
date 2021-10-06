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
            List = new List<Client>();
        }

        public void AddClient()
        {
            
        }
    }
}
