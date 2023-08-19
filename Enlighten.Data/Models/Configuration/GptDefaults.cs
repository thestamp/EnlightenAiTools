using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enlighten.Data.Models.Configuration
{
    public class GptDefaults : BaseGpt
    {
        public GptDefaults()
        {
            PromptPriority = 1;
        }

        public int Id { get; private set; }
    }
}
