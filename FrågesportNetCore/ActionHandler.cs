using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrågesportNetCore
{
    internal class ActionHandler
    {
        FileManager fm = new FileManager();
        Quiz quiz = new Quiz();
        public ActionHandler() { }        
        public bool CreateQuestion(string answer, string question, string questionType, 
            string questionAnswer, List<string> questionMcsaOptions)
        {           
            string combinedString = questionType + "|" + question + "|" + questionAnswer;

            if (questionType == "MCSACard")
            {
                string combinedMCSAOptionsString = "";

                foreach (string item in questionMcsaOptions)
                {
                    combinedMCSAOptionsString += item;
                    combinedMCSAOptionsString += ",";
                }

                combinedMCSAOptionsString = combinedMCSAOptionsString.Remove(combinedMCSAOptionsString.Length - 1);

                combinedString += "|";
                combinedString += combinedMCSAOptionsString;
            }

            fm.AddQuestionToFile(combinedString);
            return true;
        }
        public string DoWhileFunction(int highestAllowedNumber = 0)
        {
            string answer = "";
            int intAnswer = 0;

            answer = Console.ReadLine();
            if (answer == "")
            {
                answer = null;
            }
            if (highestAllowedNumber > 0)
            {
                bool successfulParse = int.TryParse(answer, out intAnswer);
                if (!successfulParse)
                {
                    intAnswer = 0;
                    answer = null;
                }

                for (int i = 1; i <= highestAllowedNumber; i++)
                {
                    if (intAnswer == i)
                    {
                        answer = intAnswer.ToString();
                        break;
                    }
                    else
                    {
                        answer = null;
                    }
                }
            }
            return answer;
        }
        public void RunQuiz()
        {
            quiz.Run();
        }
    }
}