using MediatR;
using Web_App.Server.Models;
using Web_App.Server.Services;

namespace Web_App.Server.Handlers.Questions
{
    public class GetQuestionWithoutAnswerByIdCommandHandler : IRequestHandler<GetQuestionWithoutAnswerByIdCommand, QuestionModel>
    {
        private readonly QuizService quizService;
        public GetQuestionWithoutAnswerByIdCommandHandler(QuizService quizService)
        {
            this.quizService = quizService;
        }
        public async Task<QuestionModel> Handle(GetQuestionWithoutAnswerByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await quizService.GetQuestionWithoutAnswerById(request.QuestionId);
            }
            catch 
            {
                return null;
            }
        }
    }
}
