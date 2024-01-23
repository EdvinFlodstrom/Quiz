using MediatR;
using Web_App.Server.Models;

namespace Web_App.Server.Handlers.Questions
{
    public class GetQuestionWithoutAnswerByIdCommand : IRequest<QuestionModel>
    {
        public int QuestionId { get; set; }
    }
}
