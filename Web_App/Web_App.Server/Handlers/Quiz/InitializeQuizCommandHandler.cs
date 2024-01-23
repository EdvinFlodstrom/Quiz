using MediatR;
using Web_App.Server.Services;

namespace Web_App.Server.Handlers.Quiz
{
    public class InitializeQuizCommandHandler : IRequestHandler<InitializeQuizCommand, bool>
    {
        private readonly QuizService quizService;
        public InitializeQuizCommandHandler(QuizService quizService)
        {
            this.quizService = quizService;
        }

        public async Task<bool> Handle(InitializeQuizCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await quizService.InitializeQuiz(request.PlayerName, request.NumberOfQuestions);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
