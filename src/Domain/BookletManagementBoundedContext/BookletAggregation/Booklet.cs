using Domainify.Domain;
using System.ComponentModel.DataAnnotations;
using Domain.Properties;
using MediatR;

namespace Module.Domain.BookletAggregation
{
    public class Booklet : Entity<Booklet, string>, IAggregateRoot
    {
        [MinLengthShouldBe(3)]
        [MaxLengthShouldBe(50)]
        [StringLength(50)]
        [Required(AllowEmptyStrings = false)]
        public string Title { get; protected set; } = string.Empty;

        [MinLengthShouldBe(3)]
        [MaxLengthShouldBe(10)]
        [StringLength(10)]
        [Required(AllowEmptyStrings = false)]
        public string Type { get; protected set; } = string.Empty;

        public List<Index> Indices { get; set; } = new List<Index>();

        public override ConditionProperty<Booklet>? Uniqueness()
        {
            return new ConditionProperty<Booklet>()
            {
                Condition = x => x.Title == Title && x.Type == Type,
                Description = Resource.ABookletWithThisTitleAndVersionHasAlreadyExisted
            };
        }

        public static Booklet NewInstance(string type)
        {
            return new Booklet().SetType(type);
        }

        public Booklet SetType(string value)
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

        //private static IMediator? _mediator;

        //public Booklet(IMediator mediator) { _mediator = mediator; }

        //public Booklet Setup(IMediator mediator)
        //{
        //    return new Booklet(mediator);
        //}
        //public void RemoveIndex(Index index)
        //{
        //    Indices.Remove(index);
        //}

        //public void UpdateIndex(Index index)
        //{
        //    var inx = Indices.FindIndex(item => item.Id == index.Id);

        //    if (inx != -1)
        //        Indices[inx] = index;
        //}
    }
}
