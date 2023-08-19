using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enlighten.Data.Models;

namespace Enlighten.Core.Services
{
    public class GptPromptService
    {
        public class GptPromptRenderModel : BaseGpt
        {

        }

        public GptPromptRenderModel RenderGptPrompt(params BaseGpt[] gptPromptModels)
        {
            var modelsSorted = gptPromptModels.OrderBy(i => i.PromptPriority);
            var result = new GptPromptRenderModel();

            foreach (var newModel in modelsSorted)
            {
                if (!string.IsNullOrEmpty(newModel.QuizSystemMessage))
                {
                    result.QuizSystemMessage = newModel.QuizSystemMessage;
                }

                if (!string.IsNullOrEmpty(newModel.QuizQuestionPrompt))
                {
                    result.QuizQuestionPrompt = newModel.QuizQuestionPrompt;
                }

                if (!string.IsNullOrEmpty(newModel.QuizAnswerPrompt))
                {
                    result.QuizAnswerPrompt = newModel.QuizAnswerPrompt;
                }

                if (!string.IsNullOrEmpty(newModel.InquireSystemMessage))
                {
                    result.InquireSystemMessage = newModel.InquireSystemMessage;
                }

                if (!string.IsNullOrEmpty(newModel.InquirePrompt))
                {
                    result.InquirePrompt = newModel.InquirePrompt;
                }

                if (!string.IsNullOrEmpty(newModel.ContentStart))
                {
                    result.ContentStart = newModel.ContentStart;
                }

                if (!string.IsNullOrEmpty(newModel.ContentEnd))
                {
                    result.ContentEnd = newModel.ContentEnd;
                }
            }

            return result;
        }
    }
}
