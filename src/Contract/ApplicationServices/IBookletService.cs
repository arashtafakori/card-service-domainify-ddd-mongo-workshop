using Domainify.Domain;
using Module.Domain.BookletAggregation;

namespace Module.Contract
{
    public interface IBookletService
    {
        public Task<BookletViewModel?> Process(CreateBooklet request);
        public Task<BookletViewModel?> Process(EditBookletTitle request);
        public Task Process(DeleteBooklet request);
        public Task Process(RestoreBooklet request);
        public Task Process(DeleteBookletPermanently request);
        public Task Process(EmptyBookletsTrash request);
        public Task<BookletViewModel?> Process(GetBooklet request);
        public Task<PaginatedList<BookletViewModel>> Process(GetBookletsList request);
 
        public Task<IndexViewModel?> Process(AddIndex request);
        public Task<IndexViewModel?> Process(EditIndexName request);
        public Task<IndexViewModel?> Process(GetIndex request);
        public Task<List<IndexViewModel>> Process(GetIndicesList request);
        public Task Process(DeleteIndex request);
        public Task Process(RestoreIndex request);
        public Task Process(DeleteIndexPermanently request);
        public Task Process(EmptyIndicesTrash request);
    }
}
