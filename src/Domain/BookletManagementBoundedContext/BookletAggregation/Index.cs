using Domainify.Domain;
using System.ComponentModel.DataAnnotations;
using Domain.Properties;

namespace Module.Domain.BookletAggregation
{
    public class Index : Entity<Index, string>
    {
        [Required]
        public string BookletId { get; protected set; } = string.Empty;

        [MinLengthShouldBe(3)]
        [MaxLengthShouldBe(50)]
        [StringLength(50)]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; protected set; } = string.Empty;

        public override ConditionProperty<Index>? Uniqueness()
        {
            return new ConditionProperty<Index>()
            {
                Condition = x => x.Name == Name,
                Description = Resource.AnIndexWithThisNameOnThisBookletHasAlreadyExisted
            };
        }

        public static Index NewInstance(string bookletId)
        {
            return new Index().SetBookletId(bookletId);
        }

        public Index SetBookletId(string value)
        {
            BookletId = value;

            return this;
        }
        public Index SetName(string value)
        {
            Name = value;

            return this;
        }

        public IndexViewModel ToViewModel()
        {
            var viewModel = new IndexViewModel()
            {
                BookletId = BookletId!,
                ModifiedDate = ModifiedDate,
                IsDeleted = IsDeleted,
                Id = Id!,
                Name = Name,
            };

            return viewModel;
        }
    }
}
