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
        private FileManager fm = new FileManager();
        public List<QuestionCard> Cards
        {
            get
            {
                return cards;
            }
        }
        public Deck()
        {
            foreach(List<string> line in fm.ReadFile())
            {
                if (line[0] == "QuestionCard")
                {
                    cards.Add(new QuestionCard(line[1], line[2]));
                }
                else if (line[0] == "MCSACard")
                {
                    cards.Add(new MCSACard(line[1], line[2], line[3].Split(',').ToList()));
                }
            }
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