using Domainify.Domain;
using System.ComponentModel.DataAnnotations;
using Domain.Properties;
using Module.Domain.CardAggregation;

namespace Module.Domain.BookletAggregation
{
    public class Booklet : Entity<Booklet, string>, IAggregateRoot
    {
        public double Version { get; set; }

        [MinLengthShouldBe(3)]
        [MaxLengthShouldBe(50)]
        [StringLength(50)]
        [Required(AllowEmptyStrings = false)]
        public string Title { get; protected set; } = string.Empty;

        [Required]
        public short Type { get; protected set; }

        public List<Index> Indices { get; set; } = new List<Index>();

        public Booklet SetType(short value)
        {
            Type = value;

            return this;
        }
        public override ConditionProperty<Booklet>? Uniqueness()
        {
            return new ConditionProperty<Booklet>()
            {
                Condition = x => x.Title == Title && x.Type == Type,
                Description = Resource.ABookletWithThisTitleAndVersionHasAlreadyExisted
            };
        }

        public Booklet ()
        {
            Type = 1;
            Version = 1.0;
        }

        public static Booklet NewInstance()
        {
            return new Booklet();
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
