using Domainify.Domain;
using MediatR;

namespace Domain.BookletAggregation
{
    public class AddIndex
        : RequestToCreate<Index, (string bookletId, string id)?>
    {
        [BindTo(typeof(Booklet), nameof(Booklet.Id))]
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
            var maxOrder = await mediator.Send(new MaxOrderValueOfIndexInBooklet(BookletId));
 
            var index = Index.NewInstance()
                .SetName(Name).SetOrder(maxOrder + 1);
   
            InvariantState.AddAnInvariantRequest(
                new PreventIfTheSameIndexHasAlreadyExisted(index, bookletId: BookletId));
            await InvariantState.AssestAsync(mediator);

            await base.ResolveAsync(mediator, index);
            return index;
        }
    }
}
