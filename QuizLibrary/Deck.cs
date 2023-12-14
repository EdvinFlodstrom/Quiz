namespace QuizLibrary
{    
    internal class Deck
    {
        private Random rnd = new Random();
        private List<QuestionCard> cards = new List<QuestionCard>();
        private List<QuestionCard> sortedCards = new List<QuestionCard>();
        private FileManager fm = new FileManager();
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
        public Deck()
        {
            cards = fm.ReadFile();
            sortedCards = fm.ReadFile();
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