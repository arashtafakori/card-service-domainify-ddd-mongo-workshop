﻿using Domainify.Domain;
using MediatR;

namespace Domain.BookletAggregation
{
    public class RestoreBooklet :
        RequestToRestoreById<Booklet, string>
    {
        public RestoreBooklet(string id) 
            : base(id)
        {
            ValidationState.Validate();
        }
        public override async Task<Booklet> ResolveAndGetEntityAsync(
            IMediator mediator)
        {
            var booklet = (await mediator.Send(
                new FindBooklet(Id, includeDeleted: true)))!;

            InvariantState.AddAnInvariantRequest(new PreventIfTheSameBookletHasAlreadyExisted(booklet));
            await InvariantState.AssestAsync(mediator);
 
            await base.ResolveAsync(mediator, booklet);
            return booklet;
        }
    }
}
