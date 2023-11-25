using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrågesportNetCore
{
    internal class ActionHandler
    {
        List<QuestionCard> questionCards;
        private int correctAnswers = 0;
        private int totalAnswers = 0;
        private int deckLength = 0;

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
                    return "";
                }
            }
            return combinedString;
        }
        public string CheckQuestionAnswer(string answer)
        {
            QuestionCard card = questionCards[totalAnswers++];            

            string[] splitStr = card.CorrectAnswer.Split(' ');

            bool answerTrue = false;

            foreach (string item in splitStr)
            {
                if (answer.ToLower().Contains(item.ToLower()))
                {
                    answerTrue = true;
                }
                else
                {
                    answerTrue = false;
                    return "Incorrect.";
                }
            }
            if (answerTrue)
            {
                correctAnswers++;
                return "Correct!";
            }
            return "Incorrect.";
        }
        public string DoWhileFunction(string answer, int highestAllowedNumber = 0)
        {
            int intAnswer = 0;

            if (answer == "")
            {
                answer = null;
            }
            if (highestAllowedNumber > 0)
            {                
                if (!(int.TryParse(answer, out intAnswer)))
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
        public List<string> GetAllQuestions()
        {
            List<string> listOfAllQuestions = new List<string>();
            int questionNumber = 0;
            foreach (List<string> item in fm.ReadFile())
            {
                listOfAllQuestions.Add(++questionNumber + ". " + item[1]);
            }
            return listOfAllQuestions;
        }
        public string GetCorrectAndTotalAnswers()
        {
            return "Correct answers/total answers: " + correctAnswers + "/" + totalAnswers;
        }
        public List<string> GetNewQuestion()
        {
            List<string> stringOfQuestionDetails = new List<string>();
            
            if (totalAnswers >= deckLength)
            {
                stringOfQuestionDetails.Add(null);
                stringOfQuestionDetails.Add(GetQuizResults());

                return stringOfQuestionDetails;
            }

            QuestionCard card = questionCards[totalAnswers];

            stringOfQuestionDetails.Add(card.Question);

            if (!(card.McsaOptions is null))
            {
                string stringOfMcsaOptions = "";
                for (int i = 1; i < 6; i++)
                {
                    stringOfMcsaOptions += i + ". " + card.McsaOptions[i - 1];
                    if (i < 5)
                    {
                        stringOfMcsaOptions += Environment.NewLine;
                    }
                }
                stringOfQuestionDetails.Add(stringOfMcsaOptions);
            }

            return stringOfQuestionDetails;
        }
        public string GetQuizResults()
        {
            string resultString = "";

            resultString += ("That's all the cards. Thanks for playing!" + Environment.NewLine +
                "Your final result was: " + correctAnswers + "/" + totalAnswers + "." +
                Environment.NewLine);
            resultString += (correctAnswers == totalAnswers ? "Perfect score!" :
                correctAnswers == 0 ? "All incorrect..." : "Not bad!");

            return resultString;
        }
        public string PrepareQuiz()
        {
            questionCards = quiz.Run();

            correctAnswers = 0;
            totalAnswers = 0;
            deckLength = questionCards.Count;

            return "Welcome to the quiz! You will be presented with a question, for which you may submit an answer." +
                Environment.NewLine + "Press any key to continue.";
        }
    }
}