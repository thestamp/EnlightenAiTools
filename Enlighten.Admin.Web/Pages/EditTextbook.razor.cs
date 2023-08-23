using Enlighten.Admin.Core.Services;
using Enlighten.Core.Services;
using Enlighten.Data.Infrastructure;
using Enlighten.Data.Models;
using Microsoft.AspNetCore.Components;

namespace Enlighten.Admin.Web.Pages
{
    public class EditTextbookBase : ComponentBase
    {
        [Parameter] public int? Id { get; set; }
        [Inject] public TextbookAdminService TextbookService { get; set; }
        [Inject] public DataContext DataContext { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
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

        public async Task Save()
        {
            if (Id == null)
            {
                TextbookService.AddTextbook(Textbook);
            }

            await DataContext.SaveChangesAsync();

            NavigationManager.NavigateTo("/Textbooks");

        }

    }
}
