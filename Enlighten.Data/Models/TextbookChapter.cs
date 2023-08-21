using System.ComponentModel.DataAnnotations.Schema;

namespace Enlighten.Data.Models
{
    public class TextbookChapter : BaseGpt
    {
        public TextbookChapter()
        {
            PromptPriority = 3;
        }
        public int Id { get; set; }
        public virtual Textbook Textbook { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
    }
}
