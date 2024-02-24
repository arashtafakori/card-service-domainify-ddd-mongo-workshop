﻿using XSwift.Domain;
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
        public static Booklet Instantiate() { return new Booklet(); }
        public static Booklet Instantiate(DateTime modifiedDate, bool isArchived) {
            return new Booklet()
            {
                ModifiedDate = modifiedDate,
                IsArchived = isArchived
            };
        }

        public override ConditionProperty<Booklet>? Uniqueness()
        {
            return new ConditionProperty<Booklet>()
            {
                Condition = x => x.Title == Title,
                Description = Resource.ABookletWithThisTitleAndVersionHasAlreadyExisted
            };
        }

        public Booklet SetTitle(string value)
        {
            Title = value;

            return this;
        }
    }
}
