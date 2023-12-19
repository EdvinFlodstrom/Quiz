namespace QuizLibrary
{
    internal class Quiz
    {
        IManager manager;
        private Deck deck;
        public Quiz(IManager manager)
        {
            this.manager = manager;
        }
        public List<QuestionCard> Run()
        {
            deck = new Deck(manager);

            int deckLength = deck.Cards.Count;

            List<QuestionCard> questionCards = new List<QuestionCard>();

            for (int i = 0; i < deckLength; i++)
            {
                questionCards.Add(deck.Draw());
            }
            return questionCards;
        }
        public List<QuestionCard> ListOfSortedQuestionCards()
        {
            return deck.SortedCards;
        }
    }
}
