using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;
using Web_App.Server.Models;
using Web_App.Server.Services;

namespace Web_App.Server.Handlers.Questions
{
    public class GetAllQuestionsQuery : IRequest<GetAllQuestionsQueryResponse>
    {
        
    }

    public class GetAllQuestionsQueryHandler : IRequestHandler<GetAllQuestionsQuery, GetAllQuestionsQueryResponse>
    {
        private readonly QuizService quizService;
        public GetAllQuestionsQueryHandler(QuizService quizService)
        {
            this.quizService = quizService;
        }
        public async Task<GetAllQuestionsQueryResponse> Handle(GetAllQuestionsQuery query, CancellationToken cancellationToken)
        {
            var response = new GetAllQuestionsQueryResponse();

            try
            {
                response.Questions = await quizService.GetAllQuestions();

                if (response.Questions == null)
                {
                    response.Success = false;                    
                }
                else
                {
                    response.Success = true;
                }
            }
            catch (Exception Ex)
            {
                response.ErrorMessage = Ex.Message;
                response.Success = false;
            }

            return response;
        }
    }
    
    public class GetAllQuestionsQueryResponse
    {
        public List<QuestionModel>? Questions { get; set; }
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
