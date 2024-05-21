using System.Collections;
using System.Collections.Generic;
using OpenAI;
using TMPro;
using UnityEngine;

public class ChatGPT : MonoBehaviour
{
    // text to speech irankio valdiklis
    [SerializeField] private OpenAIWrapper openAIWrapper;

    // ChatGPT atsakymo rezultatas
    [SerializeField] private TMP_Text outputField;
    
    // Open AI API raktas
    private OpenAIApi openai = new OpenAIApi();

    // Zinuciu listas
    private List<ChatMessage> messages = new List<ChatMessage>();

      //  private string prompt = "Act as a random stranger in a chat room and reply to the questions. Don't break character. Don't ever mention that you are an AI model.";
    private string prompt = "Act as a 10 years old child named Jonas communicating with other child. Do not ever speak any other language than Lithuanian. Do not tell answers that is more than 150 characters. Don't ever mention that you are an AI model.";
    
    // irasoma atsakymo zinute
    private void AppendMessage(ChatMessage message)
        {
            outputField.text = message.Content;          
        }


        public async void SendReply(string inputGPT)
        {
            Debug.Log("patekta");

            var newMessage = new ChatMessage()
            {
                Role = "user",
                Content = inputGPT
            };
            
            //AppendMessage(newMessage);

            if (messages.Count == 0) newMessage.Content = prompt + "\n" + inputGPT; 
            
            messages.Add(newMessage);
            
            inputGPT = "...................................................................";
           // inputField.enabled = false;
            
            // Complete the instruction
            var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                Model = "gpt-4-turbo",
                Messages = messages
            });

            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                var message = completionResponse.Choices[0].Message;
                message.Content = message.Content.Trim();
                
                messages.Add(message);
                AppendMessage(message);
                openAIWrapper.SynthesizeAndPlay(message.Content);
            }
            else
            {
                Debug.LogWarning("No text was generated from this prompt.");
            }
        }

}

