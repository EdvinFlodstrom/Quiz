using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizLibrary
{
    public interface IManager
    {
        bool AddQuestion(string questionCardString);
        List<QuestionCard> Read();
        void RemoveOrModifyQuestion(int numberOfQuestion, string modifiedQuestion = "");
    }
}