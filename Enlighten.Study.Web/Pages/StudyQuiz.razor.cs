using Enlighten.Data.Models;
using Enlighten.Gpt.Client.Configuration;
using Enlighten.Study.Core.Configuration;
using Enlighten.Study.Core.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Enlighten.Study.Web.Pages
{
    public class StudyQuizBase : ComponentBase
    {
        [Inject] public CoreSettingsModel CoreSettingsModel { get; set; }

        [Inject] public GptClientSettingsModel GptClientSettingsModel { get; set; }

        [Inject] public TextbookService TextbookService { get; set; }



        public MudTextField<string> txtAnswer { get; set; }
        public bool hasAnswer { get; set; }
        public bool isCorrect { get; set; }
        public bool isSomewhatCorrect { get; set; }

        public bool _processing = false;

        
        public TextbookChapter? SelectedChapter { get; set; }
        public List<Textbook> Textbooks { get; set; }
        public Textbook? SelectedTextbook { get; set; }
        public string botQuestion { get; set; }
        public string userAnswer { get; set; }
        public string botAnswerResponse { get; set; }

        public bool IsRandomChapter { get; set; }

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

        //public async Task Enter(KeyboardEventArgs e)
        //{
        //    if (e.Code == "Enter" || e.Code == "NumpadEnter")
        //    {
        //         await GenerateResponseAnswer();
        //    }
        //}

        public async Task GenerateQuestion()
        {
       
            //reset some things
            userAnswer = "";
            botAnswerResponse = "";
            hasAnswer = false;


            _processing = true;
            var svc = new StudyQuizService(CoreSettingsModel, GptClientSettingsModel);

            //if random chapter is selected, we will select a random chapter
            if (IsRandomChapter && SelectedTextbook != null)
            {
                var randomIndex = new Random().Next(SelectedTextbook.Chapters.Count - 1);
                SelectedChapter = SelectedTextbook.Chapters[randomIndex];
            }

            botQuestion = await svc.GenerateQuestion(
                "TEXTBOOK SUMMARY: " + SelectedTextbook.TextbookSummary 
                +"CHAPTER CONTENT: " + SelectedChapter.ChapterContent);
            _processing = false;
        }

        public async Task GenerateResponseAnswer()
        {
            _processing = true;
            var svc = new StudyQuizService(CoreSettingsModel, GptClientSettingsModel);
            var response = await svc.GenerateQuestionResponseAnswer(
                "TEXTBOOK SUMMARY: " + SelectedTextbook.TextbookSummary
                + "CHAPTER CONTENT: " + SelectedChapter.ChapterContent
                , botQuestion, userAnswer);
            var responseContent = "";
            await foreach (var res in response)
            {
                hasAnswer = true;

                responseContent += res;

                if (responseContent.Length > 3)
                {
                    isCorrect = responseContent.StartsWith("#Y#");
                    isSomewhatCorrect = responseContent.StartsWith("#S#");
                    botAnswerResponse = responseContent.Substring(3);
                }

                StateHasChanged();
            
            }

            _processing = false;
           
        }
    }
}
