﻿using Enlighten.Core.Services;
using Enlighten.Data.Models;
using Enlighten.Gpt.Client.Configuration;
using Enlighten.Study.Core.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Enlighten.Study.Web.Pages
{
    public class StudyInquireBase : ComponentBase
    {

        [Inject] public GptClientSettingsModel GptClientSettingsModel { get; set; }

        [Inject] public TextbookService TextbookService { get; set; }

        [Inject] public GptPromptService GptPromptService { get; set; }

        public string InquiryText { get; set; }

        public bool _processing = false;

        
        public TextbookUnit? SelectedUnit { get; set; }
        public List<Textbook> Textbooks { get; set; }
        public Textbook? SelectedTextbook { get; set; }
        public string botResponse { get; set; }


        protected override async Task OnInitializedAsync()
        {
            Textbooks = await TextbookService.GetTextbooks(true);
        }

        //public async Task RefreshUnits(ChangeEventArgs e)
        //{
        //    if (SelectedTextbook == null)
        //    {
        //        return;
        //    }

        //    Units = await TextbookService.GetTextbookUnits(SelectedTextbook);
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
            var promptSettings = GptPromptService.RenderGptPrompt(SelectedTextbook, SelectedUnit);


            var svc = new StudyInquireService(GptClientSettingsModel);
            var response = await svc.InquireTextbookUnit(promptSettings, SelectedUnit, InquiryText);
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
