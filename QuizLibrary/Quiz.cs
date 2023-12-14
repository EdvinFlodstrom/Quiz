namespace QuizLibrary
{
    internal class Quiz
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
        public List<QuestionCard> ListOfSortedQuestionCards()
        {
            return deck.SortedCards;
        }
    }
}
