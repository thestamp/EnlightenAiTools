using System.ComponentModel.DataAnnotations.Schema;

namespace Enlighten.Data.Models
{
    [NotMapped]
    public class TextbookChapter
    {
        public int Id { get; set; }
        public virtual Textbook Textbook { get; set; }
        public string ChapterName { get; set; }
        public string ChapterContent { get; set; }
    }
}
