using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MySql.Data.MySqlClient;
using Xamarin.Forms;
using System.Diagnostics;

[assembly: Dependency(typeof(diplom_mob1.Droid.MySQL))]
namespace diplom_mob1.Droid
{
    public class MySQL : IMySQL
    {
        public Task<string> GetAccountReg()
        {
            try
            {
                new I18N.West.CP1250();
                MySqlConnection sqlconn;
                string connsqlstring = "Server=37.140.192.136;Port=3306;database=u0142932_server;User Id=u0142_server;Password=vlad19957;charset=utf8";
                sqlconn = new MySqlConnection(connsqlstring);
                sqlconn.Open();
                string queryString = "select count(0) from test";
                MySqlCommand sqlcmd = new MySqlCommand(queryString, sqlconn);
                String result = sqlcmd.ExecuteScalar().ToString();
                //LblMsg.Text = result + " accounts in DB";
                sqlconn.Close();
                return Task<string>.FromResult(result + " accounts in DB");
            }
            catch (Exception ex)
            {
                return Task<string>.FromResult(ex.Message);
            }
        }

        public async Task<string> GetAccountAuth(string login, string pass)
        {
            try
            {
                string Connect = "Database=u0354899_diplom;Data Source=31.31.196.162;User Id=u0354899_vlad;Password=vlad19957;charset=cp1251";
                MySql.Data.MySqlClient.MySqlConnection myConnection = new MySql.Data.MySqlClient.MySqlConnection(Connect);
                MySql.Data.MySqlClient.MySqlCommand myCommand = new MySql.Data.MySqlClient.MySqlCommand();
                myConnection.Open();
                myCommand.Connection = myConnection;
                myCommand.CommandText = string.Format("SELECT login FROM Student WHERE login='{0}' AND password='{1}' ", login, pass);//запрос: если есть такой логин в таблице
                myCommand.Prepare();//подготавливает строку
                myCommand.ExecuteNonQuery();//выполняет запрос
                string LoginGlobal = (string)myCommand.ExecuteScalar();//результат запроса
                if (LoginGlobal == login)
                {
                    MainPage.AuthStudent = true;
                    myConnection.Close();
                    return await Task<string>.FromResult("Вы зашли как студент");
                }

                else
                {
                    myCommand.CommandText = string.Format("SELECT login FROM teacher WHERE login='{0}' AND password='{1}' ", login, pass);//запрос: если есть такой логин в таблице
                    myCommand.Prepare();//подготавливает строку
                    myCommand.ExecuteNonQuery();//выполняет запрос
                    LoginGlobal = (string)myCommand.ExecuteScalar();//результат запроса
                    if (LoginGlobal == login)
                    {
                        MainPage.AuthTeacher = true;
                        myConnection.Close();
                        return await Task<string>.FromResult("Вы зашли как преподаватель");
                    }

                    else
                    {
                        myConnection.Close();
                        return await Task<string>.FromResult("Логин или пароль не совпадают");
                    }

                }

            }
            catch (Exception ex)
            {
                return await Task<string>.FromResult(ex.Message);
            }
        }


        public async Task<List<String>> GetTakeTest()
        {
                List<String> products = new List<String>();
            try
            {
                string connsqlstring = "Database=u0354899_diplom;Data Source=31.31.196.162;User Id=u0354899_vlad;Password=vlad19957;charset=utf8";
                MySqlConnection sqlconn = new MySqlConnection(connsqlstring);
                sqlconn.Open();

                DataSet tickets = new DataSet();
                string queryString = "select name,var1,var2,var3,var4,answer,pic from vopros ";
                MySqlDataAdapter adapter = new MySqlDataAdapter(queryString, sqlconn);
                adapter.Fill(tickets, "Item");

                foreach (DataRow row in tickets.Tables["Item"].Rows)
                {
                   products.Add(row[0].ToString());
                    products.Add(row[1].ToString());
                    products.Add(row[2].ToString());
                    products.Add(row[3].ToString());
                    products.Add(row[4].ToString());
                    products.Add(row[5].ToString());
                    products.Add(row[6].ToString());
                }
               

                sqlconn.Close();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
                return await Task<List<String>>.FromResult(products);
        }
    }
}