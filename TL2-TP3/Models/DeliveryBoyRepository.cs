using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

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

      using (var conexion = new SqliteConnection(connectionString))
      {
        conexion.Open();
        string SQLQuery = "SELECT * FROM Cadetes";
        SqliteCommand command = new SqliteCommand(SQLQuery, conexion);
        SqliteDataReader DataReader = command.ExecuteReader();
        while (DataReader.NextResult())
        {
          DeliveryBoy cadete = new()
          {
            Id = (int)DataReader["cadeteId"],
            Name = DataReader["cadeteNombre"].ToString()
          };
          ListadoCadete.Add(cadete);
        }
        conexion.Close();
      }

      return ListadoCadete;
    }
  }
}