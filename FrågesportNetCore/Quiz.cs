using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FrågesportNetCore
{
    internal class Quiz
    {
        private int correctAnswers = 0;
        private int totalAnswers = 0;
        private Deck deck;
        public Quiz()
        {
            
        }
        public void Run()
        {
            deck = new Deck();

            correctAnswers = 0;
            totalAnswers = 0;
            Console.Write("Welcome to this quiz! You will be presented with a question, for which you may submit an answer." + 
                Environment.NewLine + "Press any key to continue.");
            Console.ReadLine();
            Console.WriteLine();
            
            int deckLength = deck.Cards.Count;

            for (int i = 0; i < deckLength; i++)
            {
                QuestionCard card = deck.Draw();
                IncrementAndDisplayAnswers(CheckAnswer(card));
            }
            Console.WriteLine("That's all the cards. Thanks for playing!" + Environment.NewLine + 
                "Your final result was: " + correctAnswers + "/" + totalAnswers + "." + 
                Environment.NewLine);
            Console.WriteLine(correctAnswers == totalAnswers ? "Perfect score!" : 
                correctAnswers == 0 ? "All incorrect..." : "Not bad!");
        }
        private bool AnswerIsGood(string answer, bool answerHasToBeInt, bool mcsa)
        {
            if (answer == "")
            {
                Console.WriteLine("Please input an answer.");
                return false;
            }
            if (answerHasToBeInt)
            {
                try
                {
                    int intAns = Convert.ToInt32(answer);
                    if (mcsa)
                    {
                        for (int i = 1; i < 6; i++)
                        {
                            if (intAns == i)
                            {
                                return true;
                            }
                        }
                        Console.WriteLine("Please input a number within the given range, 1-5.");
                        return false;
                    }
                    return true;
                }
                catch
                {
                    Console.WriteLine("Please input numbers, not letters.");
                    return false;
                }
            }
            else
            {
                try
                {
                    Convert.ToInt32(answer);
                    Console.WriteLine("Please input letters, not numbers.");
                    return false;
                }
                catch
                {
                    return true;
                }
            }
        }
        private bool CheckAnswer(QuestionCard card)
        {
            bool answerHasToBeInt = true;
            bool mcsa = false;
            try
            {
                Convert.ToInt16(card.CorrectAnswer);
            }
            catch
            {
                answerHasToBeInt = false;
            }

            Console.Write(card.Question);

            if (card.GetType() == typeof(MCSACard))
            {
                mcsa = true;
                Console.WriteLine();

                for (int i = 1; i < 6; i++)
                {
                    Console.WriteLine(i + ". " + card.McsaOptions[i-1]);
                }                
            }

            string answer;
            do
            {
                if (mcsa)
                {
                    Console.Write(Environment.NewLine + "Choose your option, 1-5: ");
                }
                else
                {
                    Console.Write(Environment.NewLine + "Type your answer: ");
                }
                answer = Console.ReadLine();
            } while (!AnswerIsGood(answer, answerHasToBeInt, mcsa));

            string[] splitStr = card.CorrectAnswer.Split(' ');

            bool answerTrue = false;

            foreach (string item in splitStr)
            {
                if (answer.ToLower().Contains(item))
                {
                    answerTrue = true;
                }
                else
                {
                    Console.WriteLine(Environment.NewLine + "Incorrect.");
                    answerTrue = false;
                    return false;
                }
            }
            if (answerTrue)
            {
                Console.WriteLine(Environment.NewLine + "Correct!");
                return true;
            }
            return false;
        }
        private void IncrementAndDisplayAnswers(bool answeredCorrectly)
        {
            totalAnswers++; 
            if (answeredCorrectly)
            {
                correctAnswers++;
            }

            Console.WriteLine(Environment.NewLine + 
                "Cards answered correctly: " + 
                correctAnswers + "/" + totalAnswers + Environment.NewLine);
        }
        private void ResetDeck()
        {
            deck = new Deck();
        }
    }
}
