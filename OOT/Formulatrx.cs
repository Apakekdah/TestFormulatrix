using OOT.BLL;
using OOT.Interface;
using OOT.Model;
using System;
using System.Data;
using System.IO;

namespace OOT
{
  public class Formulatrix : IFormulaTrix
  {
    const string FILE_NAME = "OOD_FT.xml";

    readonly string path;
    readonly string fullPath;
    bool isInitialize;

    BLL_OOT ottBLL;

    public Formulatrix()
    {
      Initialize();
    }

    //public Formulatrix() : this(System.AppDomain.CurrentDomain.BaseDirectory) { }

    //public Formulatrix(string path)
    //{
    //  if (string.IsNullOrEmpty(path))
    //    throw new ArgumentNullException("path");
    //  else if (!Directory.Exists(path))
    //    throw new DirectoryNotFoundException("path");

    //  this.fullPath = Path.Combine(path, FILE_NAME);

    //}
    
    #region IFormulaTrix

    public void Register(string itemName, string itemContent, int itemType)
    {
      if (string.IsNullOrEmpty(itemName))
        throw new ArgumentNullException("itemName");
      else if (string.IsNullOrEmpty(itemContent))
        throw new ArgumentNullException("itemContent");

      if (ottBLL.Exists(itemName))
        throw new DuplicateNameException(string.Format("Item name for '{0}' already exists.", itemName));

      if (itemType != ItemType.JSON.GetHashCode() && itemType != ItemType.Xml.GetHashCode())
      {
        throw new Exception("Unknown Item Type");
      }

      if (itemType == ItemType.JSON.GetHashCode())
      {
        if (!IsValidJSON(itemContent))
          throw new InvalidDataException("Invalid content for JSON");
      }
      else if (itemType == ItemType.Xml.GetHashCode())
      {
        if(!IsValidXml(itemContent))
          throw new InvalidDataException("Invalid content for XML");
      }

      ottBLL.AddOrUpdate(itemName, itemContent, itemType);

      ottBLL.Commit();
    }

    public string Retrieve(string itemName)
    {
      OOTEntity entity = ottBLL.FindByKey(itemName);
      if (entity == null)
        return null;

      return entity.ItemContent;
    }

    public int GetType(string itemName)
    {
      OOTEntity entity = ottBLL.FindByKey(itemName);
      if (entity == null)
        return -1;

      return entity.ItemType;
    }

    public void Deregister(string itemName)
    {
      OOTEntity entity = ottBLL.FindByKey(itemName);
      if (entity == null)
        throw new Exception(string.Format("Item name with '{0}' not found", itemName));

      ottBLL.Delete(entity);

      ottBLL.Commit();
    }

    public void Initialize()
    {
      if (isInitialize)
        return;

      /// for IoC maybe can be use Lifetimescope.Get<IRepository<OOTEntity>>
      ottBLL = new BLL_OOT();

      isInitialize = true;

      //FileInfo fi = new FileInfo(fullPath);

      //if (fi.Exists)
      //{
      //  tableFT = InitStructure();

      //  using (FileStream fs = fi.OpenRead())
      //  {
      //    LoadData(tableFT, fs);
      //  }
      //}
    }

    #endregion
    
    private bool IsValidJSON(string content)
    {
      if (string.IsNullOrEmpty(content))
        return false;

      string tmp = content.Trim().Substring(0, 1);
      return "{".Equals(tmp) || "[".Equals(tmp);
    }

    private bool IsValidXml(string content)
    {
      if (string.IsNullOrEmpty(content))
        return false;

      string tmp = content.Trim().Substring(0, 1);
      return "<".Equals(tmp);
    }

  }
}