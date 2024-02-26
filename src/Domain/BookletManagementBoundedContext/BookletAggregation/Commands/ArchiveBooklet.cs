﻿using XSwift.Domain;
using MediatR;

namespace Module.Domain.BookletAggregation
{
    public class ArchiveBooklet :
        RequestToArchiveById<Booklet, string>
    {
        public ArchiveBooklet(string id) 
            : base(id)
        {
            ValidationState.Validate();
        }
        public override async Task<Booklet> ResolveAndGetEntityAsync(
            IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);

            var booklet = (await mediator.Send(
                new RetrieveBooklet(Id, evenArchivedData: true)))!;
            await base.ResolveAsync(mediator, booklet);
            return booklet;
        }
    }
}