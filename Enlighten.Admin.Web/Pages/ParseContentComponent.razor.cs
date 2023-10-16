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
        public TextbookUnit Unit { get; set; }

        public string errorMessage = string.Empty;
        public bool _processing;
        private StudyContentService svc;

        protected override async Task OnInitializedAsync()
        {
            svc = new StudyContentService(GptClientSettingsModel);

        }

        public async Task LoadFile(IBrowserFile file)
        {
            _processing = true;
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

            _processing = false;

        }


        public async Task GetStructuredContent(string content)
        {
            Unit.Content = "";
            //generate question
            var promptSettings = GptPromptService.RenderGptPrompt(); //basics only
            var response = await svc.GetStructuredContent(promptSettings, "UNIT CONTENT: " + content);

            await foreach (var res in response)
            {

                Unit.Content += res;


                StateHasChanged();
            }
        }

        public async Task GetSummary()
        {
            Unit.Summary = "";
            //generate question
            var promptSettings = GptPromptService.RenderGptPrompt(); //basics only
            var response = await svc.GetSummary(promptSettings, "UNIT CONTENT: " + Unit.Content);

            await foreach (var res in response)
            {

                Unit.Summary += res;

                StateHasChanged();
            }
        }

        public async Task GetTopicList()
        {
            Unit.TopicList = "";
            //generate question
            var promptSettings = GptPromptService.RenderGptPrompt(); //basics only
            var response = await svc.GetTopicList(promptSettings, "UNIT CONTENT: " + Unit.Content);

            await foreach (var res in response)
            {

                Unit.TopicList += res;

                StateHasChanged();
            }
        }

        public async Task GetName()
        {
            Unit.Name = "";
            //generate question
            var promptSettings = GptPromptService.RenderGptPrompt(); //basics only
            var response = await svc.GetName(promptSettings, "UNIT CONTENT: " + Unit.Content);

            await foreach (var res in response)
            {

                Unit.Name += res;

                StateHasChanged();
            }
        }
    }
}
