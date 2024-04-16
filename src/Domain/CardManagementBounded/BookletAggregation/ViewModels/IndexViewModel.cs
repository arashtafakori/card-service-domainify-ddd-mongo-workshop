using Domainify.Domain;
using System.ComponentModel.DataAnnotations;

namespace Domain.BookletAggregation
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
        public double Order { get; set; }
    }
}
