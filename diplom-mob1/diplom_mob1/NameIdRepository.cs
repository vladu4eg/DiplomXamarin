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
        public IEnumerable<NameId> GetItems()
        {
            return (from i in database.Table<NameId>() select i).ToList();

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
                database.Update(item);
                return item.Id;
            }
            else
            {
                return database.Insert(item);
            }
        }
    }
}