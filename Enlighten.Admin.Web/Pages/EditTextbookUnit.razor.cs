using Enlighten.Admin.Core.Services;
using Enlighten.Data.Infrastructure;
using Enlighten.Data.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Enlighten.Admin.Web.Pages
{
    public class EditTextbookUnitBase : ComponentBase
    {
        [Parameter] public int TextbookId { get; set; }
        [Parameter] public int? UnitId { get; set; }

        [Inject] public TextbookAdminService TextbookService { get; set; }
        [Inject] public DataContext DataContext { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] IDialogService DialogService { get; set; }
        public TextbookUnit? Unit { get; set; }
        public Textbook Textbook { get; set; }

        protected override async Task OnInitializedAsync()
        {

            Textbook = await TextbookService.GetTextbook(TextbookId);

            if (UnitId != null)
            {
                Unit = Textbook.Units.First(i => i.Id == UnitId);
            }
            else
            {
                Unit = TextbookService.CreateTextbookUnit();
            }
        }

        public async Task Save()
        {
            if (UnitId == null)
            {
                TextbookService.AddTextbookUnit(Textbook, Unit);
                await DataContext.SaveChangesAsync();
                NavigationManager.NavigateTo($"/EditTextbook/{Textbook.Id}/EditUnit/{Unit.Id}");
            }
            else
            {
                TextbookService.UpdateTextbookUnit(Unit);
                await DataContext.SaveChangesAsync();
            }

            

        }

        public async Task Delete()
        {

            bool? result = await DialogService.ShowMessageBox(
                "Warning",
                "Deleting can not be undone!",
                yesText: "Delete!", cancelText: "Cancel");

            if (result ?? false)
            {
                TextbookService.DeleteTextbookUnit(Unit);
                await DataContext.SaveChangesAsync();

                Back();
            }
        }

        public void Back()
        {
            NavigationManager.NavigateTo($"/EditTextbook/{Textbook.Id}");
        }

        
    }
}
