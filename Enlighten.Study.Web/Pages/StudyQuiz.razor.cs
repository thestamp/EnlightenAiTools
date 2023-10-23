using Enlighten.Core.Services;
using Enlighten.Data.Models;
using Enlighten.Data.Models.Configuration;
using Enlighten.Gpt.Client.Configuration;
using Enlighten.Study.Core.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.IdentityModel.Tokens;
using MudBlazor;

namespace Enlighten.Study.Web.Pages
{
    public class StudyQuizBase : ComponentBase
    {
        [Parameter] public string? ShareId { get; set; }
        [Inject] public GptClientSettingsModel GptClientSettingsModel { get; set; }

        [Inject] public TextbookService TextbookService { get; set; }

        [Inject] public GptPromptService GptPromptService { get; set; }

        [Inject] NavigationManager NavigationManager { get; set; }

        public MudTextField<string> txtAnswer { get; set; }

        public MudButton NextQuestionButton { get; set; }
        public List<StudyTopicTrackerService.TopicTrackerModel> SelectedUnitTopicTrackerModels { get; set; }
        public bool hasAnswer { get; set; }
        public bool isCorrect { get; set; }
        public bool isSomewhatCorrect { get; set; }

        public bool _processing = false;

        
        public TextbookUnit? SelectedUnit { get; set; }
        public List<Textbook> Textbooks { get; set; }
        public Textbook? SelectedTextbook { get; set; }
        public string SelectedTopic { get; set; }
        public string botQuestion { get; set; }
        public string userAnswer { get; set; }
        public string botAnswerResponse { get; set; }

        public bool IsRandomUnit { get; set; }
        
        public Dictionary<Textbook, StudyTopicTrackerService> TopicTrackerServiceList { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (ShareId != null)
            {
                var sharedTextbook = await TextbookService.GetTextbookFromShareId(ShareId);

                if (sharedTextbook == null)
                {
                    NavigationManager.NavigateTo("/");
                }
                else
                {
                    Textbooks = new List<Textbook>()
                    {
                        sharedTextbook
                    };

                    SelectedTextbook = sharedTextbook;
                }

            }
            else
            {
                Textbooks = await TextbookService.GetTextbooks(true);
            }

            

            TopicTrackerServiceList = new Dictionary<Textbook, StudyTopicTrackerService>();
            foreach (var textbook in Textbooks)
            {
                TopicTrackerServiceList.Add(textbook, new StudyTopicTrackerService(textbook));
            }


        }

        public async Task RefreshUnits()
        {
            
            SelectedUnitTopicTrackerModels = TopicTrackerServiceList[SelectedTextbook].TrackerUnits.SelectMany(j => j.Topics, (model, trackerModel) => trackerModel).ToList();
        }

        //public async Task Enter(KeyboardEventArgs e)
        //{
        //    if (e.Code == "Enter" || e.Code == "NumpadEnter")
        //    {
        //         await GenerateResponseAnswer();
        //    }
        //}
        public async Task GenerateQuestion(TextbookUnit unit = null, string topic = null)
        {
            //reset some things
            botQuestion = "";
            userAnswer = "";
            botAnswerResponse = "";
            hasAnswer = false;
            var isRandomTopic = false;
            _processing = true;

            //are we forcing a specific unit or topic?
            if (unit != null)
            {
                SelectedUnit = unit;
                IsRandomUnit = false;
            }

            if (!string.IsNullOrEmpty(topic))
            {
                if (unit == null)
                {
                    throw new ArgumentException("Unit is required to select a topic");
                }

                //there no dropdown for topic (right now), so we will set it here
                SelectedTopic = topic;
                isRandomTopic = false;
            }
            else
            {
                isRandomTopic = true;
            }
            
            var svc = new StudyQuizService(GptClientSettingsModel);

            //if random unit is selected, we will select a random unit
            if (IsRandomUnit && SelectedTextbook != null)
            {
                var randomIndex = new Random().Next(SelectedTextbook.Units.Count - 1);
                SelectedUnit = SelectedTextbook.Units[randomIndex];
            }

            //select random topic
            if (isRandomTopic)
            {
                var topicList = SelectedUnit.TopicList.Split(',');
                var rnd = new Random();
                var randomTopicIndex = rnd.NextInt64(0, topicList.Length);
                SelectedTopic = topicList[randomTopicIndex];
            }

            var promptSettings = GptPromptService.RenderGptPrompt(SelectedTextbook, SelectedUnit);

            //apply selected topic to prompt (TODO: find proper place for this logic)
            promptSettings.QuizQuestionPrompt = promptSettings.QuizQuestionPrompt?.Replace("{topic}", SelectedTopic);

            var response = await svc.GenerateQuestion(
                promptSettings,
                "TEXTBOOK SUMMARY: " + SelectedTextbook.Summary 
                +"UNIT CONTENT: " + SelectedUnit.Content);

            await foreach (var res in response)
            {

                botQuestion += res;

                StateHasChanged();
            }

            _processing = false;

            await txtAnswer.Clear();
            await txtAnswer.FocusAsync();
            StateHasChanged();
        }

        public async Task GenerateResponseAnswer()
        {
            _processing = true;


            var promptSettings = GptPromptService.RenderGptPrompt(SelectedTextbook, SelectedUnit);

            var svc = new StudyQuizService(GptClientSettingsModel);
            var response = await svc.GenerateQuestionResponseAnswer(
                promptSettings,
                "TEXTBOOK SUMMARY: " + SelectedTextbook.Summary
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

            StateHasChanged();

            if (isCorrect)
            {
                //focus on next question button
                await NextQuestionButton.FocusAsync();
            }
            else
            {
                //highlight text to try again
                await txtAnswer.FocusAsync();
                await txtAnswer.SelectAsync();
                //
            }

            TopicTrackerServiceList[SelectedTextbook].AddAttemptResult(SelectedUnit, SelectedTopic, isCorrect);
        }


        public async void OnKeyDown(KeyboardEventArgs args)
        {
            if (args.Key == "Enter")
            {
                await GenerateResponseAnswer();
                StateHasChanged();
            }
        }
    }
}
