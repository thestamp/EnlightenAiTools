using Enlighten.Core.Services;
using Enlighten.Data.Models;
using Enlighten.Gpt.Client.Configuration;
using Enlighten.Study.Core.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Enlighten.Admin.Web.Pages
{
    public class ParseContentComponentBase : ComponentBase
    {
        [Inject] public GptClientSettingsModel GptClientSettingsModel { get; set; }
        [Inject] public GptPromptService GptPromptService { get; set; }
        [Parameter]
        public string ResultContent { get; set; }
        [Parameter]
        public string ResultSummary { get; set; }
        [Parameter]
        public string ResultTopicList { get; set; }

  

        public string ViewContent = string.Empty;
        public string ViewSummary = string.Empty;
        public string ViewTopics = string.Empty;
        

        public string errorMessage = string.Empty;
        private bool _processing;
        private StudyContentService svc;

        protected override async Task OnInitializedAsync()
        {
            svc = new StudyContentService(GptClientSettingsModel);

        }

        public async Task LoadFile(InputFileChangeEventArgs e)
        {

            errorMessage = string.Empty;

            using var stream = new MemoryStream();
            await e.File.OpenReadStream().CopyToAsync(stream);

            //make it delimited
            stream.Position = 0;
            var contentStringUntrimmed = System.Text.Encoding.UTF8.GetString(stream.ToArray());
            var contentString = contentStringUntrimmed.Trim(new char[] { '\uFEFF', '\u200B' });//trim the BOM https://en.wikipedia.org/wiki/Byte_Order_Mark

            try
            {
                await GetStructuredContent(contentString);
                await GetSummary(ViewContent);
                await GetTopicList(ViewContent);

            }
            catch (Exception exception)
            {
                errorMessage = exception.Message + exception.StackTrace;
                throw;
            }

        }


        public async Task GetStructuredContent(string content)
        {
            ViewContent = "";
            //generate question
            var promptSettings = GptPromptService.RenderGptPrompt(); //basics only
            var response = await svc.GetStructuredContent(promptSettings, "UNIT CONTENT: " + content);

            await foreach (var res in response)
            {

                ViewContent += res;

                StateHasChanged();
            }
        }

        public async Task GetSummary(string content)
        {
            ViewSummary = "";
            //generate question
            var promptSettings = GptPromptService.RenderGptPrompt(); //basics only
            var response = await svc.GetSummary(promptSettings, "UNIT CONTENT: " + content);

            await foreach (var res in response)
            {

                ViewSummary += res;

                StateHasChanged();
            }
        }

        public async Task GetTopicList(string content)
        {
            ViewTopics = "";
            //generate question
            var promptSettings = GptPromptService.RenderGptPrompt(); //basics only
            var response = await svc.GetTopicList(promptSettings, "UNIT CONTENT: " + content);

            await foreach (var res in response)
            {

                ViewTopics += res;

                StateHasChanged();
            }
        }
    }
}
