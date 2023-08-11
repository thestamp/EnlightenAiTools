using OpenAI_API.Chat;

namespace Enlighten.Gpt.Client.Services.Models
{
    public class ConversationModel
    {
        private readonly Conversation _gptConversation;

        public ConversationModel(Conversation gptConversation)
        {
            _gptConversation = gptConversation;
        }
    }
}
