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
        public string CreateQuestion(string answer, string question, string questionType, 
            string questionAnswer, List<string> questionMcsaOptions, bool modifyAQuestion)
        {           
            string combinedString = questionType + "|" + question.Trim() + "|" + questionAnswer.Trim();

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

            if (!modifyAQuestion)
            {
                if (!fm.AddQuestionToFile(combinedString))
                {
                    Console.WriteLine(Environment.NewLine +
                        "Failed to add question: question already exists in the quiz.");
                    return "";
                }
            }
            return combinedString;
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
        public int LogAllQuestions()
        {
            Console.WriteLine("These are all the questions that are currently saved in the quiz:" + 
                Environment.NewLine);

            int questionNumber = 0;
            foreach (List<string> item in fm.ReadFile())
            {
                Console.WriteLine(++questionNumber + ". " + item[1]);
            }
            return questionNumber;
        }
        public void RunQuiz()
        {
            quiz.Run();
        }
    }
}