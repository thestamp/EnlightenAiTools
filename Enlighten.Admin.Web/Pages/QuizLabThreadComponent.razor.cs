
using Enlighten.Core.Services;
using Enlighten.Data.Models;
using Enlighten.Gpt.Client.Configuration;
using Enlighten.Study.Core.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;


namespace Enlighten.Admin.Web.Pages
{
    public class QuizLabThreadComponentBase : ComponentBase
    {

        [Parameter] 
        public TextbookUnit SelectedUnit { get; set; }

        [Inject] public GptClientSettingsModel GptClientSettingsModel { get; set; }
        [Inject] public GptPromptService GptPromptService { get; set; }

        public MudTextField<string> txtAnswer { get; set; }

        public string botQuestion { get; set; }
        public string userAnswer { get; set; }
        public string botAnswerResponse { get; set; }
        public bool hasAnswer { get; set; }
        public bool isCorrect { get; set; }
        public bool isSomewhatCorrect { get; set; }

        public bool _processing = false;

        protected override async Task OnInitializedAsync()
        {

            _processing = true;
            var svc = new StudyQuizService(GptClientSettingsModel);

            //generate question
            var promptSettings = GptPromptService.RenderGptPrompt(SelectedUnit.Textbook, SelectedUnit);
            var response = await svc.GenerateQuestion(
                promptSettings,
                "TEXTBOOK SUMMARY: " + SelectedUnit.Textbook.Summary
                                     + "UNIT CONTENT: " + SelectedUnit.Content);

            await foreach (var res in response)
            {

                botQuestion += res;

                StateHasChanged();
            }

            _processing = false;

            //Textbooks = await TextbookService.GetTextbooks();
        }


        public async Task GenerateResponseAnswer()
        {
            _processing = true;


            var promptSettings = GptPromptService.RenderGptPrompt(SelectedUnit.Textbook, SelectedUnit);

            var svc = new StudyQuizService(GptClientSettingsModel);
            var response = await svc.GenerateQuestionResponseAnswer(
                promptSettings,
                "TEXTBOOK SUMMARY: " + SelectedUnit.Textbook.Summary
                                     + "UNIT CONTENT: " + SelectedUnit.Content
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
