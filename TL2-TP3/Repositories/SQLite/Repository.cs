using NLog;

namespace TL2_TP3.Repositories.SQLite
{
    public class Repository
    {
        public DeliveryBoyRepository DBRepo { get; set; }
        public OrderRepository orderRepo { get; set; }
        public ClientRepository clientRepo { get; set; }
        public Logger logger { get; }

        public Repository(string connectionString, Logger logger)
        {
            this.logger = logger;
            DBRepo = new DeliveryBoyRepository(connectionString, logger);
            orderRepo = new OrderRepository(connectionString, logger);
            clientRepo = new ClientRepository(connectionString, logger);
        }
    }
}
