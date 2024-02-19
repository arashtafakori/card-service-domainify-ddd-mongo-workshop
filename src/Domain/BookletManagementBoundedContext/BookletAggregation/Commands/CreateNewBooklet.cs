using XSwift.Domain;
using MediatR;

namespace Module.Domain.BookletAggregation
{
    public class CreateNewBooklet
        : RequestToCreate<Booklet, string>
    {
        [BindTo(typeof(Booklet), nameof(Booklet.Title))]
        public string Title { get; private set; }

        public CreateNewBooklet(string title) 
        {
            Title = title.Trim();
            ValidationState.Validate();
        }

        public override async Task<Booklet> ResolveAndGetEntityAsync(
            IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);

            var project = Booklet.New().SetTitle(Title);
            await base.ResolveAsync(mediator, project);
            return project;
        }
    }
}
