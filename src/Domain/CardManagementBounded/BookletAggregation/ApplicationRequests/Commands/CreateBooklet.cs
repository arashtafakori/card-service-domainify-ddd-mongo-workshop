using Domainify.Domain;
using MediatR;

namespace Module.Domain.BookletAggregation
{
    public class CreateBooklet
        : RequestToCreate<Booklet, string>
    {
        [BindTo(typeof(Booklet), nameof(Booklet.Title))]
        public string Title { get; private set; }

        public CreateBooklet(string title) 
        {
            Title = title.Trim();
            ValidationState.Validate();
        }

        public override async Task<Booklet> ResolveAndGetEntityAsync(
            IMediator mediator)
        {
            var booklet = Booklet.NewInstance(type: 1)
                .SetTitle(Title);

            InvariantState.AddAnInvariantRequest(new PreventIfTheSameBookletHasAlreadyExisted(booklet));
            await InvariantState.AssestAsync(mediator);
 
            await base.ResolveAsync(mediator, booklet);
            return booklet;
        }
    }
}
