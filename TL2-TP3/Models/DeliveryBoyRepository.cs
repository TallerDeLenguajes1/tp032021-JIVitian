using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace TL2_TP3.Models
{
  public class DeliveryBoyRepository
  {
    private readonly string connectionString;
    // private readonly SqliteConnection conexion;
    public DeliveryBoyRepository(string connectionString)
    {
      this.connectionString = connectionString;
      // conexion = new SqliteConnection(connectionString);
    }

        public List<DeliveryBoy> getAll()
        {
            List<DeliveryBoy> ListadoCadete = new();

            using (var conexion = new SQLiteConnection(connectionString))
            {
                conexion.Open();
                string SQLQuery = "SELECT * FROM Cadetes";
                SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion);
                SQLiteDataReader DataReader = command.ExecuteReader();
                while (DataReader.Read())
                {
                    DeliveryBoy cadete = new()
                    {
                        Id = Convert.ToInt32(DataReader["cadeteID"]),
                        Name = DataReader["cadeteNombre"].ToString(),
                        Phone = DataReader["cadeteTelefono"].ToString(),
                        Address = DataReader["cadeteDireccion"].ToString()
                    };
                    ListadoCadete.Add(cadete);
                }
                conexion.Close();
            }

            return ListadoCadete;
        }
    }
}