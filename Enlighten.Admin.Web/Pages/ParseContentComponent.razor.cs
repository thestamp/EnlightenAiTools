using Enlighten.Core.Services;
using Enlighten.Data.Models;
using Enlighten.Gpt.Client.Configuration;
using Enlighten.Study.Core.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MudBlazor;

namespace Enlighten.Admin.Web.Pages
{
    public class ParseContentComponentBase : ComponentBase
    {
        [Inject] private IDialogService DialogService { get; set; }

        [Inject] public GptClientSettingsModel GptClientSettingsModel { get; set; }
        [Inject] public GptPromptService GptPromptService { get; set; }
        [Parameter]
        public TextbookUnit Unit { get; set; }

        public string errorMessage = string.Empty;
        public bool _processingContent;
        public bool _processingName;
        public bool _processingSummary;
        public bool _processingTopicList;
        private StudyContentService svc;

        protected override async Task OnInitializedAsync()
        {
            svc = new StudyContentService(GptClientSettingsModel);

        }

        public async Task LoadFile(IBrowserFile file)
        {
            _processingContent = true;
            _processingName = true;
            _processingSummary = true;
            _processingTopicList = true;
            errorMessage = string.Empty;

            Unit.Content = "";
            Unit.Name = "";
            Unit.TopicList = "";
            Unit.Summary = "";

            using var stream = new MemoryStream();
            await file.OpenReadStream().CopyToAsync(stream);

            //make it delimited
            stream.Position = 0;
            var contentStringUntrimmed = System.Text.Encoding.UTF8.GetString(stream.ToArray());
            var contentString = contentStringUntrimmed.Trim(new char[] { '\uFEFF', '\u200B' });//trim the BOM https://en.wikipedia.org/wiki/Byte_Order_Mark

            if (string.IsNullOrEmpty(contentString))
            {
                await DialogService.ShowMessageBox("No Content", "File content is empty!");
                return;
            }

            try
            {
                await GetStructuredContent(contentString);
                await GetName();
                await GetSummary();
                await GetTopicList();
                

            }
            catch (Exception exception)
            {
                errorMessage = exception.Message + exception.StackTrace;
                throw;
            }

            

        }


        public async Task GetContent()
        {
            if (string.IsNullOrEmpty(Unit.Content))
            {
                await DialogService.ShowMessageBox("No Content", "Please enter some content first!");
                return;
            }

            var oldContent = Unit.Content;
            await GetStructuredContent(oldContent);
        }
        public async Task GetStructuredContent(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                await DialogService.ShowMessageBox("No Content", "No content specified!");
                return;
            }

            _processingContent = true;
            StateHasChanged();

            Unit.Content = "";
            //generate question
            var promptSettings = GptPromptService.RenderGptPrompt(); //basics only
            var response = await svc.GetStructuredContent(promptSettings, "UNIT CONTENT: " + content);

            await foreach (var res in response)
            {

                Unit.Content += res;


                StateHasChanged();
            }

            _processingContent = false;
            StateHasChanged();
        }

        public async Task GetSummary()
        {
            if (string.IsNullOrEmpty(Unit.Content))
            {
                await DialogService.ShowMessageBox("No Content", "Please enter some content first!");
                return;
            }

            _processingSummary = true;
            StateHasChanged();

            Unit.Summary = "";
            //generate question
            var promptSettings = GptPromptService.RenderGptPrompt(); //basics only
            var response = await svc.GetSummary(promptSettings, "UNIT CONTENT: " + Unit.Content);

            await foreach (var res in response)
            {

                Unit.Summary += res;

                StateHasChanged();
            }

            _processingSummary = false;
            StateHasChanged();
        }

        public async Task GetTopicList()
        {
            if (string.IsNullOrEmpty(Unit.Content))
            {
                await DialogService.ShowMessageBox("No Content", "Please enter some content first!");
                return;
            }

            _processingTopicList = true;
            StateHasChanged();

            Unit.TopicList = "";
            //generate question
            var promptSettings = GptPromptService.RenderGptPrompt(); //basics only
            var response = await svc.GetTopicList(promptSettings, "UNIT CONTENT: " + Unit.Content);

            await foreach (var res in response)
            {

                Unit.TopicList += res;

                StateHasChanged();
            }

            _processingTopicList = false;
            StateHasChanged();
        }

        public async Task GetName()
        {
            if (string.IsNullOrEmpty(Unit.Content))
            {
                await DialogService.ShowMessageBox("No Content", "Please enter some content first!");
                return;
            }

            _processingName = true;
            StateHasChanged();

            Unit.Name = "";
            //generate question
            var promptSettings = GptPromptService.RenderGptPrompt(); //basics only
            var response = await svc.GetName(promptSettings, "UNIT CONTENT: " + Unit.Content);

            await foreach (var res in response)
            {

                Unit.Name += res;

                StateHasChanged();
            }

            _processingName = false;
            StateHasChanged();
        }
    }
}
