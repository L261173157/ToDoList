using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using OpenAI;
using OpenAI.Managers;
using OpenAI.ObjectModels;
using OpenAI.ObjectModels.RequestModels;

// using OpenAI.GPT3;
// using OpenAI.GPT3.Managers;
// using OpenAI.GPT3.ObjectModels;
// using OpenAI.GPT3.ObjectModels.RequestModels;

namespace Services.Services;

public static class OpenAiApi
{
    public static async Task<string> Chat(string userInput, string systemInput = "", string assistantInput = "",
        float temperature = 0.5f)
    {
        var openAiService = new OpenAIService(new OpenAiOptions
        {
            ApiKey = ConfigurationManager.AppSettings["openAiKey"]
        });

        var create = new ChatCompletionCreateRequest
        {
            Messages = new List<ChatMessage>
            {
                ChatMessage.FromSystem(systemInput ?? "不担任任何角色"), //optional
                ChatMessage.FromUser(userInput)
            },
            Model = Models.ChatGpt3_5Turbo,
            Temperature = temperature //optional
        };
        var completionResult = await openAiService.ChatCompletion.CreateCompletion(create);
        if (completionResult.Successful)
            return completionResult.Choices.First().Message.Content;
        return completionResult.Error.Code;
    }
}