using Enlighten.Data.Models;
using Microsoft.AspNetCore.Components;

namespace Enlighten.Admin.Web.Pages
{
    public class GptSettingsComponentBase : ComponentBase
    {
        [Parameter]
        public BaseGpt Settings { get; set; }
    }
}
