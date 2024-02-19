using XSwift.Domain;
using MediatR;

namespace Module.Domain.BookletAggregation
{
    public class ChangeBookletTitle :
        RequestToUpdateById<Booklet, string>
    {
        [BindTo(typeof(Booklet), nameof(Booklet.Title))]
        public string Title { get; private set; }
 
        public ChangeBookletTitle(string id, string title) 
            : base(id)
        {
            Title = title.Trim();
            ValidationState.Validate();
        }

        public override async Task<Booklet> ResolveAndGetEntityAsync(
            IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);

            var memle = (await mediator.Send(new RetrieveBooklet(Id)))!;
            memle.SetTitle(Title);
            await base.ResolveAsync(mediator, memle);
            return memle;
        }
    }
}
