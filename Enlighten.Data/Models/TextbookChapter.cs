using System.ComponentModel.DataAnnotations.Schema;

namespace Enlighten.Data.Models
{
    public class TextbookChapter : IBaseGpt
    {
        public int Id { get; set; }
        public virtual Textbook Textbook { get; set; }
        public string ChapterName { get; set; }
        public string ChapterContent { get; set; }
        public string QuizSystemMessage { get; set; }
        public string QuizQuestionPrompt { get; set; }
        public string QuizAnswerPrompt { get; set; }
        public string InquireSystemMessage { get; set; }
        public string InquirePrompt { get; set; }
        public string ContentStart { get; set; }
        public string ContentEnd { get; set; }
    }
}
