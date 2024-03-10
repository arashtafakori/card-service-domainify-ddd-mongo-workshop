using Domainify.Domain;
using System.ComponentModel.DataAnnotations;
using Domain.Properties;

namespace Module.Domain.BookletAggregation
{
    public class Booklet : Entity<Booklet, string>, IAggregateRoot
    {
        [MinLengthShouldBe(3)]
        [MaxLengthShouldBe(50)]
        [StringLength(50)]
        [Required(AllowEmptyStrings = false)]
        public string Title { get; protected set; } = string.Empty;

        [Required]
        public short Type { get; protected set; }

        public List<Index> Indices { get; set; } = new List<Index>();

        public override ConditionProperty<Booklet>? Uniqueness()
        {
            return new ConditionProperty<Booklet>()
            {
                Condition = x => x.Title == Title && x.Type == Type,
                Description = Resource.ABookletWithThisTitleAndVersionHasAlreadyExisted
            };
        }

        public static Booklet NewInstance(short type)
        {
            return new Booklet().SetType(type);
        }

        public Booklet SetType(short value)
        {
            Type = value;

            return this;
        }

        public Booklet SetTitle(string value)
        {
            Title = value;

            return this;
        }

        public BookletViewModel ToViewModel()
        {
            var viewModel = new BookletViewModel()
            {
                Type = Type,
                ModifiedDate = ModifiedDate,
                IsDeleted = IsDeleted,
                Indices = Indices.Select(i => i.ToViewModel()).ToList(),
                Id = Id!,
                Title = Title,
            };

            return viewModel;
        }
    }
}
