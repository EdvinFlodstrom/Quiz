using MediatR;
using Web_App.Server.Services;

namespace Web_App.Server.Handlers.Quiz
{
    public class GetInitialInstructionsQueryHandler : IRequestHandler<GetInitialInstructionsQuery, List<string>>
    {
        private readonly QuizService quizService;
        public GetInitialInstructionsQueryHandler(QuizService quizService)
        {
            this.quizService = quizService;
        }
        public async Task<List<string>> Handle(GetInitialInstructionsQuery query, CancellationToken cancellationToken)
        {
            try
            {
                return await quizService.GetInitialInstructions();
            }
            catch 
            {
                return null;
            }
        }
    }
}
