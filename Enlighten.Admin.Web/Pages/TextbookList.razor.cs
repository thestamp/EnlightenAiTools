using Enlighten.Admin.Core.Services;
using Enlighten.Core.Services;
using Enlighten.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace Enlighten.Admin.Web.Pages
{
    [Authorize]
    public class TextbookListBase : ComponentBase
    {
        public List<Textbook> Textbooks { get; set; }
        [Inject] public TextbookAdminService TextbookService { get; set; }


        protected override async Task OnInitializedAsync()
        {
            Textbooks = await TextbookService.GetTextbooks();
        }

        public void AddTextbook()
        {

        }
    }
}
