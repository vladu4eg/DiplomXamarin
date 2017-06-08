using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace diplom_mob1
{
    public interface IMySQL
    {
        Task<string> GetAccountAuth(string login, string pass);
        Task<bool> GetAccountReg(string login, string pass, string lastname, string firstname, string middlename, string group, string zachetka);
        Task<bool> GetAccountReg(string login, string pass, string lastname, string firstname, string middlename);
        Task<List<string>> GetTakeTest();
        Task<List<string>> GetTakeTask();
        Task<List<string>> GetTakeNameTest();
        Task<List<string>> GetResult();
        Task<bool> CheckTestForStudent();
        void PutAnswerTest(int trueanswer, int answer, string[] answer_task);
        void PutTakeStudentTest(int idtest, int idstudent);

    }

}
