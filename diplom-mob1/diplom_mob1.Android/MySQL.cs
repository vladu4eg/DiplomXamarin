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
using System.Collections.ObjectModel;

[assembly: Dependency(typeof(diplom_mob1.Droid.MySQL))]
namespace diplom_mob1.Droid
{
    public class MySQL : IMySQL
    {
        public async Task<bool> GetAccountReg(string login, string pass, string lastname, string firstname, string middlename, string group, string zachetka)
        {
            try
            {

                string Connect = "Database=u0354899_diplom;Data Source=31.31.196.162;User Id=u0354899_vlad;Password=vlad19957;charset=cp1251";
                MySql.Data.MySqlClient.MySqlConnection myConnection = new MySql.Data.MySqlClient.MySqlConnection(Connect);
                MySql.Data.MySqlClient.MySqlCommand myCommand = new MySql.Data.MySqlClient.MySqlCommand();
                myConnection.Open();
                myCommand.Connection = myConnection;
                myCommand.CommandText = string.Format("INSERT INTO Student (FirstName,LastName,MiddleName,groups,login,password,zachetka) " +
                                     "VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", lastname, firstname, middlename, group, login, pass, zachetka);
                myCommand.Prepare();//подготавливает строку
                myCommand.ExecuteNonQuery();//выполняет запрос
                myConnection.Close();
            }
            catch (Exception ex)
            {
                return await Task<bool>.FromResult(false);
            }
            return await Task<bool>.FromResult(true);
        }

        public async Task<bool> GetAccountReg(string login, string pass, string lastname, string firstname, string middlename)
        {
            try
            {

                string Connect = "Database=u0354899_diplom;Data Source=31.31.196.162;User Id=u0354899_vlad;Password=vlad19957;charset=cp1251";
                MySql.Data.MySqlClient.MySqlConnection myConnection = new MySql.Data.MySqlClient.MySqlConnection(Connect);
                MySql.Data.MySqlClient.MySqlCommand myCommand = new MySql.Data.MySqlClient.MySqlCommand();
                myConnection.Open();
                myCommand.Connection = myConnection;
                myCommand.CommandText = string.Format("INSERT INTO Student (FirstName,LastName,MiddleName,login,password) " +
                                     "VALUES('{0}','{1}','{2}','{3}','{4}')", lastname, firstname, middlename, login, pass);
                myCommand.Prepare();//подготавливает строку
                myCommand.ExecuteNonQuery();//выполняет запрос
                myConnection.Close();
            }
            catch (Exception ex)
            {
                return await Task<bool>.FromResult(false);
            }
            return await Task<bool>.FromResult(true);
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
                    myCommand.CommandText = string.Format("SELECT id FROM Student WHERE login='{0}'", login);//запрос: если есть такой логин в таблице
                    myCommand.Prepare();//подготавливает строку
                    myCommand.ExecuteNonQuery();//выполняет запрос
                    Student.idStudent = (int)myCommand.ExecuteScalar();//результат запроса
                    myConnection.Close();
                    MainPage.AuthStudent = true;
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
                        myConnection.Close();
                        MainPage.AuthTeacher = true;
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

        public async Task<bool> CheckTestForStudent()
        {
            bool check = false;
            int test = 0;
            try
            {
                string Connect = "Database=u0354899_diplom;Data Source=31.31.196.162;User Id=u0354899_vlad;Password=vlad19957;charset=cp1251";
                MySql.Data.MySqlClient.MySqlConnection myConnection = new MySql.Data.MySqlClient.MySqlConnection(Connect);
                MySql.Data.MySqlClient.MySqlCommand myCommand = new MySql.Data.MySqlClient.MySqlCommand();
                myConnection.Open();
                myCommand.Connection = myConnection;
                myCommand.CommandText = string.Format("SELECT count(*) From test_history WHERE idstudent = '{0}' AND idtest = '{1}'", Student.idStudent, Student.idTest);//запрос: если есть такой логин в таблице
                myCommand.Prepare();//подготавливает строку
                myCommand.ExecuteNonQuery();//выполняет запрос
                test = Convert.ToInt32(myCommand.ExecuteScalar()) ;//результат запроса

                if(test == 0)
                {
                    check = true;
                }

                myConnection.Close();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            return await Task<bool>.FromResult(check);
        }


        public async Task<List<string>> GetTakeNameTest()
        {
            List<string> vopros = new List<string>();
            try
            {
                string connsqlstring = "Database=u0354899_diplom;Data Source=31.31.196.162;User Id=u0354899_vlad;Password=vlad19957;charset=utf8";
                MySqlConnection sqlconn = new MySqlConnection(connsqlstring);
                sqlconn.Open();

                DataSet tickets = new DataSet();
                string queryString = string.Format("SELECT tests.id,tests.NameTest,tests.pdf FROM tests,StudentTakeTest WHERE StudentTakeTest.idtest=tests.id AND StudentTakeTest.idstudent = '{0}'", Student.idStudent);
                MySqlDataAdapter adapter = new MySqlDataAdapter(queryString, sqlconn);
                adapter.Fill(tickets, "Item");

                foreach (DataRow row in tickets.Tables["Item"].Rows)
                {
                    vopros.Add(row[0].ToString());
                    vopros.Add(row[1].ToString());
                    vopros.Add(row[2].ToString());
                }

                sqlconn.Close();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            return await Task<List<string>>.FromResult(vopros);
        }

        public void PutAnswerTest(double ocenka, int answer, string[] answer_task)
        {
            try
            {
                new I18N.West.CP1250();
                string Connect = "Database=u0354899_diplom;Data Source=31.31.196.162;User Id=u0354899_vlad;Password=vlad19957;charset=utf8";
                MySql.Data.MySqlClient.MySqlConnection myConnection = new MySql.Data.MySqlClient.MySqlConnection(Connect);
                MySql.Data.MySqlClient.MySqlCommand myCommand = new MySql.Data.MySqlClient.MySqlCommand();
                myConnection.Open();
                myCommand.Connection = myConnection;
                myCommand.CommandText = string.Format("INSERT INTO test_history (idstudent,idtest,ocenka,true_quest) VALUES ('{0}','{1}','{2}','{3}')", Student.idStudent,Student.idTest, ocenka, answer);//запрос: если есть такой логин в таблице
                myCommand.Prepare();//подготавливает строку
                myCommand.ExecuteNonQuery();//выполняет запрос
                int idTestH = (int)myCommand.LastInsertedId;//результат запроса
                if (Test.TheTaskIs)
                {   
                    foreach(string str in answer_task)
                    {
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

        public void PutTakeStudentTest(int idtest, int idstudent)
        {
            try
            {
                new I18N.West.CP1250();
                string Connect = "Database=u0354899_diplom;Data Source=31.31.196.162;User Id=u0354899_vlad;Password=vlad19957;charset=utf8";
                MySql.Data.MySqlClient.MySqlConnection myConnection = new MySql.Data.MySqlClient.MySqlConnection(Connect);
                MySql.Data.MySqlClient.MySqlCommand myCommand = new MySql.Data.MySqlClient.MySqlCommand();
                myConnection.Open();
                myCommand.Connection = myConnection;
                myCommand.CommandText = string.Format("INSERT INTO StudentTakeTest (idtest,idstudent) VALUES ('{0}','{1}')", Student.idTest, Student.idStudent);//запрос: если есть такой логин в таблице
                myCommand.Prepare();//подготавливает строку
                myCommand.ExecuteNonQuery();//выполняет запрос
                myConnection.Close();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }

        public async Task<List<String>> GetResult()
        {
            List<String> ListNameId = new List<String>();
            try
            {
                string connsqlstring = "Database=u0354899_diplom;Data Source=31.31.196.162;User Id=u0354899_vlad;Password=vlad19957;charset=utf8";
                MySqlConnection sqlconn = new MySqlConnection(connsqlstring);
                sqlconn.Open();

                DataSet tickets = new DataSet();
                string queryString = string.Format("select tests.NameTest,test_history.ocenka,test_history.true_quest from tests,test_history where test_history.idstudent = '{0}'", Student.idStudent);
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



        public async Task<List<string>> GetTakeTask()
        {
            List<string> task = new List<string>();
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
            return await Task<List<string>>.FromResult(task);
        }
    }
}