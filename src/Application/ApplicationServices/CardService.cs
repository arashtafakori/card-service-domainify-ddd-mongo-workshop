using MediatR;
using Contract;
using Domainify.Domain;
using Domain.CardAggregation;

namespace Application
{
    public class CardService : ICardService
    {
        private readonly IMediator _mediator;

        public CardService(
            IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<CardViewModel?> Process(AddCard request)
        {
            var id = await _mediator.Send(request);
            return (await _mediator.Send(new GetCard(id)))!.ToViewModel();
        }
        public async Task<CardViewModel?> Process(EditCard request)
        {
            await _mediator.Send(request);
            return (await _mediator.Send(new GetCard(request.Id)))!.ToViewModel();
        }
        public async Task Process(DeleteCard request)
        {
            await _mediator.Send(request);
        }
        public async Task Process(RestoreCard request)
        {
            await _mediator.Send(request);
        }
        public async Task Process(DeleteCardPermanently request)
        {
            await _mediator.Send(request);
        }
        public async Task Process(EmptyCardsTrash request)
        {
            await _mediator.Send(request);
        }
        public async Task<CardViewModel?> Process(GetCard request)
        {
            return (await _mediator.Send(request))!.ToViewModel();
        }
        public async Task<PaginatedList<CardViewModel>> Process(GetCardsList request)
        {
            return await _mediator.Send(request);
        }
    }
}
