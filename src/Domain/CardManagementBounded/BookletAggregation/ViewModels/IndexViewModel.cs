using Domainify.Domain;
using System.ComponentModel.DataAnnotations;

namespace Module.Domain.BookletAggregation
{
    public class IndexViewModel : ViewModel, IModifiedViewModel, IDeletableViewModel
    {
        public string Id { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }

        [Display(Name = "Modified Date")]
        [DataType(DataType.Date)]
        public DateTime? ModifiedDate { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public short Order { get; set; }
    }
}
