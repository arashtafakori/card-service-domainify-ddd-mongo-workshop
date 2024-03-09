using MediatR;
using Module.Contract;
using Domainify.Domain;
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
        public async Task<BookletViewModel?> Process(CreateBooklet request)
        {
            var id = await _mediator.Send(request);
            return (await _mediator.Send(new GetBooklet(id)))!.ToViewModel();
        }
        public async Task<BookletViewModel?> Process(EditBookletTitle request)
        {
            await _mediator.Send(request);
            return (await _mediator.Send(new GetBooklet(request.Id)))!.ToViewModel();
        }
        public async Task Process(DeleteBooklet request)
        {
            await _mediator.Send(request);
        }
        public async Task Process(RestoreBooklet request)
        {
            await _mediator.Send(request);
        }
        public async Task Process(DeleteBookletPermanently request)
        {
            await _mediator.Send(request);
        }
        public async Task Process(EmptyBookletsTrash request)
        {
            await _mediator.Send(request);
        }
        public async Task<BookletViewModel?> Process(GetBooklet request)
        {
            return (await _mediator.Send(request))!.ToViewModel();
        }
        public async Task<PaginatedViewModel<BookletViewModel>> Process(GetBookletsList request)
        {
            return await _mediator.Send(request);
        }
        public async Task<IndexViewModel?> Process(AddIndex request)
        {
            (string bookletId, string id)? result = await _mediator.Send(request);
    
            if(result != null)
            {
                return (await _mediator.Send(
                    new GetIndex(id: result!.Value.id)))!
                        .ToViewModel();
            }
            return null;
        }
        public async Task<IndexViewModel?> Process(GetIndex request)
        {
            return (await _mediator.Send(request))!.ToViewModel();
        }
        public async Task<List<IndexViewModel>> Process(GetIndicesList request)
        {
            return await _mediator.Send(request);
        }
        public async Task<IndexViewModel?> Process(EditIndexName request)
        {
            await _mediator.Send(request);
            return (await _mediator.Send(new GetIndex(id: request.Id)))!.ToViewModel();
        }
        public async Task Process(DeleteIndex request)
        {
            await _mediator.Send(request);
        }
        public async Task Process(RestoreIndex request)
        {
            await _mediator.Send(request);
        }
        public async Task Process(DeleteIndexPermanently request)
        {
            await _mediator.Send(request);
        }
        public async Task Process(EmptyIndicesTrash request)
        {
            await _mediator.Send(request);
        }
    }
}
