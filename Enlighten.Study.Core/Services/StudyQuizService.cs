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



        public async Task<IAsyncEnumerable<string>> GenerateQuestion(ConversationSettingsModel conversationSettings)
        {
            var client = new GptClientService(_clientSettings);

            client.Connect();

            // The bot is requested to generate a short-answer question based on the textbook content
            var response = await client.StreamResponse(conversationSettings);
            return response;
        }

        public ConversationSettingsModel GenerateConversation(GptPromptService.GptPromptRenderModel gptPromptSettings, string textbookContent, string botQuestion, string userAnswer = null)
        {
            var conversationSettings = InitializeConversation(gptPromptSettings, textbookContent, userAnswer);

            // catching the bot up on the question asked before
            conversationSettings.ExampleDialogs.Add(new ConversationSettingsModel.ExampleDialog()
            {
                UserInput = gptPromptSettings.QuizQuestionPrompt,
                BotResponse = botQuestion
            });

            return conversationSettings;
        }

        public async Task<IAsyncEnumerable<string>> GenerateQuestionResponseAnswer(ConversationSettingsModel conversationSettings)
        {
            var client = new GptClientService(_clientSettings);

            client.Connect();

            // The bot is requested to generate a short-answer question based on the textbook content
            var response = await client.StreamResponse(conversationSettings);
            return response;
        }


        public ConversationSettingsModel InitializeConversation(GptPromptService.GptPromptRenderModel gptPromptSettings, string textbookContent, string userAnswer)
        {
            var conversationSettings = new ConversationSettingsModel()
            {
                SystemMessage = gptPromptSettings.QuizSystemMessage,
                UserMessage = userAnswer
            };

            // The reference content
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
