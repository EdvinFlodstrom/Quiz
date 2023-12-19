using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizLibrary
{
    public class DatabaseManager : IManager
    {
        public bool AddQuestion(string questionCardString)
        {
            throw new Exception();
        }
        public List<QuestionCard> Read()
        {
            throw new Exception();
        }
        public void RemoveOrModifyQuestion(int numberOfQuestion, string modifiedQuestion = "")
        {

        } 
    }
}
