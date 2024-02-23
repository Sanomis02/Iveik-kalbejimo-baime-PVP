using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

namespace OpenAI
{
    public class ChatGPTController : MonoBehaviour
    {
        [SerializeField] private InputField inputField;
        [SerializeField] private TMP_Text outputField;

        private float height;
        private OpenAIApi openai = new OpenAIApi("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");

        private List<ChatMessage> messages = new List<ChatMessage>();
        private string prompt = "Act as a random stranger in a chat room and reply to the questions. Don't break character. Don't ever mention that you are an AI model.";

        private void Start()
        {
            if(Input.GetKeyDown(KeyCode.Return))
                SendReply();
        }

        void OnGUI()
        {
            if(Event.current.Equals(Event.KeyboardEvent("return")))
                SendReply();
        }

        private void AppendMessage(ChatMessage message)
        {
            outputField.text = message.Content;
            
        }

        private async void SendReply()
        {
            Debug.Log("patekta");

            var newMessage = new ChatMessage()
            {
                Role = "user",
                Content = inputField.text
            };
            
            //AppendMessage(newMessage);

            if (messages.Count == 0) newMessage.Content = prompt + "\n" + inputField.text; 
            
            messages.Add(newMessage);
            
            inputField.text = "";
            inputField.enabled = false;
            
            // Complete the instruction
            var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                Model = "gpt-4-0125-preview",
                Messages = messages
            });

            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                var message = completionResponse.Choices[0].Message;
                message.Content = message.Content.Trim();
                
                messages.Add(message);
                AppendMessage(message);
            }
            else
            {
                Debug.LogWarning("No text was generated from this prompt.");
            }

            inputField.enabled = true;
        }
    }
}
