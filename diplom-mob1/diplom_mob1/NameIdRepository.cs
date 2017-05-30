using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SQLite;
using Xamarin.Forms;

namespace diplom_mob1
{
    public class NameIdReposirory
    {
        SQLiteConnection database;
        public NameIdReposirory(string filename)
        {
            try
            {
                string databasePath = DependencyService.Get<ISqLite>().GetDatabasePath(filename);
                database = new SQLiteConnection(databasePath);
                database.CreateTable<NameId>();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }
        public List<NameId> GetItems()
        {
            return (from i in database.Table<NameId>() select i).ToList();

        }

        public List<NameId> GetName(int idstudent)
        {
            try
            {
                string txtSQLQuery = string.Format("select Name where IdStudent = '{1}'", idstudent);
                return database.Query<NameId>(txtSQLQuery);
            }
            catch(Exception ex)
            {
                string txtSQLQuery = string.Format("select Name where IdStudent = '{1}'", idstudent);
                return database.Query<NameId>(txtSQLQuery);
            }

        }

        public NameId GetItem(int id)
        {
            return database.Get<NameId>(id);
        }
        public int DeleteItem(int id)
        {
            return database.Delete<NameId>(id);
        }
        public int SaveItem(NameId item)
        {
            if (item.Id != 0)
            {
                try
                {
                    database.Update(item);
                    return item.Id;
                }
                catch(Exception ex)
                {
                     Debug.WriteLine(ex.Message);
                    int i = 0;
                    return i;
                }
            }
            else
            {
                return database.Insert(item);
            }
        }
    }
}