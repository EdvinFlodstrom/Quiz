using MediatR;
using Microsoft.IdentityModel.Tokens;
using Web_App.Server.Services;

namespace Web_App.Server.Handlers.Quiz
{
    public class InitializeQuizCommand : IRequest<InitializeQuizCommandResponse>
    {
        public required string PlayerName { get; set; }
        public required int NumberOfQuestions { get; set; }
    }

    public class InitializeQuizCommandHandler : IRequestHandler<InitializeQuizCommand, InitializeQuizCommandResponse>
    {
        private readonly QuizService quizService;
        public InitializeQuizCommandHandler(QuizService quizService)
        {
            this.quizService = quizService;
        }

        public async Task<InitializeQuizCommandResponse> Handle(InitializeQuizCommand request, CancellationToken cancellationToken)
        {
            var response = new InitializeQuizCommandResponse();

            try
            {
                response.QuizInitializedSuccessfully = await quizService.InitializeQuiz(request.PlayerName, request.NumberOfQuestions);
                response.Success = response.QuizInitializedSuccessfully;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.Success = false;
            }

            return response;
        }
    }

    public class InitializeQuizCommandResponse
    {
        public bool QuizInitializedSuccessfully { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}
