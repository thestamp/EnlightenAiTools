using System.ComponentModel.DataAnnotations.Schema;

namespace Enlighten.Data.Models
{
    public class Textbook : BaseGpt
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string TextbookSummary { get; set; }
        public virtual List<TextbookChapter> Chapters { get; set; }
    }
}
