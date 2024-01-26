using MediatR;
using Web_App.Server.Models;
using Web_App.Server.Services;

namespace Web_App.Server.Handlers.Questions
{
    public class GetQuestionWithOrWithoutAnswerByIdCommand : IRequest<GetQuestionWithOrWithoutAnswerByIdCommandReponse>
    {
        public int QuestionId { get; set; }
        public bool IncludeAnswer { get; set; }
    }

    public class GetQuestionWithOrWithoutAnswerByIdCommandHandler : IRequestHandler<GetQuestionWithOrWithoutAnswerByIdCommand, GetQuestionWithOrWithoutAnswerByIdCommandReponse>
    {
        private readonly QuizService quizService;
        public GetQuestionWithOrWithoutAnswerByIdCommandHandler(QuizService quizService)
        {
            this.quizService = quizService;
        }
        public async Task<GetQuestionWithOrWithoutAnswerByIdCommandReponse> Handle(GetQuestionWithOrWithoutAnswerByIdCommand request, CancellationToken cancellationToken)
        {
            var response = new GetQuestionWithOrWithoutAnswerByIdCommandReponse();

            try
            {
                response.Question = await quizService.GetQuestionWithOrWithoutAnswerById(request.QuestionId, request.IncludeAnswer);
                response.Success = true;
            }
            catch (Exception Ex)
            {
                response.ErrorMessage = Ex.Message;
                response.Success = false;
            }

            return response;
        }
    }

    public class GetQuestionWithOrWithoutAnswerByIdCommandReponse
    {
        public QuestionModel Question { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}
