using Domainify.Domain;
using Module.Domain.CardAggregation;

namespace Module.Contract
{
    public interface ICardService
    {
        public Task<CardViewModel?> Process(AddCard request);
        public Task<CardViewModel?> Process(EditCard request);
        public Task Process(DeleteCard request);
        public Task Process(RestoreCard request);
        public Task Process(DeleteCardPermanently request);
        public Task Process(EmptyCardsTrash request);
        public Task<CardViewModel?> Process(GetCard request);
        public Task<PaginatedList<CardViewModel>> Process(GetCardsList request);
    }
}
