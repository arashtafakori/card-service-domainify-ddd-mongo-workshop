﻿using Domainify.Domain;
using MediatR;

namespace Domain.BookletAggregation
{
    public class DeleteIndexPermanently :
        RequestToDeletePermanentlyById<Index, string>
    {
        public DeleteIndexPermanently(string id)
            : base(id)
        { 
            ValidationState.Validate();
        }

        public override async Task<Index> ResolveAndGetEntityAsync(IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);

            var index = (await mediator.Send(
                new FindIndex(Id, includeDeleted: true)))!;
            await base.ResolveAsync(mediator, index);
            return index;
        }
    }
}
