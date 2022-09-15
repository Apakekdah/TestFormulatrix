using OOT.Interface;
using System;
using System.Data;

namespace OOT.Data
{
  // In here we're assume DataSet is a unit of work
  public class UnitOfWork : IDisposable, IUnitOfWork
  {
    readonly DataSet dataSet;
    readonly string fullPath;

    public UnitOfWork()
    {
      dataSet = new DataSet("UnitOfWork");

      fullPath = System.AppDomain.CurrentDomain.BaseDirectory;
    }
        
    ~UnitOfWork()
    {
      this.Dispose();
    }

    public DataTable WorkSpace(string name)
    {
      DataTable table = dataSet.Tables[name];
      if (table == null)
      {
        table = new DataTable(name);
        dataSet.Tables.Add(table);
      }
      return table;
    }

    public int Commit()
    {
      int nCount = 0;
      using (DataSet ds = dataSet.GetChanges())
      {
        if (ds == null)
          return nCount;

        foreach (DataTable table in ds.Tables)
        {
          nCount += table.Rows.Count;
          table.Dispose();
        }
      }

      dataSet.AcceptChanges();

      return nCount;
    }

    public void Rollback()
    {
      if (!dataSet.HasChanges())
        return;
      dataSet.RejectChanges();
    }

    public void Dispose()
    {
      if (dataSet != null)
      {
        dataSet.Dispose();        
      }
    }
  }
}
