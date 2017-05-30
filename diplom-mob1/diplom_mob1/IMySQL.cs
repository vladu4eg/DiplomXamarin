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
        Task<List<string>> GetTakeTest();
        Task<List<string>> GetTakeTask();
        Task<List<string>> GetNameId(int idtest);
        void PutAnswerTest(float ocenka, int answer, string[] answer_task);
        void PutTakeStudentTest(int idtest, int idstudent);
        Task<List<string>> GetTakeNameTest();
    }

}
