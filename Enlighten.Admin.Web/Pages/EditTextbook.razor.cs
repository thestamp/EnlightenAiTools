using Enlighten.Admin.Core.Services;
using Enlighten.Core.Services;
using Enlighten.Data.Infrastructure;
using Enlighten.Data.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Enlighten.Admin.Web.Pages
{
    public class EditTextbookBase : ComponentBase
    {
        [Parameter] public int? Id { get; set; }
        [Inject] public IDialogService DialogService { get; set; }
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
                await TextbookService.AddTextbook(Textbook);
                await DataContext.SaveChangesAsync();
                // After saving, redirect to the edit page with the new ID.
                NavigationManager.NavigateTo($"/EditTextbook/{Textbook.Id}");
            }
            else
            {
                await TextbookService.UpdateTextbook(Textbook);
                await DataContext.SaveChangesAsync();
            }
        }


        public async Task Delete()
        {

            //check if units exist
            if (Textbook.Units.Any())
            {
                bool? resultUnitsExist = await DialogService.ShowMessageBox(
                    "Delete Textbook",
                    "Remove all units before deleting Textbook",
                    yesText: "Delete Anyway!", cancelText: "Cancel");

                if (!(resultUnitsExist ?? false))
                {
                    return;
                }

            }

            bool? result = await DialogService.ShowMessageBox(
                "Warning",
                "Deleting can not be undone!",
                yesText: "Delete!", cancelText: "Cancel");

            if (result ?? false)
            {
                await TextbookService.DeleteTextbook(Textbook);
                await DataContext.SaveChangesAsync();

                Back();
            }
        }

        public void Back()
        {
            NavigationManager.NavigateTo("/Textbooks");
        }


        public async Task PromptSaveBeforeNavigate(int? textbookId = null, int? unitId = null)
        {
            string targetUrl = unitId.HasValue ?
                $"/EditTextbook/{textbookId}/EditUnit/{unitId}" :
                $"/EditTextbook/{textbookId}/EditUnit";
            if (true) // Determine if changes were made.
            {
                bool? result = await DialogService.ShowMessageBox(
                    "Unsaved Changes",
                    "You have unsaved changes. Would you like to save before navigating?",
                    yesText: "Save and Continue", cancelText: "Stay Here");

                if (result == true)
                {
                    await Save();
                    NavigationManager.NavigateTo(targetUrl);
                }
            }
            else
            {
                NavigationManager.NavigateTo(targetUrl);
            }
        }




    }
}
