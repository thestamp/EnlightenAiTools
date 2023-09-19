using Enlighten.Admin.Core.Services;
using Enlighten.Core.Services;
using Enlighten.Data.Infrastructure;
using Enlighten.Data.Models;
using Microsoft.AspNetCore.Components;

namespace Enlighten.Admin.Web.Pages
{
    public class QuizLabBase : ComponentBase
    {
        [Parameter] public int? UnitId { get; set; }
        [Parameter] public int TextbookId { get; set; }
        [Inject] public TextbookAdminService TextbookService { get; set; }
        [Inject] public DataContext DataContext { get; set; }
        public TextbookUnit Unit { get; set; }
        public Textbook Textbook { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Textbook = await TextbookService.GetTextbook(TextbookId);
            Unit = Textbook.Units.First(i => i.Id == UnitId);
        }
    }
}
