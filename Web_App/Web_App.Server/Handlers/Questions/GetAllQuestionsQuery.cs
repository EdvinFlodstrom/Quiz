using MediatR;
using Web_App.Server.Models;

namespace Web_App.Server.Handlers.Questions
{
    public class GetAllQuestionsQuery : IRequest<List<QuestionModel>>
    {
    }
}
