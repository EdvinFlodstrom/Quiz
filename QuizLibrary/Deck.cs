namespace QuizLibrary
{    
    internal class Deck
    {
        private Random rnd = new Random();
        private List<QuestionCard> cards = new List<QuestionCard>();
        private List<QuestionCard> sortedCards = new List<QuestionCard>();
        public List<QuestionCard> Cards
        {
            get
            {
                return cards;
            }
        }
        public List<QuestionCard> SortedCards
        {
            get
            {
                return sortedCards;
            }
        }
        public Deck(IManager manager)
        {            
            cards = manager.Read();
            sortedCards = manager.Read();
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