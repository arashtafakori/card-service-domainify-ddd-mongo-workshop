﻿using MediatR;
using Module.Domain.BookletAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;
using XSwift.Domain;
using XSwift.MongoDb.Datastore;

namespace Module.Persistence.BookletRepository
{
    public class CreateNewBookletHandler :
        IRequestHandler<CreateNewBooklet, string>
    {
        private readonly IMediator _mediator;
        private readonly IMongoDatabase _database;
        public CreateNewBookletHandler(
            IMediator mediator, IMongoDatabase database)
        {
            _mediator = mediator;
            _database = database;
        }
        public async Task<string> Handle(
            CreateNewBooklet request,
            CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<BookletDocument>(ConnectionNames.Booklet);

            var entity = await request.ResolveAndGetEntityAsync(_mediator);
            //---
            if (entity.Uniqueness() != null && entity.Uniqueness()!.Condition != null)
            {
                var uniquenessFilter = Builders<BookletDocument>
                    .Filter.Eq(d => d.Title, request.Title);

                await new LogicalState()
                    .AddAnPreventer(new PreventIfTheEntityWithTheseUniquenessConditionsHasAlreadyExisted
                    <Booklet, BookletDocument>(collection, uniquenessFilter)
                    .WithDescription(entity.Uniqueness()!.Description!))
                    .AssesstAsync();
            }
            //---
            var bookletDoc = BookletDocument.InstanceOf(entity);
            await collection.InsertOneAsync(bookletDoc);
            entity.SetId(bookletDoc.Id!);
            return entity.Id;
        }
    }
} 
