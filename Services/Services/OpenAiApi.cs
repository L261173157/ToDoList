using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using OpenAI.GPT3;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.ObjectModels.RequestModels;

namespace Services.Services;

public static class OpenAiApi
{
    public static async Task<string> Chat(string input)
    {
        var openAiService = new OpenAIService(new OpenAiOptions()
        {
            ApiKey = ConfigurationManager.AppSettings["openAiKey"]
        });
        var completionResult = await openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
        {
            Messages = new List<ChatMessage>
            {
                //ChatMessage.FromSystem("You are a helpful assistant."),
                //ChatMessage.FromUser("Who won the world series in 2020?"),
                //ChatMessage.FromAssistant("The Los Angeles Dodgers won the World Series in 2020."),
                //ChatMessage.FromUser("Where was it played?")
                ChatMessage.FromUser(input)
            },
            Model = Models.ChatGpt3_5Turbo,
            MaxTokens = 500//optional
        });
        if (completionResult.Successful)
        {
            //Console.WriteLine(completionResult.Choices.First().Message.Content);
            return completionResult.Choices.First().Message.Content;
        }
        else
        {
            //Console.WriteLine(completionResult.Error);
            return completionResult.Error.Code.ToString();
        }
    }
}

