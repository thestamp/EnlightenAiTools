using Enlighten.Data.Models;
using Enlighten.Gpt.Client.Configuration;
using Enlighten.Gpt.Client.Services;
using Enlighten.Gpt.Client.Services.Models;
using Enlighten.Study.Core.Configuration;

namespace Enlighten.Study.Core.Services
{
    public class StudyInquireService
    {
        private readonly CoreSettingsModel _settings;
        private readonly GptClientSettingsModel _clientSettings;

        public StudyInquireService(CoreSettingsModel settings, GptClientSettingsModel clientSettings)
        {
            _settings = settings;
            _clientSettings = clientSettings;
        }

        public async Task<IAsyncEnumerable<string>> InquireTextbookChapter(TextbookChapter chapter, string inquiry)
        {
            var client = new GptClientService(_clientSettings);

            client.Connect();
            
            var conversationSettings = GetQuestionSettings($"Textbook Summary: {chapter.Textbook.Summary} Chapter {chapter.Name} Content: {chapter.Content}");

            // The bot is requested to generate a short-answer question based on the textbook content
            var response = await client.StreamResponse(conversationSettings, $"{_settings.InquireSettings.InquiryPrompt} ' " + inquiry + "': ");
            return response;
        }

        public ConversationSettingsModel GetQuestionSettings(string textbookContent)
        {
            var conversationSettings = new ConversationSettingsModel()
            {
                SystemMessage = _settings.InquireSettings.SystemMessage
            };

            // The user provides content from a geometry textbook
            var words = textbookContent.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < words.Length; i += 500)
            {
                var part = string.Join(" ", words.Skip(i).Take(500));
                conversationSettings.ExampleDialogs.Add(new ConversationSettingsModel.ExampleDialog()
                {
                    UserInput = $"{_settings.TextbookContentSetup}:{part}",
                    BotResponse = "READY"
                });
            }

            return conversationSettings;
        }

    }
}
