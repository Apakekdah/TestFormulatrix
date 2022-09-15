using OOT.Interface;
using System;
using System.Collections.Generic;

namespace OOT_Sample
{
    /*
        This is my work for current test, in here i'm using Repository, Unit Of Work, Model and Bussiness Library.
        It's not fully Repositry, but only simulate in using repository.
        My point in this work it's, even if we change data store, we're don't need to re-write any bussiness logic (Repository base Implement in another class base by current DB we use)
        We just only create New Unit Of Work and Repository base for the storage data (Ex. Sql, My Sql, Oracle, etc).
        But, in here i'll make it simple, Repository base is Implement in Bussiness Library.

        But in real programming, there are 3 class to implement Unit Of work Patern
        1. UnitOfWork implement from IUnitOfWork (with only have function like Commit And Rollback)
        2. RepositoryBase implement from IRepository (this is where data is manipulate like FindByKey, Delete, Get, GetMany, etc..)
        3. Bussiness Class inherits from IRepository and IUnitOfWork (this is where are Any logic for Api is using, like Ex. FindDataByItemType, etc.. or Commit/Rollback data).

        For now, this is all I can do, hopefully these results can be your consideration

        In this project i'm using .NET Framework 3.5 with C# programming language.
     */
    class Program
    {
        static void Main(string[] args)
        {
            string json = @"{
    'glossary': {
        'title': 'example glossary',
		'GlossDiv': {
            'title': 'S',
			'GlossList': {
                'GlossEntry': {
                    'ID': 'SGML',
					'SortAs': 'SGML',
					'GlossTerm': 'Standard Generalized Markup Language',
					'Acronym': 'SGML',
					'Abbrev': 'ISO 8879:1986',
					'GlossDef': {
                        'para': 'A meta-markup language, used to create markup languages such as DocBook.',
						'GlossSeeAlso': ['GML', 'XML']
                    },
					'GlossSee': 'markup'
                }
            }
        }
    }
}";

            string xml = @"<!DOCTYPE glossary PUBLIC \""-//OASIS//DTD DocBook V3.1//EN\"">
 <glossary><title>example glossary</title>
  <GlossDiv><title>S</title>
   <GlossList>
    <GlossEntry ID=\""SGML\"" SortAs=\""SGML\"">
     <GlossTerm>Standard Generalized Markup Language</GlossTerm>
     <Acronym>SGML</Acronym>
     <Abbrev>ISO 8879:1986</Abbrev>
     <GlossDef>
      <para>A meta-markup language, used to create markup
languages such as DocBook.</para>
      <GlossSeeAlso OtherTerm=\""GML\"">
      <GlossSeeAlso OtherTerm=\""XML\"">
     </GlossDef>
     <GlossSee OtherTerm=\""markup\"">
    </GlossEntry>
   </GlossList>
  </GlossDiv>
 </glossary>";


            OOT.Formulatrix ft = new OOT.Formulatrix();
            string tmp = null;
            int typ = 0;

            ft.Initialize();
            Console.WriteLine("Initialize object 'Formulatrix' Ok");
            Console.WriteLine();

            ft.Register("Json", json, ItemType.JSON.GetHashCode());
            Console.WriteLine("Create Item Name 'Json' Ok");
            Console.WriteLine();

            ft.Register("Xml", xml, ItemType.Xml.GetHashCode());
            Console.WriteLine("Create Item Name 'Json' Ok");
            Console.WriteLine();

            try
            {
                ft.Register("Bson", json, ItemType.Xml.GetHashCode());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Create Item Name 'Bson' Failed");
                Console.WriteLine(ex);
                Console.WriteLine();
            }

            try
            {
                // Re-register same Item Name, will cause exception
                ft.Register("Json", json, ItemType.JSON.GetHashCode());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Create Item Name 'Json' Failed");
                Console.WriteLine(ex);
                Console.WriteLine();
            }

            tmp = ft.Retrieve("Json");
            Console.WriteLine(string.Format("Got Content for 'Json' \n'{0}'", tmp));
            Console.WriteLine();

            tmp = ft.Retrieve("Xml");
            Console.WriteLine(string.Format("Got Content for 'Xml' \n'{0}'", tmp));
            Console.WriteLine();

            typ = ft.GetType("Json");
            Console.WriteLine(string.Format("Got Type for 'Json' \n'{0}'", ((ItemType)typ)).ToString());
            Console.WriteLine();

            typ = ft.GetType("Xml");
            Console.WriteLine(string.Format("Got Type for 'Xml' \n'{0}'", ((ItemType)typ)).ToString());
            Console.WriteLine();

            try
            {
                // Deregister Item Name with unknown name, will cause exception
                ft.Deregister("Bson");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Deregister 'Bson' Failed");
                Console.WriteLine(ex);
                Console.WriteLine();
            }

            ft.Deregister("Json");
            Console.WriteLine("Deregister Item Name 'Json' Ok");
            Console.WriteLine();

            ft.Deregister("Xml");
            Console.WriteLine("Deregister Item Name 'Json' Ok");
            Console.WriteLine();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press enter to exit !");

            Console.ReadLine();
        }
    }
}
