using Domainify.Domain;
using MediatR;

namespace Module.Domain.BookletAggregation
{
    public class GetIndex :
        QueryItemRequestById<Index, string, Index?>
    {
        [BindTo(typeof(Index), nameof(Index.BookletId))]
        public string BookletId { get; private set; } = string.Empty;

        public GetIndex(string bookletId, string id, bool evenDeletedData = false) : base(id)
        {
            BookletId = bookletId.Trim();
            //PreventIfNoEntityWasFound = true;
            EvenDeletedData = evenDeletedData;
        }
        public override async Task ResolveAsync(IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);
        }
    }
}
