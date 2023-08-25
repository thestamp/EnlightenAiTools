using Enlighten.Admin.Core.Services;
using Enlighten.Data.Infrastructure;
using Enlighten.Data.Models;
using Microsoft.AspNetCore.Components;

namespace Enlighten.Admin.Web.Pages
{
    public class EditTextbookUnitBase : ComponentBase
    {
        [Parameter] public int TextbookId { get; set; }
        [Parameter] public int? UnitId { get; set; }

        [Inject] public TextbookAdminService TextbookService { get; set; }
        [Inject] public DataContext DataContext { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
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
                await TextbookService.AddTextbookUnit(Textbook, Unit);
            }
            else
            {
                await TextbookService.UpdateTextbookUnit(Unit);
            }

            await DataContext.SaveChangesAsync();

            Back();

        }

        public void Back()
        {
            NavigationManager.NavigateTo($"/EditTextbook/{Textbook.Id}");
        }
    }
}
