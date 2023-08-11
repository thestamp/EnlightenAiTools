namespace Enlighten.Data.Models
{
    public class Textbook
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string TextbookSummary { get; set; }
        public virtual List<TextbookChapter> Chapters { get; set; }
        //note: multiple schools can use multiple textbooks (many to many)

    }
}
