namespace Enlighten.Study.Core.Configuration;

public class CoreSettingsModel
{
    public QuizSettingsModel QuizSettings { get; set; }
    public InquireSettingsModel InquireSettings { get; set; }
    

    public string TextbookContentSetup { get; set; }
    public string ElaboratePrompt { get; set; }
    public class QuizSettingsModel
    {
        public string SystemMessage { get; set; }
        public string GenerateQuestionPrompt { get; set; }
        public string GenerateResponseAnswerPrompt { get; set; }
    }

    public class InquireSettingsModel
    {
        public string SystemMessage { get; set; }
        public string InquiryPrompt { get; set; }
    }


}