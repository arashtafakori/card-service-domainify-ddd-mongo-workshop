using MediatR;
using Module.Contract;
using XSwift.Domain;
using Module.Domain.BookletAggregation;

namespace Module.Application
{
    public class BookletService : IBookletService
    {
        private readonly IMediator _mediator;

        public BookletService(
            IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<string> Process(CreateNewBooklet request)
        {
            var id = await _mediator.Send(request);
            return id;
        }
        public async Task Process(ChangeBookletTitle request)
        {
            await _mediator.Send(request);
        }
        public async Task Process(ArchiveBooklet request)
        {
            await _mediator.Send(request);
        }
        public async Task Process(CheckBookletForArchiving request)
        {
            await _mediator.Send(request);
        }
        public async Task Process(RestoreBooklet request)
        {
            await _mediator.Send(request);
        }
        public async Task Process(DeleteBooklet request)
        {
            await _mediator.Send(request);
        }
        public async Task<BookletViewModel?> Process(GetBooklet request)
        {
            return await _mediator.Send(request);
        }
        public async Task<PaginatedViewModel<BookletViewModel>> Process(GetBookletList request)
        {
            return await _mediator.Send(request);
        }
    }
}
