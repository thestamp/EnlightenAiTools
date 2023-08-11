using Enlighten.Gpt.Client.Configuration;
using Enlighten.Gpt.Client.Services.Models;
using OpenAI_API;
using OpenAI_API.Models;

namespace Enlighten.Gpt.Client.Services
{
    public class GptClientService
    {
        private readonly GptClientSettingsModel _settings;
        private OpenAIAPI? client;

        public GptClientService(GptClientSettingsModel settings)
        {
            _settings = settings;
        }

        //currently using https://github.com/OkGoDoIt/OpenAI-API-dotnet
        public void Connect()
        {
            client = new OpenAIAPI(_settings.ApiKey);
        }


        public async Task<string> GetResponse(ConversationSettingsModel settings, string userInput)
        {
            var chat = client.Chat.CreateConversation();
            chat.Model = Model.GPT4;

            // give instruction as System
            chat.AppendSystemMessage(settings.SystemMessage);

            // give a few examples as user and assistant
            foreach (var dialog in settings.ExampleDialogs)
            {
                chat.AppendUserInput(dialog.UserInput);
                chat.AppendExampleChatbotOutput(dialog.BotResponse);
            }

            // now let's ask it a question
            chat.AppendUserInput(userInput);
            // and get the response
            var response = await chat.GetResponseFromChatbotAsync();
            return response;
        }

        public async Task<IAsyncEnumerable<string>> StreamResponse(ConversationSettingsModel settings, string userInput)
        {
            var chat = client.Chat.CreateConversation();
            chat.Model = Model.ChatGPTTurbo;

            // give instruction as System
            chat.AppendSystemMessage(settings.SystemMessage);

            // give a few examples as user and assistant
            foreach (var dialog in settings.ExampleDialogs)
            {
                chat.AppendUserInput(dialog.UserInput);
                chat.AppendExampleChatbotOutput(dialog.BotResponse);
            }

            // now let's ask it a question
            chat.AppendUserInput(userInput);
            // and get the response

            return chat.StreamResponseEnumerableFromChatbotAsync();
            //await foreach (var res in chat.StreamResponseEnumerableFromChatbotAsync())
            //{
            //    Console.Write(res);
            //}
            //return response;
        }

    }
}
