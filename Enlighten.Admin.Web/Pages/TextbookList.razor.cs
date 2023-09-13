using Enlighten.Core.Services;
using Enlighten.Data.Models;
using Microsoft.AspNetCore.Components;

namespace Enlighten.Admin.Web.Pages
{
    public class TextbookListBase : ComponentBase
    {
        public List<Textbook> Textbooks { get; set; }
        [Inject] public TextbookService TextbookService { get; set; }


        protected override async Task OnInitializedAsync()
        {
            Textbooks = await TextbookService.GetTextbooks();
        }

        public void AddTextbook()
        {

        }
    }
}
