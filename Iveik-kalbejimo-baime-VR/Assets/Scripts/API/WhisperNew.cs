 using OpenAI;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.InputSystem;

namespace Samples.Whisper
{
    public class WhisperNew : MonoBehaviour
    {
        // ChatGPT controleris
        [SerializeField] private ChatGPT chatGPT;
 
        //gaunama zinute 
    //    [SerializeField] private TextMeshProUGUI message;
 
        //ekrane parasomas transkribuotas tekstas
      //  [SerializeField] private TMP_Text outputField;

        //ChatGPt siuncimas tekstas
        private string inputGPT;
        private readonly string fileName = "output.wav";
        private readonly int duration = 5;

        private AudioClip clip;
         //   private bool isRecording;     
        private OpenAIApi openai = new OpenAIApi();

        private InputAction paspaudimas;

        [SerializeField]
        private InputActionAsset asset;
       
        private void Start()
        {
            // Patikrina ar yra naudojamas mikrofonas
            if (Microphone.devices.Length == 0)
            {               
               Debug.LogWarning ( "Klaida: Nėra galimų mikrofonų");
            }



            InputActionMap map = asset.FindActionMap("XRI LeftHand Interaction", false);
            if (map == null)
                Debug.Log("Nerado map");

            paspaudimas = map.FindAction("Record");
            if (paspaudimas == null)
                Debug.Log("Nera veiksmo");



        }

      //  private void ChangeMicrophone(int index)
      //  {
     //       PlayerPrefs.SetInt("user-mic-device-index", index);
      //  }

        private void StartRecording()
        {
               //     isRecording = true;

            string defaultMicrophone = Microphone.devices.Length > 0 ? Microphone.devices[0] : null;
            clip = Microphone.Start(defaultMicrophone, false, duration, 44100);
        }

        private async void EndRecording()
        {
        //    message.text = "Transkribuojama...";

            byte[] data = SaveWav.Save(fileName, clip);

            var req = new CreateAudioTranscriptionsRequest
            {
                FileData = new FileData() { Data = data, Name = "audio.wav" },
                Model = "whisper-1",
                Language = "lt"
            };

            var res = await openai.CreateAudioTranscription(req);

            // gaunamas tekstas(from speech)
        Debug.Log( res.Text); 
            inputGPT = res.Text;     
            chatGPT.SendReply(inputGPT);    
        }
        
        // tikrina ar paspaustas "v" mygtukas ir iraso garsa
        private void Update()
        {
            paspaudimas.started+=_=> StartRecording();
         
            paspaudimas.canceled +=_=>   EndRecording();
        }
    }

}
