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
        private Deck deck = new Deck();
        public UserInterface() { }

        public virtual void Run()
        {
            Quiz quiz = new Quiz(deck); //Ändras inte vid ResetDeck();

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
            bool returnValue;

            if (numberOfAction == 1)
            {
                ResetDeck();
                quiz.Run();
                returnValue = true;
            }
            else if (numberOfAction == 2)
            {


                returnValue = true;
            }
            else if (numberOfAction == 3)
            {


                returnValue = true;
            }
            else if (numberOfAction == 4)
            {


                returnValue = true;               
            }
            else
            {
                Console.Write("Closing application. Press any key.");
                return false;
            }
            Console.WriteLine(Environment.NewLine);
            LogInstructions();
            return returnValue;
        }
        public void ResetDeck()
        {
            deck = new Deck();
        }
    }
}
