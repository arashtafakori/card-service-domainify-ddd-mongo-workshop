﻿using Domainify.Domain;
using MediatR;

namespace Domain.BookletAggregation
{
    public class DeleteBooklet :
        RequestToDeleteById<Booklet, string>
    {
        public DeleteBooklet(string id) 
            : base(id)
        {
            ValidationState.Validate();
        }
        public override async Task<Booklet> ResolveAndGetEntityAsync(
            IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);

            var booklet = (await mediator.Send(
                new FindBooklet(Id, includeDeleted: true)))!;
            await base.ResolveAsync(mediator, booklet);
            return booklet;
        }
    }
}