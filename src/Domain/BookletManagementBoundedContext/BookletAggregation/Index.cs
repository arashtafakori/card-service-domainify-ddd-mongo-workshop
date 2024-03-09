using Domainify.Domain;
using System.ComponentModel.DataAnnotations;
using Domain.Properties;

namespace Module.Domain.BookletAggregation
{
    public class Index : Entity<Index, string>
    {
        [MinLengthShouldBe(3)]
        [MaxLengthShouldBe(50)]
        [StringLength(50)]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; protected set; } = string.Empty;

        [Required]
        public int Order { get; protected set; }

        public override ConditionProperty<Index>? Uniqueness()
        {
            return new ConditionProperty<Index>()
            {
                Condition = x => x.Name == Name,
                Description = Resource.AnIndexWithThisNameOnThisBookletHasAlreadyExisted
            };
        }

        public static Index NewInstance()
        {
            return new Index();
        }
        public Index SetName(string value)
        {
            Name = value;

            return this;
        }
        public Index SetOrder(int value)
        {
            Order = value;

            return this;
        }
        public IndexViewModel ToViewModel()
        {
            var viewModel = new IndexViewModel()
            {
                ModifiedDate = ModifiedDate,
                IsDeleted = IsDeleted,
                Id = Id!,
                Name = Name,
            };

            return viewModel;
        }
    }
}
