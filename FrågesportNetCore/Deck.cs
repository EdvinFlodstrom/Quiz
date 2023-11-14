using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace FrågesportNetCore
{    
    internal class Deck
    {
        private Random rnd = new Random();
        private List<QuestionCard> cards = new List<QuestionCard>();
        public List<QuestionCard> Cards
        {
            get
            {
                return cards;
            }
        }
        public Deck()
        {
            string docPath =
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string pathAndFileName = Path.Combine(docPath, "quiz_questions.txt");

            using (var sr = new StreamReader(pathAndFileName))
            {
                string row = sr.ReadLine();
                while (row != null)
                {
                    //row == QuestionCard|What is Eyjafjallajökull, and where is it located?|volcano iceland
                    List<string> listOfRow = row.Split('|').ToList(); //Splits into list: "QuestionCard", "[q]" etc.                 
                    if (listOfRow[0] == "QuestionCard")
                    {
                        cards.Add(new QuestionCard(listOfRow[1], listOfRow[2]));
                    }
                    else if (listOfRow[0] == "MCSACard")
                    {                      
                        cards.Add(new MCSACard(listOfRow[1], listOfRow[2], listOfRow[3].Split(',').ToList()));                       
                    }                  
                    row = sr.ReadLine();
                }
            }
            /*cards.AddRange(new List<QuestionCard>() {
                new QuestionCard("What is Eyjafjallajökull, and where is it located? ", "volcano iceland"),
                new QuestionCard("How many cards are in a regular card deck? ", "52"),
                new QuestionCard("How many points are equal to Blackjack, in Blackjack? ", "21"),
                new MCSACard("Which country has the largest landmass? ", "3", new List<string>{"Canada", "USA", "Russia", "China", "Sweden"}),
                new MCSACard("In chess, how many pawns do each side start with? ", "4", new List<string>{"7", "6", "9", "8", "10"}),
                new MCSACard("Which programming language is currently the most used among developers? ", "5", new List<string>{"Java", "C#", "Python", "Assembly", "JavaScript"}),
                new MCSACard("What is the wildcard mask for the network address 192.168.1.0/20? ", "2", new List<string>{"255.255.240.0", "0.0.15.255", "*20", "0.0.0.20", "255.255.255.20"}),
                new QuestionCard("What is the chemical symbol of mercury? ", "hg"),
                new QuestionCard("In web design, what is 'HTML' short for? ", "hyper text markup language"),
                new MCSACard("In C#, what is the data type for a true/false value? ", "5", new List<string>{"int", "string", "Object", "There is none", "bool"})
        });*/
        }
        public QuestionCard Draw()
        {
            int randomIndex = rnd.Next(cards.Count);
            QuestionCard card = cards[randomIndex];
            cards.Remove(card);

            return card;
        }
    }
}