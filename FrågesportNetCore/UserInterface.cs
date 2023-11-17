using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace FrågesportNetCore
{
    internal class UserInterface
    {
        FileManager fm = new FileManager();
        public UserInterface() { }

        public virtual void Run()
        {
            Quiz quiz = new Quiz();

            LogInstructions();
            string answer;

            do
            {
                answer = Console.ReadLine();
            } while (TestAnswer(answer, 5, quiz));                                              
        }
        private void LogInstructions()
        {
            Console.Write("Welcome to the quiz UI! Your options are as follow:" + Environment.NewLine +
                "1. Take the quiz." + Environment.NewLine +
                "2. Add a question to the quiz." + Environment.NewLine +
                "3. Remove a question from the quiz." + Environment.NewLine +
                "4. Modify a question in the quiz." + Environment.NewLine +
                "5. Close the application." + Environment.NewLine + Environment.NewLine +
                "Enter your answer: 1-5: ");
        }
        private bool TestAnswer(string answer, int highestAcceptableNumber, Quiz quiz)
        {
            int intAnswer;
            try
            {
                intAnswer = Convert.ToInt16(answer);
            }
            catch
            {
                Console.Write("Please input a number, in the given range: ");
                return true;
            }

            for (int i = 1; i <= highestAcceptableNumber; i++)
            {
                if (intAnswer == i)
                {
                    return PerformAction(intAnswer, quiz);                    
                }
            }
            Console.Write("Please input a number within the given range: ");
            return true;
        }
        private bool PerformAction(int numberOfAction, Quiz quiz)
        {
            Console.WriteLine();

            if (numberOfAction == 1)
            {
                quiz.Run();
            }
            else if (numberOfAction == 2)
            {
                /*
                quiz.AddCardToDeck(new QuestionCard("", ""));
                quiz.AddCardToDeck(new MCSACard("", "", new List<string> {"","","","","" }));
                */

                try
                {
                    Console.WriteLine("What type of question would you like to add?" + Environment.NewLine +
                    "1. Regular question." + Environment.NewLine +
                    "2. Multiple Choice Single Answer question.");
                    string answer = DoWhileFunction(2);

                    string question;
                    string questionType = "";
                    string questionAnswer = "";
                    List<string> questionMcsaOptions = new List<string>();

                    Console.Write("What is the question going to be?");
                    question = DoWhileFunction();

                    if (answer == "1")
                    {
                        questionType = "QuestionCard";
                        Console.Write(Environment.NewLine + "What is the answer for the question going to be?");
                        questionAnswer = DoWhileFunction();                        
                    }
                    else
                    {
                        questionType = "MCSACard";
                        Console.WriteLine(Environment.NewLine + "What are the five options for the question going to be?" +
                            Environment.NewLine);
                        for (int i = 1; i < 6; i++)
                        {
                            Console.Write(i + ".");
                            string option = DoWhileFunction();
                            if (!(questionMcsaOptions.Contains(option)))
                            {
                                questionMcsaOptions.Add(option);
                            }
                            else
                            {
                                throw new Exception();
                            }
                            Console.WriteLine();
                        }
                        Console.WriteLine(Environment.NewLine + "What is the answer going to be? Choose in the range of 1-5.");
                        questionAnswer = DoWhileFunction(5);
                    }

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
                }
                catch
                {
                    Console.WriteLine(Environment.NewLine + "Error detected. For MCSA questions, please do not input the same option twice.");
                }
            }
            else if (numberOfAction == 3)
            {


            }
            else if (numberOfAction == 4)
            {


            }
            else
            {
                Console.Write("Closing application. Press any key.");
                return false;
            }
            Console.WriteLine(Environment.NewLine);
            LogInstructions();
            return true;
        }
        private string DoWhileFunction(int highestAllowedNumber = 0)
        {
            Console.WriteLine();
            string answer = "";
            int intAnswer = 0;
            do
            {
                try
                {
                    Console.Write("Enter answer: ");
                    answer = Console.ReadLine();
                    if (answer == "")
                    {
                        answer = null;
                    }
                    if (highestAllowedNumber > 0)
                    {
                        intAnswer = Convert.ToInt32(answer);

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
                }
                catch
                {                    
                    answer = null;
                }
                if (answer == null)
                {
                    Console.WriteLine("Incorrectly inputted answer. Please verify it and try again.");
                }
            } while (answer  == null);

            return answer;
        }
    }
}
