using System.ComponentModel.DataAnnotations.Schema;

namespace Enlighten.Data.Models
{
    public class Textbook : BaseGpt
    {
        public Textbook()
        {
            PromptPriority = 2;
        }
        public int Id { get; set; }

        public string Name { get; set; }
        public string Summary { get; set; }
        public virtual List<TextbookUnit> Units { get; set; }
    }
}
