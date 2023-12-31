﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enlighten.Data.Models
{
    public class Textbook : BaseGpt
    {
        public Textbook()
        {
            PromptPriority = 2;
        }
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string? PrivateShareId { get; set; }
        public bool IsPublished { get; set; }
        public bool IsPrivateShared { get; set; }
        public string Summary { get; set; }
        public virtual List<TextbookUnit> Units { get; set; }
    }
}
