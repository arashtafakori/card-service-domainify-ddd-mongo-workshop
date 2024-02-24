using XSwift.Base;
using XSwift.Domain;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace XSwift.MongoDb.Datastore
{
    /// <summary>
    /// Represents a logical preventer that checks if an entity with specified uniqueness conditions already exists.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity derived from BaseEntity.</typeparam>
    public class PreventIfTheEntityWithTheseUniquenessConditionsHasAlreadyExisted<TEntity, TDocument>
        : LogicalPreventer
        where TEntity : BaseEntity<TEntity>
    {
        private IMongoCollection<TDocument> _document;
        private FilterDefinition<TDocument> _condition;

        /// <summary>
        /// Initializes a new instance of the PreventIfTheEntityWithTheseUniquenessConditionsHasAlreadyBeenExisted class.
        /// </summary>
        /// <param name="context">The DbContext associated with the entity.</param>
        /// <param name="condition">The condition specifying the uniqueness conditions for the entity.</param>
        public PreventIfTheEntityWithTheseUniquenessConditionsHasAlreadyExisted(
            IMongoCollection<TDocument> document, FilterDefinition<TDocument> condition)
        {
            _document = document;
            _condition = condition;
        }

        /// <summary>
        /// Resolves the logical condition by checking if an entity with the specified uniqueness conditions already exists.
        /// </summary>
        /// <returns>A Task representing the asynchronous operation with a result indicating whether the entity already exists.</returns>
        public override async Task<bool> ResolveAsync()
        {
            return await _document.Find(_condition).AnyAsync();
        }

        /// <summary>
        /// Gets the issue related to the logical preventer, indicating that an entity with the specified uniqueness conditions already exists.
        /// </summary>
        /// <returns>The issue is representing the condition of an entity already existing with the specified uniqueness conditions.</returns>
        public override IIssue? GetIssue()
        {
            return new AnEntityWithTheseUniquenessConditionsHasAlreadyExisted(
                    typeof(TEntity).Name, Description);
        }
    }
}
