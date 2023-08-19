using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enlighten.Data.Models.Configuration
{
    internal class GptDefaults : IBaseGpt
    {
        public int Id { get; set; }
        public string QuizSystemMessage { get; set; }
        public string QuizQuestionPrompt { get; set; }
        public string QuizAnswerPrompt { get; set; }
        public string InquireSystemMessage { get; set; }
        public string InquirePrompt { get; set; }
        public string ContentStart { get; set; }
        public string ContentEnd { get; set; }
    }
}
