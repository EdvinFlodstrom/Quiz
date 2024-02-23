using MediatR;
using Web_App.Server.Services;

namespace Web_App.Server.Handlers.Quiz
{
    public class GetInitialInstructionsQuery : IRequest<GetInitialInstructionsQueryResponse>
    {
    }

    public class GetInitialInstructionsQueryHandler : IRequestHandler<GetInitialInstructionsQuery, GetInitialInstructionsQueryResponse>
    {
        private readonly QuizService quizService;
        public GetInitialInstructionsQueryHandler(QuizService quizService)
        {
            this.quizService = quizService;
        }
        public async Task<GetInitialInstructionsQueryResponse> Handle(GetInitialInstructionsQuery query, CancellationToken cancellationToken)
        {
            var response = new GetInitialInstructionsQueryResponse();

            try
            {
                response.Instructions = await quizService.GetInitialInstructions();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.Success = false;
            }

            return response;
        }
    }

    public class GetInitialInstructionsQueryResponse
    {
        public List<string>? Instructions { get; set; }
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
