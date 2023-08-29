using Enlighten.Core.Services;
using Enlighten.Data.Models;
using Enlighten.Gpt.Client.Configuration;
using Enlighten.Gpt.Client.Services;
using Enlighten.Gpt.Client.Services.Models;
using System.Runtime;

namespace Enlighten.Study.Core.Services
{
    public class StudyInquireService
    {
        private readonly GptClientSettingsModel _clientSettings;

        public StudyInquireService(GptClientSettingsModel clientSettings)
        {
            _clientSettings = clientSettings;
        }

        public async Task<IAsyncEnumerable<string>> InquireTextbookUnit(GptPromptService.GptPromptRenderModel gptPromptSettings, TextbookUnit unit, string inquiry)
        {
            var client = new GptClientService(_clientSettings);

            client.Connect();

            var conversationSettings = InitializeConversation(gptPromptSettings, unit.Content);


            // The bot is requested to generate a short-answer question based on the textbook content
            var response = await client.StreamResponse(conversationSettings, $"{gptPromptSettings.InquirePrompt} ' " + inquiry + "': ");
            return response;
        }
        
        public ConversationSettingsModel InitializeConversation(GptPromptService.GptPromptRenderModel gptPromptSettings, string textbookContent)
        {

            var conversationSettings = new ConversationSettingsModel()
            {
                SystemMessage = gptPromptSettings.InquireSystemMessage
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
