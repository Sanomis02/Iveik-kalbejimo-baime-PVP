using System;
using System.IO;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
     //[SerializeField]private Animator animator;
    private AudioSource audioSource;
    private bool deleteCachedFile = true;
     private bool isPlayingAudio = false; // Flag to track if audio is currently playing
    
    private void OnEnable()
    {
        this.audioSource = GetComponent<AudioSource>();
    }

    public void ProcessAudioBytes(byte[] audioData)
    {
        string filePath = Path.Combine(Application.persistentDataPath, "audio.mp3");
        File.WriteAllBytes(filePath, audioData);

        StartCoroutine(LoadAndPlayAudio(filePath));
    }
    
    private IEnumerator LoadAndPlayAudio(string filePath)
    {
        using UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file://" + filePath, AudioType.MPEG);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            AudioClip audioClip = DownloadHandlerAudioClip.GetContent(www);
            audioSource.clip = audioClip;
            audioSource.Play();
            isPlayingAudio = true; // Set the flag to true when audio starts playing
        }
        else
        {
            Debug.LogError("Audio file loading error: " + www.error);
        }
        
        if (deleteCachedFile) File.Delete(filePath);
    }
    private void Update()
    {
        if (this.audioSource.isPlaying)
        {
            // Run command when audio finishes playing
            //animator.SetBool("Speak", true);
            isPlayingAudio = false; // Reset the flag
          //  Debug.Log("...");
        }
        else if (!this.audioSource.isPlaying);
        //animator.SetTrigger("Idle");
       // Debug.Log("idle");
    }

}