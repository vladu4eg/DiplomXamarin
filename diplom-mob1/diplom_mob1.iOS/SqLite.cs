using System;
using Xamarin.Forms;
using System.IO;
using diplom_mob1.Droid;

[assembly: Dependency(typeof(diplom_mob1.SQLite))]
namespace diplom_mob1
{
    public class SQLite : ISqLite
    {
        public SQLite() { }
        public string GetDatabasePath(string sqliteFilename)
        {
            // определяем путь к бд
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            //нужно указать
            string libraryPath = Path.Combine(documentsPath, "..", "Library"); // папка библиотеки
            var path = Path.Combine(libraryPath, sqliteFilename);

            return path;
        }
    }
}