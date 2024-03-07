﻿using Domainify.Domain;
using System.ComponentModel.DataAnnotations;

namespace Module.Domain.BookletAggregation
{
    public class BookletViewModel : ViewModel, IModifiedViewModel, IDeletableViewModel
    {
        public required string Id { get; set; }
        public required string Type { get; set; }
        public required string Title { get; set; }

        public bool IsDeleted { get; set; }
        public List<IndexViewModel> Indices { get; set; } = new List<IndexViewModel>();

        [Display(Name = "Modified Date")]
        [DataType(DataType.Date)]
        public DateTime? ModifiedDate { get; set; }
    }
}
