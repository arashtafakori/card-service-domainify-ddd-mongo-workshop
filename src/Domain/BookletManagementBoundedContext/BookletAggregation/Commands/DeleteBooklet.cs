﻿using XSwift.Domain;
using MediatR;

namespace Module.Domain.BookletAggregation
{
    public class DeleteBooklet :
        RequestToDeleteById<Booklet, string>
    {
        public DeleteBooklet(string id)
            : base(id)
        { 
            ValidationState.Validate();
        }

        public override async Task<Booklet> ResolveAndGetEntityAsync(IMediator mediator)
        {
            var booklet = (await mediator.Send(
                new RetrieveBooklet(Id, evenArchivedData: true)))!;
            await base.ResolveAsync(mediator, booklet);
            return booklet;
        }
    }
}
