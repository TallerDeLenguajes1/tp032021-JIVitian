using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TL2_TP3.Repositories.Interfaces
{
    interface IRepository <T>
    {
        //public string DBName { get; set; }
        public List<T> GetAll();
        public T GetById(int id);
        public void Insert(T data);
        public void Update(T data);
        public void Delete(int id);
    }
}
