using Domainify.Domain;
using System.ComponentModel.DataAnnotations;

namespace Module.Domain.CardAggregation
{
    public class CardViewModel : ViewModel, IModifiedViewModel, IDeletableViewModel
    {
        public string Id { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }

        [Display(Name = "Modified Date")]
        [DataType(DataType.Date)]
        public DateTime? ModifiedDate { get; set; }

        public string BookletId { get; set; } = string.Empty;
        public string IndexId { get; set; } = string.Empty;
        public short Type { get; set; }
        public long Order { get; set; }
        public string Expression { get; set; } = string.Empty;
        public string ExpressionLanguage { get; set; } = string.Empty;
        public string Translation { get; set; } = string.Empty;
        public string TranslationLanguage { get; set; } = string.Empty;
    }
}
