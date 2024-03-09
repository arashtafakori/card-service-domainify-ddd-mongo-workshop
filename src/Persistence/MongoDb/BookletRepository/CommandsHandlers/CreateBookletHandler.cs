﻿using MediatR;
using Module.Domain.BookletAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;

namespace Module.Persistence.BookletRepository
{
    public class CreateBookletHandler :
        IRequestHandler<CreateBooklet, string>
    {
        private readonly IMediator _mediator;
        private readonly IMongoDatabase _database;
        public CreateBookletHandler(
            IMediator mediator, IMongoDatabase database)
        {
            _mediator = mediator;
            _database = database;
        }
        public async Task<string> Handle(
            CreateBooklet request,
            CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<BookletDocument>(ConnectionNames.Booklet);
            var preparedItem = await request.ResolveAndGetEntityAsync(_mediator);

            //await BookletAggregation.Setup(_mediator).CreateBooklet(preparedItem);

            var bookletDoc = BookletDocument.InstanceOf(preparedItem);
            await collection.InsertOneAsync(bookletDoc);
            preparedItem.SetId(bookletDoc.Id!);
            return preparedItem.Id;
        }
    }
} 
