using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enlighten.Data.Models;
using Enlighten.Data.Models.Configuration;

namespace Enlighten.Study.Core.Configuration
{
    public class DefaultGptAppSettingsModel : BaseGpt
    {
        public DefaultGptAppSettingsModel()
        {
            PromptPriority = 0;
        }
    }
}
