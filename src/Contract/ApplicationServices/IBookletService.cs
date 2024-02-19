﻿using XSwift.Domain;
using Module.Domain.BookletAggregation;

namespace Module.Contract
{
    public interface IBookletService
    {
        public Task<string> Process(CreateNewBooklet request);
        public Task Process(ChangeBookletTitle request);
        public Task Process(ArchiveBooklet request);
        public Task Process(CheckBookletForArchiving request);
        public Task Process(RestoreBooklet request);
        public Task Process(DeleteBooklet request);
        public Task<BookletViewModel?> Process(GetBooklet request);
        public Task<PaginatedViewModel<BookletViewModel>> Process(GetBookletList request);
    }
}