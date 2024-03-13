using OpenAI;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

namespace Samples.Whisper
{
    public class WhisperNew : MonoBehaviour
    {
        // ChatGPT controleris
        [SerializeField] private ChatGPT chatGPT;
 
        //gaunama zinute 
        [SerializeField] private TextMeshProUGUI message;
 
        //ekrane parasomas transkribuotas tekstas
        [SerializeField] private TMP_Text outputField;

        //ChatGPt siuncimas tekstas
        private string inputGPT;
        private readonly string fileName = "output.wav";
        private readonly int duration = 5;

        private AudioClip clip;
     //   private bool isRecording;     
        private OpenAIApi openai = new OpenAIApi();

       
        private void Start()
        {
        #if UNITY_WEBGL && !UNITY_EDITOR
            // Klaidos pranesimas del WebGL
            outputField.text = "Mikrofonas neplaikomas WebGL";
        #else
            // Patikrina ar yra naudojamas mikrofonas
            if (Microphone.devices.Length == 0)
            {
                
                outputField.text = "Klaida: Nėra galimų mikrofonų";
            }
        #endif
        }

        private void ChangeMicrophone(int index)
        {
            PlayerPrefs.SetInt("user-mic-device-index", index);
        }

        private void StartRecording()
        {
               //     isRecording = true;

#if !UNITY_WEBGL
            string defaultMicrophone = Microphone.devices.Length > 0 ? Microphone.devices[0] : null;
            clip = Microphone.Start(defaultMicrophone, false, duration, 44100);
#endif
        }

        private async void EndRecording()
        {
            message.text = "Transkribuojama...";
#if !UNITY_WEBGL
            Microphone.End(null);
#endif

            byte[] data = SaveWav.Save(fileName, clip);

            var req = new CreateAudioTranscriptionsRequest
            {
                FileData = new FileData() { Data = data, Name = "audio.wav" },
                Model = "whisper-1",
                Language = "lt"
            };

            var res = await openai.CreateAudioTranscription(req);


            // gaunamas tekstas(from speech)
            message.text = res.Text; 
            inputGPT = res.Text;     
            chatGPT.SendReply(inputGPT);    
        }
        

        // tikrina ar paspaustas "v" mygtukas ir iraso garsa
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                StartRecording();
                 message.text = "Klausoma...";

            }

            if (Input.GetKeyUp(KeyCode.V))
            {
                EndRecording();
            }
        }
    }
}
