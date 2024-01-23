using MediatR;
using Web_App.Server.Models;
using Web_App.Server.Services;

namespace Web_App.Server.Handlers.Questions
{
    public class GetAllQuestionsQueryHandler : IRequestHandler<GetAllQuestionsQuery, List<QuestionModel>>
    {
        private readonly QuizService quizService;
        public GetAllQuestionsQueryHandler(QuizService quizService)
        {
            this.quizService = quizService;
        }
        public async Task<List<QuestionModel>> Handle(GetAllQuestionsQuery query, CancellationToken cancellationToken)
        {
            try
            {
                return await quizService.GetAllQuestions();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
