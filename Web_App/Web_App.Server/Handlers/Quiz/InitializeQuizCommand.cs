using MediatR;

namespace Web_App.Server.Handlers.Quiz
{
    public class InitializeQuizCommand : IRequest<bool>
    {
        public required string PlayerName { get; set; }
        public required int NumberOfQuestions { get; set; }
    }
}
