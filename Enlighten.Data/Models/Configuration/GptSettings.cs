using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enlighten.Data.Models.Configuration
{
    public class GptDataSettingsModel: BaseGpt
    {
        public GptDataSettingsModel()
        {
            PromptPriority = 1;
        }

        public int Id { get; private set; }
    }
}
