using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace FrågesportNetCore
{
    public class Quiz
    {
        private Deck deck;
        public Quiz() { }
        public List<QuestionCard> Run()
        {
            deck = new Deck();

            int deckLength = deck.Cards.Count;

            List<QuestionCard> questionCards = new List<QuestionCard>();

            for (int i = 0; i < deckLength; i++)
            {
                questionCards.Add(deck.Draw());
            }
            return questionCards;
        }        
    }
}
