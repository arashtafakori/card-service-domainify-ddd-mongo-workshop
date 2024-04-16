using Domainify.Domain;
using MediatR;

namespace Domain.BookletAggregation
{
    public class EditBookletTitle :
        RequestToUpdateById<Booklet, string>
    {
        [BindTo(typeof(Booklet), nameof(Booklet.Title))]
        public string Title { get; private set; }
 
        public EditBookletTitle(string id, string title) 
            : base(id)
        {
            Title = title.Trim();
            ValidationState.Validate();
        }

        public override async Task<Booklet> ResolveAndGetEntityAsync(
            IMediator mediator)
        {
            var booklet = (await mediator.Send(new FindBooklet(Id)))!;
            booklet.SetTitle(Title);

            InvariantState.AddAnInvariantRequest(new PreventIfTheSameBookletHasAlreadyExisted(booklet));
            await InvariantState.AssestAsync(mediator);
 
            await base.ResolveAsync(mediator, booklet);
            return booklet;
        }
    }
}
