using Enlighten.Core.Services;
using Enlighten.Data.Models.Configuration;
using Enlighten.Gpt.Client.Configuration;
using Enlighten.Gpt.Client.Services;
using Enlighten.Gpt.Client.Services.Models;

namespace Enlighten.Study.Core.Services
{
    public class StudyContentService
    {
        private readonly GptClientSettingsModel _clientSettings;

        public StudyContentService(GptClientSettingsModel clientSettings)
        {
            _clientSettings = clientSettings;
        }

        public async Task<IAsyncEnumerable<string>> GetStructuredContent(GptPromptService.GptPromptRenderModel gptPromptSettings, string textbookContent)
        {
            var client = new GptClientService(_clientSettings);

            client.Connect();

            var conversationSettings = InitializeConversation(gptPromptSettings, textbookContent);

            // The bot is requested to generate a short-answer question based on the textbook content
            var response = await client.StreamResponse(conversationSettings,
                "From the provided textbook content, extract and return a structured json format, breaking down the content into topics with summaries, subtopics with summaries, and other important details to be used as a knowledge reference for generating questions. Be sure to include the relevant section numbers.");
            return response;
        }


        public async Task<IAsyncEnumerable<string>> GetSummary(GptPromptService.GptPromptRenderModel gptPromptSettings, string textbookContent)
        {
            var client = new GptClientService(_clientSettings);

            client.Connect();

            var conversationSettings = InitializeConversation(gptPromptSettings, textbookContent);

            // The bot is requested to generate a short-answer question based on the textbook content
            var response = await client.StreamResponse(conversationSettings,
                "From the provided textbook content, generate a 1 sentence summary");
            return response;
        }

        public async Task<IAsyncEnumerable<string>> GetTopicList(GptPromptService.GptPromptRenderModel gptPromptSettings, string textbookContent)
        {
            var client = new GptClientService(_clientSettings);

            client.Connect();

            var conversationSettings = InitializeConversation(gptPromptSettings, textbookContent);

            // The bot is requested to generate a short-answer question based on the textbook content
            var response = await client.StreamResponse(conversationSettings,
                "From the provided textbook content, generate a topic list in a comma-delimited format");
            return response;
        }

        public ConversationSettingsModel InitializeConversation(GptPromptService.GptPromptRenderModel gptPromptSettings, string textbookContent)
        {
            var conversationSettings = new ConversationSettingsModel()
            {
                SystemMessage = gptPromptSettings.QuizSystemMessage
            };

            // The user provides content from a geometry textbook
            var words = textbookContent.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < words.Length; i += 500)
            {
                var part = string.Join(" ", words.Skip(i).Take(500));
                conversationSettings.ExampleDialogs.Add(new ConversationSettingsModel.ExampleDialog()
                {
                    UserInput = $"{gptPromptSettings.ContentStart}:{part}",
                    BotResponse = "READY"
                });
            }

            return conversationSettings;
        }

    }
}
