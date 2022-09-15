using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOT.Interface
{
  public interface IRepository<T>
    where T : class
  {
    T FindByKey(string itemName);

    void AddOrUpdate(T entity);
    
    void Delete(T entity);

    void Delete(string key);

    bool Exists(string key);
  }
}
