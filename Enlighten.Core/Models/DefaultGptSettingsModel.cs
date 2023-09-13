using Enlighten.Data.Models;

namespace Enlighten.Core.Models
{
    public class DefaultGptAppSettingsModel : BaseGpt
    {
        public DefaultGptAppSettingsModel()
        {
            PromptPriority = 0;
        }
    }
}
