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
        ActionHandler handler = new ActionHandler();
        public UserInterface() { }
        public void Run()
        {
            string answer;

            do
            {
                LogInstructions();
                answer = Console.ReadLine();
            } while (PerformAction(answer));
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
        private bool PerformAction(string answer)
        {
            Console.WriteLine();
            if (answer == "1")
            {
                handler.RunQuiz();
            }
            else if (answer == "2")
            {
                Console.WriteLine("What type of question would you like to add?" + Environment.NewLine +
                "1. Regular question." + Environment.NewLine +
                "2. Multiple Choice Single Answer question.");

                answer = VerifyAnswer(2);

                Console.WriteLine("What is the question going to be?");
                string question = VerifyAnswer();
                string questionType = "";
                string questionAnswer = "";
                List<string> questionMcsaOptions = new List<string>();

                if (answer == "1")
                {
                    questionType = "QuestionCard";
                    Console.Write(Environment.NewLine + "What are the words that the answer has to include for it to be correct?");
                    questionAnswer = VerifyAnswer();
                }
                else
                {
                    questionType = "MCSACard";
                    Console.WriteLine(Environment.NewLine + "What are the five options for the question going to be?" +
                        Environment.NewLine);
                    for (int i = 1; i < 6; i++)
                    {
                        Console.Write(i + ".");
                        string option = VerifyAnswer();
                        if (!(questionMcsaOptions.Contains(option)))
                        {
                            questionMcsaOptions.Add(option);
                        }
                        else
                        {
                            Console.WriteLine("Please do not input the same answer twice for MCSA questions." + Environment.NewLine);
                            return true;
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine(Environment.NewLine + "What is the answer going to be? Choose in the range of 1-5.");
                    questionAnswer = VerifyAnswer(5);
                }

                handler.CreateQuestion(answer, question, questionType, questionAnswer, questionMcsaOptions);
            }
            else if (answer == "3")
            {

            }
            else if (answer == "4")
            {

            }
            else if (answer == "5")
            {
                Console.Write("Closing application. Press any key.");
                return false;
            }
            else
            {
                Console.WriteLine("Please enter a number in the range 1-5.");
            }
            Console.WriteLine();
            return true;
        }
        private string VerifyAnswer(int highestAllowedNumber = 0)
        {
            Console.WriteLine();
            string answer = "";

            string answerFormat = "Enter your answer";

            if (highestAllowedNumber > 0)
            {
                answerFormat += " in the range 1-" + highestAllowedNumber + ": ";
            }
            else
            {
                answerFormat += ": ";
            }

            do
            {
                if (answer == null)
                {
                    Console.WriteLine("Please verify that you entered a valid answer." 
                        + Environment.NewLine);
                }
                Console.Write(answerFormat);
                answer = handler.DoWhileFunction(highestAllowedNumber);
            } while (answer == null);

            return answer;
        }
    }
}