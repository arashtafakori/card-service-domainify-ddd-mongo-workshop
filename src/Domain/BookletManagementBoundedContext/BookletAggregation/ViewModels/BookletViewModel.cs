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

        public static BookletViewModel? InstantiateFrom(Booklet? project)
        {
            if (project == null) return null;

            return new BookletViewModel()
            {
                Id = project.Id,
                Title = project.Title,
                IsArchived = Convert.ToBoolean(project.Deleted),
                ModifiedDate = project.ModifiedDate
            };
        }
    }
}
