using Domainify.Domain;
using System.ComponentModel.DataAnnotations;

namespace Module.Domain.BookletAggregation
{
    public class IndexViewModel : ViewModel, IModifiedViewModel, IDeletableViewModel
    {
        public required string Id { get; set; }
        public required string BookletId { get; set; }
        public required string Name { get; set; }

        public bool IsDeleted { get; set; }

        [Display(Name = "Modified Date")]
        [DataType(DataType.Date)]
        public DateTime? ModifiedDate { get; set; }
    }
}
