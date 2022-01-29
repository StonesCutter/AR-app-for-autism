//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//
// <code>
//
//
//
using UnityEngine;
using UnityEngine.UI;
using Microsoft.CognitiveServices.Speech;

public class HelloWorld : MonoBehaviour
{
    // Hook up the three properties below with a Text, InputField and Button object in your UI.
    public Text outputText;
    public InputField inputField;
    public Button speakButton;
    public AudioSource audioSource;
    private IntentRecognition IR;
    public int canary;
    public bool muteUnmute;
    public bool isStart = false;

    public GameObject RobotAngry;
    public GameObject RobotHappy;
    public GameObject RobotNormal;
    public GameObject RobotReward;

    public GameObject ButtonMicrophone;
    public bool CaOn = false;

    private object threadLocker = new object();
    private bool waitingForSpeak;
    private string message;
    private string messageConfront;
    public string textValue;
    private SpeechConfig speechConfig;
    private SpeechSynthesizer synthesizer;

    float timer = 0f; 
    public bool VolumeChosen = false;


    public void ButtonClick()
    {

        /* lock (threadLocker)
         {
             waitingForSpeak = true;
          } */

        string newMessage = string.Empty;

        // Starts speech synthesis, and returns after a single utterance is synthesized.
        using (var result = synthesizer.SpeakTextAsync(textValue).Result)
        {
            if (canary == 0)
            {
                audioSource.volume = 0.0f;
            }
            else if (canary == 1)
            {
                audioSource.volume = 0.25f;
            }
            else if (canary == 2)
            {
                audioSource.volume = 0.50f;
            }
            else if (canary == 3)
            {
                audioSource.volume = 0.75f;
            }
            else if (canary == 4)
            {
                audioSource.volume = 1.0f;
            }
            else
            {
                audioSource.volume = 0.0f;
            }

            if (muteUnmute == true)
            {
                audioSource.mute = true;
            }
            if (muteUnmute == false)
            {
                audioSource.mute = false;
            }

            // Checks result.
            // if (result.Reason == ResultReason.SynthesizingAudioCompleted)
            // {
            // Native playback is not supported on Unity yet (currently only supported on Windows/Linux Desktop).
            // Use the Unity API to play audio here as a short term solution.
            // Native playback support will be added in the future release.
            Debug.Log("RESULT --->" + result);
            var sampleCount = result.AudioData.Length / 2;
            //var sampleCount = textValue.Length;
            var audioData = new float[sampleCount];
            for (var i = 0; i < sampleCount; ++i)
            {
                audioData[i] = (short)(result.AudioData[i * 2 + 1] << 8 | result.AudioData[i * 2]) / 32768.0F;
            }

            // audioSource.volume = 0.75f;
            // The output audio format is 16K 16bit mono
            var audioClip = AudioClip.Create("SynthesizedAudio", sampleCount, 1, 16000, false);
            audioClip.SetData(audioData, 0);
            audioSource.clip = audioClip;
            audioSource.Play();

            newMessage = "Speech synthesis succeeded!";
            //}
            /*else if (result.Reason == ResultReason.Canceled)
            {
                var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
                newMessage = $"CANCELED:\nReason=[{cancellation.Reason}]\nErrorDetails=[{cancellation.ErrorDetails}]\nDid you update the subscription info?";
            }*/
        }

        /*  lock (threadLocker)
          {
              message = newMessage;
              waitingForSpeak = false;
          }*/
    }

    void Start()
    {

        RobotNormal.gameObject.SetActive(false);
        RobotAngry.gameObject.SetActive(false);
        RobotHappy.gameObject.SetActive(true);
        RobotReward.gameObject.SetActive(false);

        messageConfront = "aaa";
        IR = GameObject.FindObjectOfType<IntentRecognition>();
        /*if (outputText == null)
        {
            UnityEngine.Debug.LogError("outputText property is null! Assign a UI Text element to it.");
        }
        else if (inputField == null)
        {
            message = "inputField property is null! Assign a UI InputField element to it.";
            UnityEngine.Debug.LogError(message);
        }
        else if (speakButton == null)
        {
            message = "speakButton property is null! Assign a UI Button to it.";
            UnityEngine.Debug.LogError(message);
        }*/
        //else
        // {
        // Continue with normal initialization, Text, InputField and Button objects are present.
        // inputField.text = "Enter text you wish spoken here.";
        message = "Click button to synthesize speech";
        //speakButton.onClick.AddListener(ButtonClick);


        // Creates an instance of a speech config with specified subscription key and service region.
        // Replace with your own subscription key and service region (e.g., "westus").
        speechConfig = SpeechConfig.FromSubscription("2770648cd1214b979cfde95be9931e19", "westeurope");

        // The default format is Riff16Khz16BitMonoPcm.
        // We are playing the audio in memory as audio clip, which doesn't require riff header.
        // So we need to set the format to Raw16Khz16BitMonoPcm.
        speechConfig.SetSpeechSynthesisOutputFormat(SpeechSynthesisOutputFormat.Raw16Khz16BitMonoPcm);

        // Creates a speech synthesizer.
        // Make sure to dispose the synthesizer after use!
        synthesizer = new SpeechSynthesizer(speechConfig, null);
        //}
    }

    void Update()
    {

        timer += Time.deltaTime;
        if (VolumeChosen == false && timer > 3)
        {
            canary = 3;
        }
        //lock (threadLocker)
        //{
        textValue = IR.PassPhrase();
        //if (speakButton != null)
        //{
        // speakButton.interactable = !waitingForSpeak;
        //}

        //if (outputText != null)
        //{
        Debug.Log("MESSAGE --->" + textValue);
        // Debug.Log("CANARY --->" + canary);
        //   outputText.text = message;
        // IR = GameObject.FindObjectOfType<IntentRecognition>();
        //textValue = IR.PassPhrase();
        //}
        /* if (isStart = true)
         {
             textValue = "Hello, ARE YOU RETARDED OR WHAT?";
                 isStart = false;
         }*/
        if (messageConfront != textValue)
        {
            Debug.Log("SONO ENTRTO>");
            ButtonClick();
            messageConfront = textValue;
        }

        // }
    }

    void OnDestroy()
    {
        synthesizer.Dispose();
    }

    public void IsStarted()
    {
        isStart = true;
    }

    public void MuteSpeaker()
    {
        muteUnmute = true;
        //audioSource.mute = true;
    }

    public void UnMuteSpeaker()
    {
        muteUnmute = false;
        //audioSource.mute = false;
    }

    public void SetVolume0()
    {
        canary = 0;
    }

    public void SetVolume1()
    {
        canary = 1;
        VolumeChosen = true;
    }

    public void SetVolume2()
    {
        canary = 2; VolumeChosen = true;
    }

    public void SetVolume3()
    {
        canary = 3;
        VolumeChosen = true;
    }

    public void SetVolume4()
    {
        canary = 4;
        VolumeChosen = true;
    }

    public bool CaOnOff()
    {
        if (ButtonMicrophone.activeSelf)
        {
            CaOn = true;
        }
        else
        {
            CaOn = false;
        }
        return CaOn;
    }

}
// </code>