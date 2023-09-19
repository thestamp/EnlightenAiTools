using Enlighten.Core.Services;
using Enlighten.Data.Models.Configuration;
using Enlighten.Gpt.Client.Configuration;
using Enlighten.Gpt.Client.Services;
using Enlighten.Gpt.Client.Services.Models;

namespace Enlighten.Study.Core.Services
{
    public class StudyQuizService
    {
        private readonly GptClientSettingsModel _clientSettings;

        public StudyQuizService(GptClientSettingsModel clientSettings)
        {
            _clientSettings = clientSettings;
        }

        public async Task<IAsyncEnumerable<string>> GenerateQuestion(GptPromptService.GptPromptRenderModel gptPromptSettings, string textbookContent)
        {
            var client = new GptClientService(_clientSettings);

            client.Connect();
            

            var conversationSettings = InitializeConversation(gptPromptSettings, textbookContent);

            // The bot is requested to generate a short-answer question based on the textbook content
            var response = await client.StreamResponse(conversationSettings, gptPromptSettings.QuizQuestionPrompt);
            return response;
        }

        public async Task<IAsyncEnumerable<string>> GenerateQuestionResponseAnswer(GptPromptService.GptPromptRenderModel gptPromptSettings, string textbookContent, string botQuestion, string userAnswer)
        {
            var client = new GptClientService(_clientSettings);

            client.Connect();

            var conversationSettings = InitializeConversation(gptPromptSettings, textbookContent);

            // catching the bot up on the question asked before
            conversationSettings.ExampleDialogs.Add(new ConversationSettingsModel.ExampleDialog()
            {
                UserInput = gptPromptSettings.QuizQuestionPrompt,
                BotResponse = botQuestion
            });

            // The bot is requested to generate a short-answer question based on the textbook content
            var response = await client.StreamResponse(conversationSettings, gptPromptSettings.QuizAnswerPrompt.Replace("{userAnswer}", userAnswer));
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
