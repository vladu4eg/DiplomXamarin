using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using Xamarin.Forms;
using diplom_mob1;
using diplom_mob1.Droid;
using System.Data;

[assembly: Dependency(typeof(diplom_mob1.iOS.MySQL))]
namespace diplom_mob1.iOS
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
                    myCommand.CommandText = string.Format("SELECT id FROM Student WHERE login='{0}'", login);//запрос: если есть такой логин в таблице
                    myCommand.Prepare();//подготавливает строку
                    myCommand.ExecuteNonQuery();//выполняет запрос
                    Student.idStudent = (int)myCommand.ExecuteScalar();//результат запроса
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
            List<String> vopros = new List<String>();
            try
            {
                string connsqlstring = "Database=u0354899_diplom;Data Source=31.31.196.162;User Id=u0354899_vlad;Password=vlad19957;charset=utf8";
                MySqlConnection sqlconn = new MySqlConnection(connsqlstring);
                sqlconn.Open();

                DataSet tickets = new DataSet();
                string queryString = string.Format("select name,var1,var2,var3,var4,answer,pic from vopros,tests where tests.id = '{0}'", Student.idTest);
                MySqlDataAdapter adapter = new MySqlDataAdapter(queryString, sqlconn);
                adapter.Fill(tickets, "Item");

                foreach (DataRow row in tickets.Tables["Item"].Rows)
                {
                    vopros.Add(row[0].ToString());
                    vopros.Add(row[1].ToString());
                    vopros.Add(row[2].ToString());
                    vopros.Add(row[3].ToString());
                    vopros.Add(row[4].ToString());
                    vopros.Add(row[5].ToString());
                    vopros.Add(row[6].ToString());
                }


                sqlconn.Close();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            return await Task<List<String>>.FromResult(vopros);
        }

        public void PutAnswerTest(float ocenka, int answer, string[] answer_task)
        {
            try
            {
                new I18N.West.CP1250();
                string Connect = "Database=u0354899_diplom;Data Source=31.31.196.162;User Id=u0354899_vlad;Password=vlad19957;charset=utf8";
                MySql.Data.MySqlClient.MySqlConnection myConnection = new MySql.Data.MySqlClient.MySqlConnection(Connect);
                MySql.Data.MySqlClient.MySqlCommand myCommand = new MySql.Data.MySqlClient.MySqlCommand();
                myConnection.Open();
                myCommand.Connection = myConnection;
                myCommand.CommandText = string.Format("INSERT INTO test_history (idstudent,idtest,ocenka,true_quest) VALUES ('{0}','{1}','{2}','{3}')", Student.idStudent, Student.idTest, ocenka, answer);//запрос: если есть такой логин в таблице
                myCommand.Prepare();//подготавливает строку
                myCommand.ExecuteNonQuery();//выполняет запрос
                if (Test.TheTaskIs)
                {
                    foreach (string str in answer_task)
                    {
                        int idTestH = (int)myCommand.LastInsertedId;//результат запроса
                        myCommand.CommandText = string.Format("INSERT INTO task_history (idstudent,idtest,idhistory_quest,answer) VALUES ('{0}','{1}','{2}','{3}')", Student.idStudent, Student.idTest, idTestH, str);//запрос: если есть такой логин в таблице
                        myCommand.Prepare();//подготавливает строку
                        myCommand.ExecuteNonQuery();//выполняет запрос
                    }

                }
                myConnection.Close();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }

        public async Task<List<String>> GetNameId(int idtest)
        {
            List<String> ListNameId = new List<String>();
            try
            {
                string connsqlstring = "Database=u0354899_diplom;Data Source=31.31.196.162;User Id=u0354899_vlad;Password=vlad19957;charset=utf8";
                MySqlConnection sqlconn = new MySqlConnection(connsqlstring);
                sqlconn.Open();

                DataSet tickets = new DataSet();
                string queryString = string.Format("select id,NameTest,pdf from tests where tests = '{0}'", idtest);
                MySqlDataAdapter adapter = new MySqlDataAdapter(queryString, sqlconn);
                adapter.Fill(tickets, "Item");

                foreach (DataRow row in tickets.Tables["Item"].Rows)
                {
                    ListNameId.Add(row[0].ToString());
                    ListNameId.Add(row[1].ToString());
                    ListNameId.Add(row[2].ToString());
                }


                sqlconn.Close();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            return await Task<List<String>>.FromResult(ListNameId);
        }

        public async Task<List<String>> GetTakeTask()
        {
            List<String> task = new List<String>();
            try
            {
                string connsqlstring = "Database=u0354899_diplom;Data Source=31.31.196.162;User Id=u0354899_vlad;Password=vlad19957;charset=utf8";
                MySqlConnection sqlconn = new MySqlConnection(connsqlstring);
                sqlconn.Open();

                DataSet tickets = new DataSet();
                string queryString = string.Format("select TextTask,PicTask from task, tests where tests.id = '{0}'", Student.idTest);
                MySqlDataAdapter adapter = new MySqlDataAdapter(queryString, sqlconn);
                adapter.Fill(tickets, "Item");

                foreach (DataRow row in tickets.Tables["Item"].Rows)
                {
                    task.Add(row[0].ToString());
                    task.Add(row[1].ToString());
                }


                sqlconn.Close();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            return await Task<List<String>>.FromResult(task);
        }
    }
}