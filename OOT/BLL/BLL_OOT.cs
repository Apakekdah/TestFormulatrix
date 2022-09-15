using OOT.Data;
using OOT.Interface;
using OOT.Model;
using System;
using System.Data;
using System.IO;

namespace OOT.BLL
{
  // Assume this is BLL, but Repository is Implement in this Class, not in Another class as The Real Repository
  public class BLL_OOT : IRepository<OOTEntity>
  {
    // As Repository
    DataTable tableFT;

    const string TABLE_NAME = "Formulatrix";
    IUnitOfWork unitFoWork;

    public BLL_OOT()
    {
      /// If you're using IoC you simply get with this
      /// Usualy I'm using AutoFac so will be called LifetimeScope.GetInstance<IRepository<OOTEntity>>();
      /// Then Repository automatically called Repositoy for Entity Form for IFormulaTrix with OOTEntity
      /// But, in here i'm using simple DataTable as Repository
      /// And unit of work as DataSet

      #region Initialize Repo for Formulatrix

      unitFoWork = new UnitOfWork();

      tableFT = unitFoWork.WorkSpace(TABLE_NAME);

      InitStructure();

      #endregion
    }

    #region Column Name

    const string COL_ITEMNAME = "itemName";
    const string COL_ITEMCONTENT = "itemContent";
    const string COL_ITEMTYPE = "itemType";

    #endregion

    #region All Bussiness Function

    #region Repo Function

    public OOTEntity FindByKey(string itemName)
    {
      DataRow row = FindByKeyAsRow(itemName);
      if (row == null)
        return null;
      return new OOTEntity()
      {
        ItemName = row.Field<string>(COL_ITEMNAME),
        ItemContent = row.Field<string>(COL_ITEMCONTENT),
        ItemType = row.Field<int>(COL_ITEMTYPE),
      };
    }

    public void AddOrUpdate(OOTEntity entity)
    {
      AddOrUpdate(entity.ItemName, entity.ItemContent, entity.ItemType);
    }

    public void Delete(OOTEntity oot)
    {
      DataRow row = FindByKeyAsRow(oot.ItemName);
      if (oot == null)
        return;
      Delete(row);
    }

    public void Delete(string key)
    {
      DataRow row = FindByKeyAsRow(key);
      Delete(row);
    }

    public bool Exists(string key)
    {
      return (FindByKeyAsRow(key) != null);
    }

    #endregion
    
    private DataRow FindByKeyAsRow(string itemName)
    {
      DataRow[] rows = tableFT.Select(string.Concat(COL_ITEMNAME, "='", itemName, "'"));
      if ((rows == null) || (rows.Length < 1))
        return null;
      return rows[0];
    }

    public void AddOrUpdate(string itemName, string itemContent, int itemType)
    {
      DataRow row = FindByKeyAsRow(itemName);
      if (row == null)
      {
        row = tableFT.NewRow();
        row[COL_ITEMNAME] = itemName;
        tableFT.Rows.Add(row);
      }

      row.BeginEdit();

      row[COL_ITEMCONTENT] = itemContent;
      row[COL_ITEMTYPE] = itemType;

      row.EndEdit();
    }

    private void Delete(DataRow row)
    {
      if (row == null)
        return;
      tableFT.Rows.Remove(row);
    }

    #endregion

    #region Private

    private void InitStructure()
    {
      DataColumn column = new DataColumn()
      {
        AllowDBNull = false,
        Caption = "Item Name",
        ColumnName = COL_ITEMNAME,
        DataType = typeof(string),
        Unique = true,
      };
      
      tableFT.Columns.Add(column);
      
      tableFT.Columns.Add(new DataColumn()
      {
        AllowDBNull = true,
        Caption = "Item Content",
        ColumnName = COL_ITEMCONTENT,
        DataType = typeof(string),
        Unique = false,
      });

      tableFT.Columns.Add(new DataColumn()
      {
        AllowDBNull = true,
        Caption = "Item Type",
        ColumnName = COL_ITEMTYPE,
        DataType = typeof(int),
        Unique = false,
      });
      
      tableFT.PrimaryKey = new[] { column };
    }

    private void LoadData(DataTable table, Stream stream)
    {
      using (DataSet ds = new DataSet())
      {
        ds.Tables.Add(table);

        try
        {
          ds.ReadXml(stream);
        }
        catch { ;}
      }
    }

    private void StoreData(DataTable table, string toLocation)
    {
      using (DataSet ds = new DataSet())
      {
        ds.Tables.Add(table);

        ds.WriteXml(toLocation);
      }
    }

    #endregion
    
    public int Commit()
    {
      return unitFoWork.Commit();
    }

    public void Rollback()
    {
      unitFoWork.Rollback();
    }
  }
}
