using Domainify.Domain;
using System.ComponentModel.DataAnnotations;

namespace Domain.BookletAggregation
{
    public class BookletViewModel : ViewModel, IModifiedViewModel, IDeletableViewModel
    {
        public string Id { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        
        [Display(Name = "Modified Date")]
        [DataType(DataType.Date)]
        public DateTime? ModifiedDate { get; set; }

        public short Type { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<IndexViewModel> Indices { get; set; } = new List<IndexViewModel>();
    }
}
