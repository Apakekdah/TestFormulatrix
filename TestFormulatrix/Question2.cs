using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TestFormulatrix
{
    /*
     Given these two tables:
    Table: USA_CUSTOMERS (USA)
        ID	Name
        1	Thomas
        3	Cindy
	
    Table: EU_CUSTOMERS (EU)
	    ID	Name
        2	Francois
        1	Thomas

    Questions :
        
        Select USA.NAME, EU.NAME
        From USA, EU
        Where USA.ID = EU.ID	

        Select USA.NAME, EU.NAME
        From USA, EU
        Where USA.ID = EU.ID (+)
            ((+) is outer join in Oracle)

        Select USA.NAME, EU.NAME
        From USA, EU

    Discussion :
        We use those tables to keep track of our European and American customers.  
        Please provide a critique to that table design (is it good?  How could it be better?).

    Answers :
        CREATE TABLE USA (
            ID int,
            Name varchar(255)
        ); 

        CREATE TABLE EU (
            ID int,
            Name varchar(255)
        ); 

        Insert Into USA(ID, Name) Values
        (1, 'Thomas'),
        (3, 'Cindy');

        Insert Into EU(ID, Name) Values
        (2, 'Francois'),
        (1, 'Thomas');

        Select USA.NAME, EU.NAME
        From USA, EU
        Where USA.ID = EU.ID


        Select USA.NAME, EU.NAME
        From USA
        Left Join EU on USA.ID = EU.ID

        Select USA.NAME, EU.NAME
        From USA, EU
    Tips :
        - Column ID Should Be Add as Primary key or if not possible, you need to add some Indexing
        - In Sql Server Joining table in Where is not recommended
     */

    class FakeSqlTabel
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    class Question2
    {
        readonly static List<FakeSqlTabel> lstUSA = new List<FakeSqlTabel>
        {
            new FakeSqlTabel{ ID = 1, Name = "Thomas"},
            new FakeSqlTabel{ ID = 3, Name = "Cindy"},
        };
        readonly static List<FakeSqlTabel> lstEU = new List<FakeSqlTabel>
        {
            new FakeSqlTabel{ ID = 2, Name = "Francois"},
            new FakeSqlTabel{ ID = 1, Name = "Thomas"},
        };

        public static IEnumerable<(string, string)> Q1()
        {
            /*
              Select USA.NAME, EU.NAME
                From USA, EU
                Where USA.ID = EU.ID
            */

            return (from usa in lstUSA
                         join eu in lstEU on usa.ID equals eu.ID
                         select (usa.Name, eu.Name));
        }

        public static IEnumerable<(string, string)> Q2()
        {
            /*
             Select USA.NAME, EU.NAME
                From USA, EU
                Where USA.ID = EU.ID (+)
                    ((+) is outer join in Oracle)
            */
            return (from usa in lstUSA
                         join eu in lstEU on usa.ID equals eu.ID into lj
                         from result in lj.DefaultIfEmpty()
                         select (usa.Name, result?.Name));
        }

        public static IEnumerable<(string, string)> Q3()
        {
            /*
             Select USA.NAME, EU.NAME
                From USA, EU
            */

            return (from usa in lstUSA
                         from eu in lstEU
                         select (usa.Name, eu.Name));
        }
    }
}
