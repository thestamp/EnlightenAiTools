using Enlighten.Admin.Core.Services;
using Enlighten.Core.Services;
using Enlighten.Data.Models;
using Microsoft.AspNetCore.Components;

namespace Enlighten.Admin.Web.Pages
{
    public class EditTextbookBase : ComponentBase
    {
        [Parameter] public int? Id { get; set; }
        [Inject] public TextbookAdminService TextbookService { get; set; }
        public Textbook Textbook { get; set; }


        protected override async Task OnInitializedAsync()
        {
            if (Id != null)
            {
                Textbook = await TextbookService.GetTextbook(Id.Value);
            }
            else
            {
                Textbook = TextbookService.CreateTextbook();
            }
            
        }

    }
}
