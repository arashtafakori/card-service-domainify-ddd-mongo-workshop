using XSwift.Domain;
using MediatR;

namespace Module.Domain.BookletAggregation
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
            await InvariantState.AssestAsync(mediator);

            var booklet = (await mediator.Send(new RetrieveBooklet(Id)))!;
            booklet.SetTitle(Title);
            await base.ResolveAsync(mediator, booklet);
            return booklet;
        }
    }
}
