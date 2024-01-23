using MediatR;

namespace Web_App.Server.Handlers.Quiz
{
    public class GetInitialInstructionsQuery : IRequest<List<string>>
    {
    }
}
