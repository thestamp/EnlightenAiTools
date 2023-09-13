using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enlighten.Data.Models
{
    public class TextbookUnit : BaseGpt
    {
        public TextbookUnit()
        {
            PromptPriority = 3;
        }
        [Key]
        public int Id { get; set; }
        public virtual Textbook Textbook { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string? Content { get; set; }
    }
}
