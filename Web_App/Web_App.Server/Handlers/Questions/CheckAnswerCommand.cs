using MediatR;
using Web_App.Server.Services;

namespace Web_App.Server.Handlers.Questions
{
    public class CheckAnswerCommand : IRequest<CheckAnswerCommandReponse>
    {
        public required string PlayerName { get; set; }
        public required string PlayerAnswer { get; set; }
    }

    public class CheckAnswerCommandHandler : IRequestHandler<CheckAnswerCommand, CheckAnswerCommandReponse>
    {
        private readonly QuizService quizService;
        public CheckAnswerCommandHandler(QuizService quizService)
        {
            this.quizService = quizService;
        }
        public async Task<CheckAnswerCommandReponse> Handle(CheckAnswerCommand request, CancellationToken cancellationToken)
        {
            var response = new CheckAnswerCommandReponse();

            try
            {
                (bool checkedSuccessfully, response.AnswerMessage) = await quizService.CheckAnswer(request.PlayerName, request.PlayerAnswer);
                response.Success = checkedSuccessfully;
            }
            catch (Exception Ex)
            {
                response.ErrorMessage = Ex.Message;
                response.Success = false;
            }

            return response;
        }
    }

    public class CheckAnswerCommandReponse
    {
        public string? AnswerMessage { get; set; }
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
