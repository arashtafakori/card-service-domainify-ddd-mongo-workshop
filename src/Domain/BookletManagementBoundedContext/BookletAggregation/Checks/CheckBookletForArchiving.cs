using XSwift.Domain;
using MediatR;

namespace Module.Domain.BookletAggregation
{
    public class CheckBookletForArchiving :
        AnyRequestById<Booklet, string>
    {
        public CheckBookletForArchiving(string id)
            : base(id)
        {
        }
        public override async Task ResolveAsync(IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);
        }
    }
}
