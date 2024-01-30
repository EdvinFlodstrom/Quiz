using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Web_App.Server.Models;
using Web_App.Server.Services;

namespace Web_App.Server.Handlers.Questions
{
    public class GetQuestionCommand : IRequest<GetQuestionCommandResponse>
    {
        public string PlayerName { get; set; }
    }

    public class GetQuestionCommandHandler : IRequestHandler<GetQuestionCommand, GetQuestionCommandResponse>
    {
        private readonly QuizService quizService;

        public GetQuestionCommandHandler(QuizService quizService)
        {
            this.quizService = quizService;
        }

        public async Task<GetQuestionCommandResponse> Handle(GetQuestionCommand request, CancellationToken cancellationToken)
        {
            var response = new GetQuestionCommandResponse();

            try
            {
                (bool successfullyGotQuestion, response.AnswerMessage, response.Question) = await quizService.GetQuestion(request.PlayerName);
                response.Success = successfullyGotQuestion;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.Success = false;
            }

            return response;
        }
    }

    public class GetQuestionCommandResponse
    {
        public QuestionModel Question { get; set; }
        public string AnswerMessage { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}
