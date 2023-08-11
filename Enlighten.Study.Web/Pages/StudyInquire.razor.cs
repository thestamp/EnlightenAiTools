using Enlighten.Data.Models;
using Enlighten.Gpt.Client.Configuration;
using Enlighten.Study.Core.Configuration;
using Enlighten.Study.Core.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Enlighten.Study.Web.Pages
{
    public class StudyInquireBase : ComponentBase
    {
        [Inject] public CoreSettingsModel CoreSettingsModel { get; set; }

        [Inject] public GptClientSettingsModel GptClientSettingsModel { get; set; }

        [Inject] public TextbookService TextbookService { get; set; }


        public string InquiryText { get; set; }

        public bool _processing = false;

        
        public TextbookChapter? SelectedChapter { get; set; }
        public List<Textbook> Textbooks { get; set; }
        public Textbook? SelectedTextbook { get; set; }
        public string botResponse { get; set; }


        protected override async Task OnInitializedAsync()
        {
            Textbooks = await TextbookService.GetTextbooks();
        }

        //public async Task RefreshChapters(ChangeEventArgs e)
        //{
        //    if (SelectedTextbook == null)
        //    {
        //        return;
        //    }

        //    Chapters = await TextbookService.GetTextbookChapters(SelectedTextbook);
        //}

        public async Task Enter(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
            {
                 await Inquire();
            }
        }

       

        public async Task Inquire()
        {
            _processing = true;
            botResponse = "";
            var svc = new StudyInquireService(CoreSettingsModel, GptClientSettingsModel);
            var response = await svc.InquireTextbookChapter(SelectedChapter, InquiryText);
            await foreach (var res in response)
            {
                botResponse += res;
               if (!botResponse.Contains("```"))
                    StateHasChanged();
                

            }

            _processing = false;
     
        }
    }
}
