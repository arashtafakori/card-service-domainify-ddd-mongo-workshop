using XSwift.Domain;
using System.ComponentModel.DataAnnotations;

namespace Module.Domain.BookletAggregation
{
    public class BookletViewModel : ViewModel, IModifiedViewModel, IArchivableViewModel
    {
        public required string Id { get; set; }
        public required string Title { get; set; }

        public bool IsArchived { get; set; }

        [Display(Name = "Modified Date")]
        [DataType(DataType.Date)]
        public DateTime? ModifiedDate { get; set; }
    }
}
