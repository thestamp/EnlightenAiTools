using Enlighten.Admin.Core.Services;
using Enlighten.Core.Services;
using Enlighten.Data.Infrastructure;
using Enlighten.Data.Models;
using Microsoft.AspNetCore.Components;
using System.Threading;

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

        public List<QuizLabThreadComponentBase> LabThreads { get; set; }
        protected override void OnInitialized()
        {
            Textbook = TextbookService.GetTextbook(TextbookId).Result;
            Unit = Textbook.Units.First(i => i.Id == UnitId);

            LabThreads = new List<QuizLabThreadComponentBase>();
            for (int i = 0; i < 5; i++)
            {
                var lab = new QuizLabThreadComponent
                {
                   // SelectedUnit = Unit
                };

                LabThreads.Add(lab);
            }

        }

        public async Task RefreshLabs()
        {
            foreach (var labThread in LabThreads)
            {
                await labThread.Refresh();
            }
        }
    }
}
