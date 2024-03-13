using System.Text;
using UnityEngine;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using TMPro;
using UnityEngine.UI;
using System.IO; // Added for file operations
using UnityEngine.Networking;
using System;

// klase TTS duomenims 
[System.Serializable]
public class TTSPayload
{
    public string model;
    public string input;
    public string voice;
    public string response_format;
    public float speed;
}

public class OpenAIWrapper : MonoBehaviour
{
    // naudojamas audio playeris
    [SerializeField] private AudioPlayer audioPlayer;

    // Open Ai API raktas
    private string openAIKey = "";

    // kalbos modelis 
    private string model = "tts-1";  // tts-1-hd , geresne kokybe, ilgesnis atsako laikas

    // kalbos balsas
    private string voice = "alloy";  // alloy, echo, fable, onyx, nova, shimmer

    // kalbejimo greitis
    private float speed = 1f;   // range 0.00  -->  4.00

    // sukurimao audio formatas
    private readonly string outputFormat = "mp3";

    private async Task<string> ReadApiKeyFromJson()
    {
        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".openai", "auth.json");

        if (!File.Exists(path))
        {
            Debug.LogError("OpenAI auth.json file not found.");
            return null;
        }

        string jsonContent = File.ReadAllText(path);
        var data = JsonUtility.FromJson<OpenAIAuthData>(jsonContent);
        return data.api_key;
    }

    [System.Serializable]
    private class OpenAIAuthData
    {
        public string api_key;
    }

    private async Task<string> GetOpenAIKey()
    {
        if (string.IsNullOrEmpty(openAIKey))
        {
            openAIKey = await ReadApiKeyFromJson();
        }
        return openAIKey;
    }

    public async Task<byte[]> RequestTextToSpeech(string text)
    {
        Debug.Log("Sending new request to OpenAI TTS.");
        string apiKey = await GetOpenAIKey();
        if (string.IsNullOrEmpty(apiKey))
        {
            Debug.LogError("OpenAI API key is missing or invalid.");
            return null;
        }

        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        TTSPayload payload = new TTSPayload
        {
            model = this.model,
            input = text,
            voice = this.voice,
            response_format = this.outputFormat,
            speed = this.speed
        };

        string jsonPayload = JsonUtility.ToJson(payload);

        var httpResponse = await httpClient.PostAsync(
            "https://api.openai.com/v1/audio/speech",
            new StringContent(jsonPayload, Encoding.UTF8, "application/json"));

        byte[] response = await httpResponse.Content.ReadAsByteArrayAsync();

        if (httpResponse.IsSuccessStatusCode)
        {
            return response;
        }
        Debug.Log("Error: " + httpResponse.StatusCode);
        return null;
    }

    public async Task<byte[]> RequestTextToSpeech(string text, string model, string voice, float speed)
    {
        this.model = model;
        this.voice = voice;
        this.speed = speed;
        return await RequestTextToSpeech(text);
    }

    public async void SynthesizeAndPlay(string text)
    {
        Debug.Log("Trying to synthesize " + text);
        byte[] audioData = await RequestTextToSpeech(text, model, voice, speed);
        if (audioData != null)
        {
            Debug.Log("Playing audio.");
            audioPlayer.ProcessAudioBytes(audioData);
        }
        else
        {
            Debug.LogError("Failed to get audio data from OpenAI.");
        }
    }

    public void SynthesizeAndPlay(string text, string model, string voice, float speed)
    {
        this.model = model;
        this.voice = voice;
        this.speed = speed;
        SynthesizeAndPlay(text);
    }

    //  private void Update()
    // {
    //     // Check if the "T" key is pressed
    //     if (Input.GetKeyUp(KeyCode.T))
    //     {
    //         // Call the SynthesizeAndPlay method with your desired text
    //         SynthesizeAndPlay(inputTxt);
    //     }
    // }
}
