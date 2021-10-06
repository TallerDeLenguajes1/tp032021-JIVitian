using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace TL2_TP3.Repositories
{
    public abstract class RepositoryJSON <T>
    {
        protected string path;

        public List<T> List { get; set; }

        protected List<T> ReadJSON()
        {
            if (File.Exists(path))
            {
                using (FileStream archivo = new FileStream(path, FileMode.Open))
                {
                    StreamReader strReader = new StreamReader(archivo);
                    string json = strReader.ReadToEnd();
                    strReader.Close();
                    strReader.Dispose();
                    return json != "" ? JsonSerializer.Deserialize<List<T>>(json) : new List<T>();
                }
            }
            else
            {
                var archivo = new FileStream(path, FileMode.Create);
                return new List<T>();
            }
        }

        protected void SaveJSON()
        {
            string JSONData = JsonSerializer.Serialize(List);

            using (FileStream file = new FileStream(path, FileMode.Create))
            {
                StreamWriter strWrite = new StreamWriter(file);
                strWrite.WriteLine(JSONData);
                strWrite.Close();
                strWrite.Dispose();
            }
        }

    }
}
