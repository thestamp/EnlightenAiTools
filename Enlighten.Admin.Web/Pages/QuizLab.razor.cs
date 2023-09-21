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

        private List<QuizLabThreadComponent> _labThreadComponents = new List<QuizLabThreadComponent>();
        public QuizLabThreadComponent LabThreadComponents
        {
            get => null; // A getter is required, but not actually used here
            set => _labThreadComponents.Add(value); // Every time a component is created, it's added to our list
        }

        protected override void OnInitialized()
        {
            Textbook = TextbookService.GetTextbook(TextbookId).Result;
            Unit = Textbook.Units.First(i => i.Id == UnitId);

            //LabThreads = new List<QuizLabThreadComponentBase>();
            //for (int i = 0; i < 5; i++)
            //{
            //    var lab = new QuizLabThreadComponent
            //    {
            //       // SelectedUnit = Unit
            //    };

            //    LabThreads.Add(lab);
            //}

        }

        public async Task RefreshLabs()
        {
            var tasks = new List<Task>();

            foreach (var labThread in _labThreadComponents)
            {
                tasks.Add(labThread.Refresh());
            }

            await Task.WhenAll(tasks);
        }

    }
}
