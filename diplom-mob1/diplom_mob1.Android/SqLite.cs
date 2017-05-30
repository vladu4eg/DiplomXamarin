using System;
using diplom_mob1.Droid;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(diplom_mob1.Droid.SqLite))]
namespace diplom_mob1.Droid
{
    public class SqLite : ISqLite
    {
        public SqLite() { }
        public string GetDatabasePath(string sqliteFilename)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, sqliteFilename);
            return path;
        }
    }
}