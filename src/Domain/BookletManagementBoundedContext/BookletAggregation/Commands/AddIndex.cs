using Domainify.Domain;
using MediatR;

namespace Module.Domain.BookletAggregation
{
    public class AddIndex
        : RequestToCreate<Index, (string bookletId, string id)?>
    {
        [BindTo(typeof(Index), nameof(Index.BookletId))]
        public string BookletId { get; private set; } = string.Empty;

        [BindTo(typeof(Index), nameof(Index.Name))]
        public string Name { get; private set; }

        public AddIndex(string bookletId, string name)
        {
            BookletId = bookletId.Trim();
            Name = name.Trim();
            ValidationState.Validate();
        }

        public override async Task<Index> ResolveAndGetEntityAsync(
            IMediator mediator)
        {
            var index = Index.NewInstance(bookletId: BookletId)
                .SetName(Name);

            InvariantState.AddAnInvariantRequest(new PreventIfTheSameIndexHasAlreadyExisted(index));
            await InvariantState.AssestAsync(mediator);

            await base.ResolveAsync(mediator, index);
            return index;
        }
    }
}
