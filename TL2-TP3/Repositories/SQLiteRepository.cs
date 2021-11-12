using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using TL2_TP3.Repositories.Interfaces;

namespace TL2_TP3.Repositories
{
    public abstract class SQLiteRepository <T> : IRepository <T>
    {
        protected string tableName;
        protected string conectionString;

        public List<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public void GetById()
        {
            throw new NotImplementedException();
        }

        public void Insert(T data)
        {
            throw new NotImplementedException();
        }

        public void Update(T data)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
