using System.ComponentModel.DataAnnotations.Schema;

namespace Enlighten.Data.Models
{
    [NotMapped]
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
