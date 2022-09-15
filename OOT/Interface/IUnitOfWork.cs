using System;
using System.Data;

namespace OOT.Interface
{
  public interface IUnitOfWork
  {
    DataTable WorkSpace(string name);
    int Commit();
    void Rollback();
  }
}
