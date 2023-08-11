using Enlighten.Gpt.Client.Configuration;
using Enlighten.Gpt.Client.Services;
using Enlighten.Gpt.Client.Services.Models;
using Enlighten.Study.Core.Configuration;

namespace Enlighten.Study.Core.Services
{
    public class StudyQuizService
    {
        private readonly CoreSettingsModel _settings;
        private readonly GptClientSettingsModel _clientSettings;

        public StudyQuizService(CoreSettingsModel settings, GptClientSettingsModel clientSettings)
        {
            _settings = settings;
            _clientSettings = clientSettings;
        }

        public async Task<string> GenerateQuestion(string textbookContent)
        {
            var client = new GptClientService(_clientSettings);

            client.Connect();
            

            var conversationSettings = GetQuestionSettings(textbookContent);

            // The bot is requested to generate a short-answer question based on the textbook content
            var response = await client.GetResponse(conversationSettings, _settings.QuizSettings.GenerateQuestionPrompt);
            return response;
        }

        public async Task<IAsyncEnumerable<string>> GenerateQuestionResponseAnswer(string textbookContent, string botQuestion, string userAnswer)
        {
            var client = new GptClientService(_clientSettings);

            client.Connect();

            var conversationSettings = GetQuestionSettings(textbookContent);

            // catching the bot up on the question asked before
            conversationSettings.ExampleDialogs.Add(new ConversationSettingsModel.ExampleDialog()
            {
                UserInput = _settings.QuizSettings.GenerateQuestionPrompt,
                BotResponse = botQuestion
            });


            // The bot is requested to generate a short-answer question based on the textbook content
            var response = await client.StreamResponse(conversationSettings, _settings.QuizSettings.GenerateResponseAnswerPrompt.Replace("{userAnswer}", userAnswer));
            return response;
        }


        public ConversationSettingsModel GetQuestionSettings(string textbookContent)
        {
            var conversationSettings = new ConversationSettingsModel()
            {
                SystemMessage = _settings.QuizSettings.SystemMessage
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
