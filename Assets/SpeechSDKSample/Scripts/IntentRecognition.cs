//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//
using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech.Intent;
using UnityEngine;
using UnityEngine.UI;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif
using IntentRecognitionResults;
using TinyJson;
/// <summary>
/// The IntentRecognition class lets the user dictate voice commands via speech recognition and
/// Natural Language Understanding (NLU). Once captured, the voice command are interpreted based
/// on intents and entities returned by the LUIS service, and then executed against the various
/// shapes in the scene.
/// </summary>
public class IntentRecognition : MonoBehaviour {

    private TestSpeech TS;
    public bool stopUpdate;
    // Public fields in the Unity inspector
    [Tooltip("Unity UI Text component used to report potential errors on screen.")]
    public Text RecognizedText;
    [Tooltip("Unity UI Text component used to post recognition results on screen.")]
    public Text ErrorText;

    // Speech recognition key, not required when the IntentRecognizer is used
    [Tooltip("Azure API key for Cognitive Services Speech.")]
    public string SpeechServiceAPIKey = string.Empty;
    [Tooltip("Region for your Cognitive Services Speech instance (must match the key).")]
    public string SpeechServiceRegion = string.Empty;

    // LUIS AppId and service key
    [Tooltip("AppId for your LUIS model.")]
    public string LUISAppId = string.Empty;
    [Tooltip("Azure API key for your LUIS service.")]
    public string LUISAppKey = string.Empty;
    [Tooltip("Region for your LUIS service (must match the key).")]
    public string LUISRegion = string.Empty;

    // Variables for understanding in which part of the flow we are right now  ///!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    public string actualTask = "intro";
    float timer = 0f;
    float rewardTimer = 0f;
    public string phraseToSay;
    public string helpText;
    public string phraseConfront = "aaa";
    public int counter;
    public int stepAutoConfirm = 0;
    public bool tardFlag = false;
    public bool accendi = false;
    private PageHelpScript PHS;
    public bool Finish = false;

    /// <summary>
    /// //////////////////

        float timerPanelScompare = 0f;
        public GameObject PanelScompare;

    /// 
     bool passa = true;
    bool passa1 = true;
    bool passa2 = true;
    bool passa3 = true;
    bool passa4 = true;
    bool passa5 = true;
    bool passa6 = true;
    bool passa7 = true;
    bool passa8 = true;
    bool passa9 = true;
    bool passa10 = true;
    bool passa11= true;
    bool passa12= true;
    bool passa13= true;
    bool passa14= true;
    bool passa15 = true;
    bool passa16 = true;
    bool passa17= true;
    bool passa18= true;
    bool passa19 = true;
    bool passa20 = true;
    bool passa21 = true;
    bool passa22 = true;
    bool passa23 = true;
    bool passa24 = true;
    bool passa25 = true;

    /// </summary>
    //AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA

    public GameObject RobotAngry;
    public GameObject RobotHappy;
    public GameObject RobotNormal;
    public GameObject RobotNormal1;
    public GameObject RobotReward;
    public GameObject pageFinish;
    public GameObject panel;
    public GameObject panelUI;

    // -------------- Importing from all other scripts ------------------------\\
    public string textValue;
    public Text textElement;
    public bool itsOn = false;


    public metroSignScript MSS;

    private ScriptEntrata SE;

    public bool Card = true;

    private ShowImages MIMG;

    private ticketMachineScript IMT0;

    private ScriptImageTarget1 IMT1;

    private ScriptImageTarget2 IMT2;

    private ScriptImageTarget3 IMT3;

    private ScriptImageTarget4 IMT4;

    private ScriptImageTarget5_Cash IMT5_Cash;

    private ScriptImageTarget5_Card IMT5_Card;

    private ScriptImageTarget6 IMT6;

    private ScriptImageTarget7 IMT7;

    private ScriptImageTargetTicket IMTT;

    private turnstilesScript TSs;

    private DiscesaScaleMobiliScript DSM;

    private Binario1_Bignami2 B1B2;

    private ScriptBinario1_Bignami B1B;

    private ScriptBinario2_SanSiro B2SS;
    //se si va verso bignami come direzione la variabile versoBignami � true, se si va verso San Siro � false
    public bool versoBignami = true;

    private PortaExtMetroScript PEM;

    private PortaIntMetroScript PIM;

    private UscitaScript USC;

    private turnstilesExitScript TSE;

    private UscitaFinaleScript UFS;

    private metroSignExit MSE;

    private HelloWorld HELL;


    //-------------------------------------------------------------------------------------------------------\\

    // Used to show live messages on screen, must be locked to avoid threading deadlocks since
    // the recognition events are raised in a separate thread
    private string recognizedString = "";
    private string errorString = "";
    private System.Object threadLocker = new System.Object();

    // Cognitive Services Speech objects used for Intent Recognition
    private IntentRecognizer intentreco;
    private IntentResult intent = null;
    private bool isIntentReady = false;

    private bool micPermissionGranted = false;
#if PLATFORM_ANDROID
    // Required to manifest microphone permission, cf.
    // https://docs.unity3d.com/Manual/android-manifest.html
    private Microphone mic;
#endif

    private void Awake()
    {
        // IMPORTANT INFO BEFORE YOU CAN USE THIS SAMPLE:
        // Get your own Cognitive Services LUIS subscription key for free by following the
        // instructions under the section titled 'Get LUIS key' in the article found at
        // https://docs.microsoft.com/azure/cognitive-services/luis/luis-get-started-cs-get-intent.
        // Use the inspector fields to manually set these values with your subscription info.
        // If you prefer to manually set your LUIS AppId, Key and Region in code,
        // then uncomment the three lines below and set the values to your own.
        LUISAppId = "57a4fdc6-b228-437f-a59e-9c493fc48ee1";
        LUISAppKey = "092b8d29de294b6b9b29c26f50723356";
        LUISRegion = "westeurope";
/*                LUISAppId = "7b067b7f-39b0-4d42-ba5b-ca770a73ad6a";
                LUISAppKey = "b0c7e37ef0a94996a8a00a4523a81395";
                LUISRegion = "westeurope";
*/
    }

    // Use this for initialization
    void Start() {

#if PLATFORM_ANDROID
        // Request to use the microphone, cf.
        // https://docs.unity3d.com/Manual/android-RequestingPermissions.html
        recognizedString = "Waiting for microphone permission...";
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }
#else
        micPermissionGranted = true;
        //StartContinuous();

        RobotNormal.gameObject.SetActive(false);
        RobotAngry.gameObject.SetActive(false);
        RobotHappy.gameObject.SetActive(true);
        RobotReward.gameObject.SetActive(false);


#endif
    }

    /// <summary>
    /// Attach to button component used to launch continuous intent recognition
    /// </summary>
    public void StartContinuous()
    {
        //if (micPermissionGranted)
        //{

         itsOn = true;
        StartContinuousIntentRecognition();
        //}
        //else
        // {
        //    recognizedString = "This app cannot function without access to the microphone.";
        // }
    }

    /// <summary>
    /// Creates and initializes the IntentRecognizer
    /// </summary>
    void CreateIntentRecognizer()
    {
        /*if (LUISAppKey.Length == 0 || LUISAppKey != "f8687397678a4a5a85b01f97dd969d47")
        {
            recognizedString = "You forgot to obtain Cognitive Services LUIS credentials and inserting them in this app." + Environment.NewLine +
                               "See the README file and/or the instructions in the Awake() function for more info before proceeding.";
            errorString = "ERROR: Missing service credentials";
            UnityEngine.Debug.LogFormat(errorString);
            return;
        }*/
        UnityEngine.Debug.LogFormat("Creating Intent Recognizer.");
        recognizedString = "Initializing intent recognition, please wait...";

        if (intentreco == null)
        {
            // Creates an instance of a speech config with specified subscription key
            // and service region. Note that in contrast to other services supported by
            // the Cognitive Services Speech SDK, the Language Understanding service
            // requires a specific subscription key from https://www.luis.ai/.
            // The Language Understanding service calls the required key 'endpoint key'.
            // Once you've obtained it, replace with below with your own Language Understanding subscription key
            // and service region (e.g., "westus").
            // The default language is "en-us".
            var config = SpeechConfig.FromSubscription(LUISAppKey, LUISRegion);
            // Creates an intent recognizer using microphone as audio input.
            intentreco = new IntentRecognizer(config);

            // Creates a Language Understanding model using the app id, and adds specific intents from your model
            /*var model = LanguageUnderstandingModel.FromAppId(LUISAppId);
            intentreco.AddIntent(model, "ChangeColor", "color");
            intentreco.AddIntent(model, "Transform", "transform");
            intentreco.AddIntent(model, "Help", "help");
            intentreco.AddIntent(model, "None", "none");*/

            var model = LanguageUnderstandingModel.FromAppId(LUISAppId);
            intentreco.AddIntent(model, "Affermative", "affermative");
            intentreco.AddIntent(model, "Negative", "negative");
            intentreco.AddIntent(model, "Help", "help");
            intentreco.AddIntent(model, "Ticket", "ticket");
            intentreco.AddIntent(model, "Turnstile", "turnstile");
            intentreco.AddIntent(model, "Metro", "metro");
            intentreco.AddIntent(model, "Exit", "exit");
            intentreco.AddIntent(model, "None", "none");

            // Subscribes to speech events.
            intentreco.Recognizing += RecognizingHandler;
            intentreco.Recognized += RecognizedHandler;
            intentreco.SpeechStartDetected += SpeechStartDetectedHandler;
            intentreco.SpeechEndDetected += SpeechEndDetectedHandler;
            intentreco.Canceled += CanceledHandler;
            intentreco.SessionStarted += SessionStartedHandler;
            intentreco.SessionStopped += SessionStoppedHandler;
        }
        UnityEngine.Debug.LogFormat("CreateIntentRecognizer exit");
    }

    /// <summary>
    /// Starts the IntentRecognizer which will remain active until stopped
    /// </summary>
    private async void StartContinuousIntentRecognition()
    {
        if (LUISAppId.Length == 0 || LUISAppKey.Length == 0 || LUISRegion.Length == 0)
        {
            errorString = "One or more LUIS subscription parameters are missing. Check your values and try again.";
            return;
        }

        
            UnityEngine.Debug.LogFormat("Starting Continuous Intent Recognition.");
        CreateIntentRecognizer();

        if (intentreco != null)
        {
            UnityEngine.Debug.LogFormat("Starting Intent Recognizer.");

            // Starts continuous intent recognition.
            await intentreco.StartContinuousRecognitionAsync().ConfigureAwait(false);

            recognizedString = "Intent Recognizer is now running.";
            UnityEngine.Debug.LogFormat("Intent Recognizer is now running.");
        }
        UnityEngine.Debug.LogFormat("Start Continuous Intent Recognition exit");

        
    }

    #region Intent Recognition Event Handlers
    private void SessionStartedHandler(object sender, SessionEventArgs e)
    {
        UnityEngine.Debug.LogFormat($"\n    Session started event. Event: {e.ToString()}.");
    }

    private void SessionStoppedHandler(object sender, SessionEventArgs e)
    {
        UnityEngine.Debug.LogFormat($"\n    Session event. Event: {e.ToString()}.");
        UnityEngine.Debug.LogFormat($"Session Stop detected. Stop the recognition.");
    }

    private void SpeechStartDetectedHandler(object sender, RecognitionEventArgs e)
    {
        UnityEngine.Debug.LogFormat($"SpeechStartDetected received: offset: {e.Offset}.");
    }

    private void SpeechEndDetectedHandler(object sender, RecognitionEventArgs e)
    {
        UnityEngine.Debug.LogFormat($"SpeechEndDetected received: offset: {e.Offset}.");
        UnityEngine.Debug.LogFormat($"Speech end detected.");
    }

    private void RecognizingHandler(object sender, IntentRecognitionEventArgs e)
    {
        if (e.Result.Reason == ResultReason.RecognizingSpeech)
        {
            UnityEngine.Debug.LogFormat($"HYPOTHESIS: Text={e.Result.Text}");
            lock (threadLocker)
            {
                recognizedString = $"HYPOTHESIS: {Environment.NewLine}{e.Result.Text}";
            }
        }
    }

    private void RecognizedHandler(object sender, IntentRecognitionEventArgs e)
    {
        if (e.Result.Reason == ResultReason.RecognizedIntent)
        {
            //Console.WriteLine($"    Language Understanding JSON: {e.Result.Properties.GetProperty(PropertyId.LanguageUnderstandingServiceResponse_JsonResult)}.");
            UnityEngine.Debug.LogFormat($"RECOGNIZED: Intent={e.Result.IntentId} Text={e.Result.Text}");
            lock (threadLocker)
            {
                recognizedString = $"RESULT: Intent={e.Result.IntentId}";
                string json = e.Result.Properties.GetProperty(PropertyId.LanguageUnderstandingServiceResponse_JsonResult);
                var result = json.FromJson<IntentResult>();
                if (result != null)
                {
                    recognizedString += $" [Confidence={result.topScoringIntent.score.ToString("0.000")}]";
                    if (result.entities.Count > 0)
                    {
                        recognizedString += $"{Environment.NewLine}Entities=";
                        for (int i = 0; i < result.entities.Count; i++)
                        {
                            recognizedString += $"[{result.entities[i].type}: {result.entities[i].entity}] ";
                        }
                    }
                    lock (threadLocker)
                    {
                        intent = result;
                        isIntentReady = true;
                    }
                }
                recognizedString += $"{Environment.NewLine}{e.Result.Text}";
            }
        }
        if (e.Result.Reason == ResultReason.RecognizedSpeech)
        {
            UnityEngine.Debug.LogFormat($"RECOGNIZED: Text={e.Result.Text}");
            lock (threadLocker)
            {
                recognizedString = $"RESULT: {Environment.NewLine}{e.Result.Text}";
            }
        }
        else if (e.Result.Reason == ResultReason.NoMatch)
        {
            UnityEngine.Debug.LogFormat($"NOMATCH: Speech could not be recognized.");
        }
    }

    private void CanceledHandler(object sender, IntentRecognitionCanceledEventArgs e)
    {
        UnityEngine.Debug.LogFormat($"CANCELED: Reason={e.Reason}");

        errorString = e.ToString();
        if (e.Reason == CancellationReason.Error)
        {
            UnityEngine.Debug.LogFormat($"CANCELED: ErrorDetails={e.ErrorDetails}");
            UnityEngine.Debug.LogFormat($"CANCELED: Did you update the subscription info?");
        }
    }
    #endregion

    // Update is called once per frame
    void Update() {

        MIMG = GameObject.FindObjectOfType<ShowImages>();
        HELL = GameObject.FindObjectOfType<HelloWorld>();


        if (itsOn == true)
        {
#if PLATFORM_ANDROID
        if (!micPermissionGranted && Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            micPermissionGranted = true;
        }
#endif


            // Used to update results on screen during updates
            timer += Time.deltaTime;
            rewardTimer += Time.deltaTime;
            lock (threadLocker)
            {
                RecognizedText.text = recognizedString;
                ErrorText.text = errorString;

                AutoPassNextTask();

                if (isIntentReady && intent != null)
                {
                    ProcessIntent(intent);
                    isIntentReady = false;
                    intent = null;
                    
                }
                //Debug.Log("TIMER ------>" + timer);

                // IN CASO DI SILENZIO ------------ SSSSSSSSSSSSHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH ------------------------------------
                if (timer > 15 && Finish==false) //&& phraseToSay!=phraseConfront)
                {
                    switch (actualTask)
                    {
                        case "intro":
                            phraseToSay = "I am your personal assistant, I'm going to guide you through this experience. If you need assistance just say 'Help'. Now frame the metro sign with your phone. Can you see it?";
                            actualTask = "metroSign";
                            timer = 0;

                            RobotNormal.gameObject.SetActive(true);
                            RobotAngry.gameObject.SetActive(false);
                            RobotHappy.gameObject.SetActive(false);
                            RobotReward.gameObject.SetActive(false);

                            break;

                        case "metroSign":

                            if (counter == 0) {
                                phraseToSay = "Try to focus, you have to find the red metro sign and target it with your phone. Did you do it?";
                                counter++;
                                timer = 0;
                                MIMG.buttonMetroSignTrue();
                                tardFlag = false;

                                RobotNormal.gameObject.SetActive(true);
                                RobotAngry.gameObject.SetActive(false);
                                RobotHappy.gameObject.SetActive(false);
                                RobotReward.gameObject.SetActive(false);

                            }
                            else if (counter == 1)
                            {

                                phraseToSay = "Look at me one second, you have to find this red sign. Look around you carefully and find it.";
                                timer = 0;
                                MIMG.buttonMetroSignTrue();
                                Handheld.Vibrate();
                                counter++;

                                RobotNormal.gameObject.SetActive(false);
                                RobotAngry.gameObject.SetActive(true);
                                RobotHappy.gameObject.SetActive(false);
                                RobotReward.gameObject.SetActive(false);
                                //MIMG.PanelScomparee();

                            }
                            else if (counter == 2)
                            {
                                //stepAutoConfirm = 0;
                                phraseToSay = "Did someone help you to pass the Metro sign ?";
                                tardFlag = true;

                                RobotNormal.gameObject.SetActive(true);
                                RobotAngry.gameObject.SetActive(false);
                                RobotHappy.gameObject.SetActive(false);
                                RobotReward.gameObject.SetActive(false);
                            }
                            break;


                        case "entranceSign":
                            if (counter == 0)
                            {
                                phraseToSay = "You have to find the stairs where there's written the station's name.";
                                counter++;
                                timer = 0;

                               // MIMG.PanelAppare();

                                RobotNormal.gameObject.SetActive(true);
                                RobotAngry.gameObject.SetActive(false);
                                RobotHappy.gameObject.SetActive(false);
                                RobotReward.gameObject.SetActive(false);
                            }
                            else if (counter > 0)
                            {
                                phraseToSay = "Take your time. Find the stairs and go down.";
                                timer = 0;
                                Handheld.Vibrate();
                                RobotNormal.gameObject.SetActive(false);
                                RobotAngry.gameObject.SetActive(true);
                                RobotHappy.gameObject.SetActive(false);
                                RobotReward.gameObject.SetActive(false);
                            }
                            break;

                        case "findTicketMachine":
                            if (counter == 0)
                            {
                                phraseToSay = "Try to focus, you have to find the ticket machine and target it with your phone. Did you do it?";
                                counter++;
                                timer = 0;
                                tardFlag = false;

                                RobotNormal.gameObject.SetActive(true);
                                RobotAngry.gameObject.SetActive(false);
                                RobotHappy.gameObject.SetActive(false);
                                RobotReward.gameObject.SetActive(false);
                            }
                            else if (counter == 1)
                            {
                                MIMG.buttonTicketMachineTrue();
                                phraseToSay = "Look at me one second, you have to find this ticket machine. Look around you carefully and find it.";
                                timer = 0;
                                counter++;
                                Handheld.Vibrate();

                                RobotNormal.gameObject.SetActive(false);
                                RobotAngry.gameObject.SetActive(true);
                                RobotHappy.gameObject.SetActive(false);
                                RobotReward.gameObject.SetActive(false);

                            }
                            else if (counter == 2)
                            {
                                //stepAutoConfirm = 2;
                                phraseToSay = "Did someone already show you where is the ticket machine?";
                                tardFlag = true;

                                RobotNormal.gameObject.SetActive(true);
                                RobotAngry.gameObject.SetActive(false);
                                RobotHappy.gameObject.SetActive(false);
                                RobotReward.gameObject.SetActive(false);
                            }
                            break;

                        case "ticketCheck":
                            if (counter == 0)
                            {
                                MIMG.buttonTicketTrue();
                                phraseToSay = "Take out your ticket and target it  with the camera of your phone.";
                                counter++;
                                timer = 0;

                                RobotNormal.gameObject.SetActive(true);
                                RobotAngry.gameObject.SetActive(false);
                                RobotHappy.gameObject.SetActive(false);
                                RobotReward.gameObject.SetActive(false);
                            }
                            else if (counter > 0)
                            {
                                MIMG.buttonTicketTrue();
                                phraseToSay = "Look at me, you have to target this side of the ticket";
                                timer = 0;
                                Handheld.Vibrate();

                                RobotNormal.gameObject.SetActive(false);
                                RobotAngry.gameObject.SetActive(true);
                                RobotHappy.gameObject.SetActive(false);
                                RobotReward.gameObject.SetActive(false);
                            }
                            break;

                        case "findTurnstile":
                            if (counter == 0)
                            {
                                MIMG.buttonTurnstileTrue();
                                phraseToSay = "Try to focus, you have to find the turnstile and target it with your phone. Did you do it?";
                                counter++;
                                timer = 0;
                                tardFlag = false;

                                RobotNormal.gameObject.SetActive(true);
                                RobotAngry.gameObject.SetActive(false);
                                RobotHappy.gameObject.SetActive(false);
                                RobotReward.gameObject.SetActive(false);
                            }
                            else if (counter == 1)
                            {
                                MIMG.buttonTurnstileTrue();
                                phraseToSay = "Look at me one second, you have to find this turnstile. Look around you carefully and find it.";
                                timer = 0;
                                counter++;
                                Handheld.Vibrate();

                                RobotNormal.gameObject.SetActive(false);
                                RobotAngry.gameObject.SetActive(true);
                                RobotHappy.gameObject.SetActive(false);
                                RobotReward.gameObject.SetActive(false);
                            }
                            else if (counter == 2)
                            {
                                //stepAutoConfirm = 0;
                                phraseToSay = "Did someone help you to use the turnstile ?";
                                tardFlag = true;

                                RobotNormal.gameObject.SetActive(true);
                                RobotAngry.gameObject.SetActive(false);
                                RobotHappy.gameObject.SetActive(false);
                                RobotReward.gameObject.SetActive(false);
                            }
                            break;

                        case "autoStairs":
                            if (counter == 0)
                            {
                                phraseToSay = "You have to use the ticket following the arrows and then find the stairs and follow the arrow hanged above in order to reach the correct train, did you do it?";
                                counter++;
                                timer = 0;
                                tardFlag = false;

                                RobotNormal.gameObject.SetActive(true);
                                RobotAngry.gameObject.SetActive(false);
                                RobotHappy.gameObject.SetActive(false);
                                RobotReward.gameObject.SetActive(false);
                            }
                            else if (counter == 1)
                            {
                                phraseToSay = "Take your time. Insert the ticket, then find the stairs and go down.";
                                timer = 0;
                                counter++;
                                Handheld.Vibrate();

                                RobotNormal.gameObject.SetActive(false);
                                RobotAngry.gameObject.SetActive(true);
                                RobotHappy.gameObject.SetActive(false);
                                RobotReward.gameObject.SetActive(false);
                            }
                            else if (counter == 2)
                            {
                                //stepAutoConfirm = 0;
                                phraseToSay = "Did someone help you to find the automatic stairs ?";
                                tardFlag = true;

                                RobotNormal.gameObject.SetActive(true);
                                RobotAngry.gameObject.SetActive(false);
                                RobotHappy.gameObject.SetActive(false);
                                RobotReward.gameObject.SetActive(false);
                            }
                            break;

                        case "versoBignami":
                            if (counter == 0)
                            {
                                phraseToSay = "You have to find the arrows in order to reach the metro. Look around, they are hanged on the walls. Once you reach the metro, target th external doors.";
                                counter++;
                                timer = 0;

                                RobotNormal.gameObject.SetActive(true);
                                RobotAngry.gameObject.SetActive(false);
                                RobotHappy.gameObject.SetActive(false);
                                RobotReward.gameObject.SetActive(false);
                            }
                            else if (counter > 0)
                            {
                                phraseToSay = "Just take your time, relax and focus. Find the arrows and follow them. Then remember to target the doors of the metro";
                                timer = 0;
                                Handheld.Vibrate();

                                RobotNormal.gameObject.SetActive(false);
                                RobotAngry.gameObject.SetActive(true);
                                RobotHappy.gameObject.SetActive(false);
                                RobotReward.gameObject.SetActive(false);
                            }
                            break;

                        case "metroDoor":
                            if (counter == 0)
                            {
                                MIMG.buttonOutMetroTrue();
                                phraseToSay = "Once you are in front of the metro doors, target the external cartel sticked on them";
                                counter++;
                                timer = 0;

                                RobotNormal.gameObject.SetActive(true);
                                RobotAngry.gameObject.SetActive(false);
                                RobotHappy.gameObject.SetActive(false);
                                RobotReward.gameObject.SetActive(false);
                            }
                            else if (counter > 0)
                            {
                                MIMG.buttonOutMetroTrue();
                                phraseToSay = "Look at me, you have to target this cartel outside the doors of the metro";
                                timer = 0;
                                Handheld.Vibrate();

                                RobotNormal.gameObject.SetActive(false);
                                RobotAngry.gameObject.SetActive(true);
                                RobotHappy.gameObject.SetActive(false);
                                RobotReward.gameObject.SetActive(false);
                            }
                            break;

                        case "metroInDoor":
                            if (counter == 0)
                            {
                                MIMG.buttonInMetroTrue();
                                phraseToSay = "You have to target the cartel sticked on the inside of the door";
                                counter++;
                                timer = 0;

                                RobotNormal.gameObject.SetActive(true);
                                RobotAngry.gameObject.SetActive(false);
                                RobotHappy.gameObject.SetActive(false);
                                RobotReward.gameObject.SetActive(false);
                            }
                            else if (counter > 0)
                            {
                                MIMG.buttonInMetroTrue();
                                phraseToSay = "Look at me, you have to target this cartel indide the doors of the metro";
                                timer = 0;
                                Handheld.Vibrate();

                                RobotNormal.gameObject.SetActive(false);
                                RobotAngry.gameObject.SetActive(true);
                                RobotHappy.gameObject.SetActive(false);
                                RobotReward.gameObject.SetActive(false);
                            }
                            break;

                        case "goOut":
                            if (counter == 0)
                            {
                                phraseToSay = "Once you exit at the right stop, follow the arrows and signs hanged on walls and ceiling";
                                counter++;
                                timer = 0;

                                RobotNormal.gameObject.SetActive(true);
                                RobotAngry.gameObject.SetActive(false);
                                RobotHappy.gameObject.SetActive(false);
                                RobotReward.gameObject.SetActive(false);
                            }
                            else if (counter > 0)
                            {
                                phraseToSay = "Take your time, find the signs and climb the stairs";
                                timer = 0;
                                Handheld.Vibrate();

                                RobotNormal.gameObject.SetActive(false);
                                RobotAngry.gameObject.SetActive(true);
                                RobotHappy.gameObject.SetActive(false);
                                RobotReward.gameObject.SetActive(false);
                            }
                            break;

                        case "findTurnstileExit":
                            if (counter == 0)
                            {
                                MIMG.buttonTurnstileTrue();
                                phraseToSay = "You have to find, like you did before, the turnstiles to exit the metro";
                                counter++;
                                timer = 0;

                                RobotNormal.gameObject.SetActive(true);
                                RobotAngry.gameObject.SetActive(false);
                                RobotHappy.gameObject.SetActive(false);
                                RobotReward.gameObject.SetActive(false);
                            }
                            else if (counter == 1)
                            {
                                MIMG.buttonTurnstileTrue();
                                phraseToSay = "Look at me, you have to find this turnstile and frame it with the phone";
                                timer = 0;
                                counter++;
                                Handheld.Vibrate();

                                RobotNormal.gameObject.SetActive(false);
                                RobotAngry.gameObject.SetActive(true);
                                RobotHappy.gameObject.SetActive(false);
                                RobotReward.gameObject.SetActive(false);
                            }
                            else if (counter == 2)
                            {

                                phraseToSay = "Did someone help you to use the turnstile ?";
                                tardFlag = true;

                                RobotNormal.gameObject.SetActive(true);
                                RobotAngry.gameObject.SetActive(false);
                                RobotHappy.gameObject.SetActive(false);
                                RobotReward.gameObject.SetActive(false);
                            }
                            break;

                        case "finalExit":
                            if (counter == 0)
                            {
                                phraseToSay = "You have to find, like you did before, the stairs to exit the metro following the directions";
                                counter++;
                                timer = 0;

                                RobotNormal.gameObject.SetActive(true);
                                RobotAngry.gameObject.SetActive(false);
                                RobotHappy.gameObject.SetActive(false);
                                RobotReward.gameObject.SetActive(false);
                            }
                            else if (counter > 0)
                            {
                                phraseToSay = "Take your time, follow the cartels hanged on the walls and find the stairs to exit";
                                timer = 0;
                                Handheld.Vibrate();

                                RobotNormal.gameObject.SetActive(false);
                                RobotAngry.gameObject.SetActive(true);
                                RobotHappy.gameObject.SetActive(false);
                                RobotReward.gameObject.SetActive(false);
                            }
                            break;

                        case "endGame":
                            if (counter == 0 && Finish!=true)
                            {
                                MIMG.buttonMetroSignTrue();
                                phraseToSay = "You have to find, like you did before, the red metro sign outside the station";
                                counter++;
                                timer = 0;

                                RobotNormal.gameObject.SetActive(true);
                                RobotAngry.gameObject.SetActive(false);
                                RobotHappy.gameObject.SetActive(false);
                                RobotReward.gameObject.SetActive(false);
                            }
                            else if (counter > 0 && Finish != true)
                            {
                                MIMG.buttonMetroSignTrue();
                                phraseToSay = "If you don't remember what looked like, look at me right now. Find this cartel";
                                timer = 0;
                                Handheld.Vibrate();

                                RobotNormal.gameObject.SetActive(false);
                                RobotAngry.gameObject.SetActive(true);
                                RobotHappy.gameObject.SetActive(false);
                                RobotReward.gameObject.SetActive(false);
                            }
                            break;

                        default:
                            break;
                    }
                }
            }
        }
    }

    public void OnDisable()
    {
        StopIntentRecognition();
    }

    /// <summary>
    /// IntentRecognizer & event handlers cleanup after use
    /// </summary>
    public async void StopIntentRecognition()
    {
        if (intentreco != null)
        {
            await intentreco.StopContinuousRecognitionAsync().ConfigureAwait(false);
            intentreco.Recognizing -= RecognizingHandler;
            intentreco.Recognized -= RecognizedHandler;
            intentreco.SpeechStartDetected -= SpeechStartDetectedHandler;
            intentreco.SpeechEndDetected -= SpeechEndDetectedHandler;
            intentreco.Canceled -= CanceledHandler;
            intentreco.SessionStarted -= SessionStartedHandler;
            intentreco.SessionStopped -= SessionStoppedHandler;
            intentreco.Dispose();
            intentreco = null;
            recognizedString = "Intent Recognizer is now stopped.";
            UnityEngine.Debug.LogFormat("Intent Recognizer is now stopped.");
        }
    }

    /// <summary>
    /// Processes the user's voice command captured via speech recognition based on the 
    /// intent & entities returned by the LUIS service.
    /// To learn more about a more advanced approach for processing LUIS results from
    /// within Unity projects, check out http://aka.ms/MrLuis (external blog link)
    /// </summary>
    /// <param name="intent"></param>
    /// -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------/////
    void ProcessIntent(IntentResult intent)
    {
        //string target = "";
        //string attribute = "";

        string YesEnt = "";
        string NoEnt = "";
        string HelpEnt = "";
        string TicketEnt = "";
        string TurnstileEnt = "";
        string MetroEnt = "";
        string ExitEnt = "";
        string NoneEnt = "";

        // Extract the entities if any, this can be the target shape or the 
        // transformation such as colors or scaling.
        // INTRO 

        if (intent.entities.Count > 0)
        {
            TS = GameObject.FindObjectOfType<TestSpeech>();
            recognizedString += $"{Environment.NewLine}Entities=";
            for (int i = 0; i < intent.entities.Count; i++)
            {
                Debug.Log(intent.entities[i].type.ToLower());
                if (intent.entities[i].type.ToLower() == "yesent")
                {
                    //Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaa" + actualTask);
                    switch (actualTask)
                    {
                        case "intro":
                            phraseToSay = "If you need assistance, just say 'Help'. Now frame the metro sign with your phone. Do you see it?";
                            actualTask = "metroSign";
                            timer = 0f;
                            break;
                        case "metroSign":
                            if (tardFlag == true)
                            {
                                MSS.statusMetroSignTrue();
                                tardFlag = false;
                            }
                            if (MSS.StatusMetroSign() == true)
                            {
                                MIMG.buttonMetroSignFalse();
                                phraseToSay = MSS.StringaMetroSign();
                                actualTask = "entranceSign";
                                timer = 0f;
                                counter = 0;

                                RobotNormal.gameObject.SetActive(false);
                                RobotAngry.gameObject.SetActive(false);
                                RobotHappy.gameObject.SetActive(true);
                                RobotReward.gameObject.SetActive(false); 

                            }
                            else if (MSS.StatusMetroSign() == false)
                            {
                                phraseToSay = "I don't think so";
                                timer = 12f;

                                RobotNormal.gameObject.SetActive(true);
                                RobotAngry.gameObject.SetActive(false);
                                RobotHappy.gameObject.SetActive(false);
                                RobotReward.gameObject.SetActive(false);
                            }
                            break;
                        case "findTicketMachine":
                            if (tardFlag == true)
                            {
                                IMT0.statusTrue();
                                tardFlag = false;
                            }
                            if (IMT0.Status() == true)
                            {
                                MIMG.buttonTicketMachineFalse();
                                phraseToSay = IMT0.Stringa0();
                                actualTask = "screen1";
                                timer = 0f;
                                counter = 0;

                                RobotNormal.gameObject.SetActive(false);
                                RobotAngry.gameObject.SetActive(false);
                                RobotHappy.gameObject.SetActive(true);
                                RobotReward.gameObject.SetActive(false);
                            }
                            else if (IMT0.Status() == false)
                            {
                                phraseToSay = "I don't think so";
                                timer = 12f;

                                RobotNormal.gameObject.SetActive(true);
                                RobotAngry.gameObject.SetActive(false);
                                RobotHappy.gameObject.SetActive(false);
                                RobotReward.gameObject.SetActive(false);
                            }
                            break;

                        case "findTurnstile":
                            if (tardFlag == true)
                            {
                                TSs.StatusTurnTrue();
                                DSM.StatusScaleMobiliTrue();
                                tardFlag = false;
                                stepAutoConfirm = 12;
                                timer = 0f;
                                counter = 0;
                                MIMG.buttonTurnstileFalse();
                                break;
                            }
                            if (TSs.StatusTurn() == true)
                            {
                                MIMG.buttonTurnstileFalse();
                                phraseToSay = TSs.StringaTurn();
                                actualTask = "autoStairs";
                                timer = 0f;
                                counter = 0;

                                RobotNormal.gameObject.SetActive(false);
                                RobotAngry.gameObject.SetActive(false);
                                RobotHappy.gameObject.SetActive(true);
                                RobotReward.gameObject.SetActive(false);
                            }
                            else if (TSs.StatusTurn() == false)
                            {
                                phraseToSay = "I don't think so";
                                timer = 12f;

                                RobotNormal.gameObject.SetActive(true);
                                RobotAngry.gameObject.SetActive(false);
                                RobotHappy.gameObject.SetActive(false);
                                RobotReward.gameObject.SetActive(false);
                            }

                            break;
                        case "autoStairs":

                            if (tardFlag == true)
                            {
                                DSM.StatusScaleMobiliTrue();
                                tardFlag = false;
                            }

                            if (DSM.StatusScaleMobili() == true)
                            {
                                phraseToSay = DSM.StringaScaleMobili();
                                actualTask = "versoBignami";
                                timer = 0f;
                                counter = 0;

                                RobotNormal.gameObject.SetActive(false);
                                RobotAngry.gameObject.SetActive(false);
                                RobotHappy.gameObject.SetActive(true);
                                RobotReward.gameObject.SetActive(false);
                            }
                            else if (DSM.StatusScaleMobili() == false)
                            {
                                phraseToSay = "I don't think so";
                                timer = 12f;

                                RobotNormal.gameObject.SetActive(true);
                                RobotAngry.gameObject.SetActive(false);
                                RobotHappy.gameObject.SetActive(false);
                                RobotReward.gameObject.SetActive(false);
                            }

                            break;

                        case "findTurnstileExit":
                            if (tardFlag == true)
                            {
                                TSE.statusTurnstilesExitTrue();
                                tardFlag = false;
                                MIMG.buttonTurnstileFalse();
                            }
                            if (TSE.StatusTurnstilesExit() == true)
                            {
                                MIMG.buttonTurnstileFalse();
                                phraseToSay = TSE.StringaTurnstilesExit();
                                actualTask = "finalExit";
                                timer = 0f;
                                counter = 0;
                                RobotNormal.gameObject.SetActive(false);
                                RobotAngry.gameObject.SetActive(false);
                                RobotHappy.gameObject.SetActive(true);
                                RobotReward.gameObject.SetActive(false);

                            }
                            else if (TSE.StatusTurnstilesExit() == false)
                            {
                                phraseToSay = "I don't think so";
                                timer = 12f;

                                RobotNormal.gameObject.SetActive(true);
                                RobotAngry.gameObject.SetActive(false);
                                RobotHappy.gameObject.SetActive(false);
                                RobotReward.gameObject.SetActive(false);
                            }
                            break;

                        default:
                            break;
                    }
                    //phraseToSay = intent.entities[i].entity;

                }
                if (intent.entities[i].type.ToLower() == "noent")
                {
                    switch (actualTask)
                    {
                        case "intro":
                            phraseToSay = "I am your personal assistant, i'm going to guide you through this experience. If you need assistance just say 'Help'. Now frame the metro sign with your phone. Do you see it?";
                            actualTask = "metroSign";
                            break;
                        case "metroSign":
                            if (tardFlag == true)
                            {
                                counter = 0;
                            }
                            if (MSS.StatusMetroSign() == true)
                            {
                                MIMG.buttonMetroSignFalse();
                                phraseToSay = MSS.StringaMetroSign();
                                actualTask = "entranceSign";
                                timer = 0f;
                                break;
                            }

                            RobotNormal.gameObject.SetActive(true);
                            RobotAngry.gameObject.SetActive(false);
                            RobotHappy.gameObject.SetActive(false);
                            RobotReward.gameObject.SetActive(false);

                            timer = 15f;
                            break;

                        case "findTicketMachine":
                            if (tardFlag == true)
                            {
                                counter = 0;
                            }
                            if (IMT0.Status() == true)
                            {
                                MIMG.buttonTicketMachineFalse();
                                phraseToSay = IMT0.Stringa0();
                                actualTask = "screen1";
                                timer = 0f;
                                break;
                            }

                            RobotNormal.gameObject.SetActive(true);
                            RobotAngry.gameObject.SetActive(false);
                            RobotHappy.gameObject.SetActive(false);
                            RobotReward.gameObject.SetActive(false);

                            timer = 15f;
                            break;

                        case "findTurnstile":
                            if (tardFlag == true)
                            {
                                counter = 0;
                            }
                            if (IMT0.Status() == true)
                            {
                                MIMG.buttonTurnstileFalse();
                                phraseToSay = IMT0.Stringa0();
                                actualTask = "autoStairs";
                                timer = 0f;
                                break;
                            }

                            RobotNormal.gameObject.SetActive(true);
                            RobotAngry.gameObject.SetActive(false);
                            RobotHappy.gameObject.SetActive(false);
                            RobotReward.gameObject.SetActive(false);

                            timer = 15f;
                            break;

                        case "autoStairs":
                            if (tardFlag == true)
                            {
                                counter = 0;
                            }
                            if (DSM.StatusScaleMobili() == true)
                            {
                                phraseToSay = DSM.StringaScaleMobili();
                                actualTask = "versoBignami";
                                timer = 0f;
                                break;
                            }

                            RobotNormal.gameObject.SetActive(true);
                            RobotAngry.gameObject.SetActive(false);
                            RobotHappy.gameObject.SetActive(false);
                            RobotReward.gameObject.SetActive(false);

                            timer = 15f;
                            break;

                        case "findTurnstileExit":
                            if (tardFlag == true)
                            {
                                counter = 0;
                            }
                            if (TSE.StatusTurnstilesExit() == true)
                            {
                                MIMG.buttonTurnstileFalse();
                                phraseToSay = TSE.StringaTurnstilesExit();
                                actualTask = "finalExit";
                                timer = 0f;
                                break;
                            }

                            RobotNormal.gameObject.SetActive(true);
                            RobotAngry.gameObject.SetActive(false);
                            RobotHappy.gameObject.SetActive(false);
                            RobotReward.gameObject.SetActive(false);

                            timer = 15f;
                            break;


                        default:
                            break;
                    }
                }
                if (intent.entities[i].type.ToLower() == "helpent")
                {

                    PHS = GameObject.FindObjectOfType<PageHelpScript>();
                    switch (actualTask)
                    {
                        case "metroSign":
                            PHS.HelpOn();
                            helpText = "Right now you are outside the station, it is necessary that you find the sign of the Metro which is big and red, and you target it with the phone. Is about 3 meters tall and it has a white M in the middle";
                            break;

                        case "entranceSign":
                            PHS.HelpOn();
                            helpText = "You have to find the stairs to go down and above them you will see the sign of the metro of your current stop with big letters. You have to target that with your phone";
                            break;

                        case "findTicketMachine":
                            PHS.HelpOn();
                            helpText = "To proceed with the experience you have to buy a ticket and in order to do that you have first to find the ticket machine. It is purple and it is tall about two meters: find it and frame it with the phone";
                            break;

                        case "screen1":
                            PHS.HelpOn();
                            helpText = "Tap on the screen, which usually have different patterns, in order to start the flow to buy the ticket";
                            break;

                        case "screen2":
                            PHS.HelpOn();
                            helpText = "In this screen you have to select the language as the hand is showing, then you will continue the purchase";
                            break;
                        case "screen3":
                            PHS.HelpOn();
                            helpText = "In this screen , following the hand on the phone, purchase the ticket of the type '' Ordinary 3 Zones Mi1-Mi3 '' ";
                            break;
                        case "screen4":
                            PHS.HelpOn();
                            helpText = "This screen just shows a preview of the price of the ticket you are about to buy";
                            break;
                        case "screen5":
                            PHS.HelpOn();
                            helpText = "In this screen, according to how you want to pay, you can select and insert the payment method. Follow the guide of the hand on your phone";
                            break;
                        case "screen6":
                            PHS.HelpOn();
                            helpText = "In this screen, according to how you want to pay, you can select and insert the payment method. Follow the guide of the hand on your phone";
                            break;
                        case "screen7":
                            PHS.HelpOn();
                            helpText = "In this screen you complete the payment by inserting the coins or by inserting the card";
                            break;

                        case "ticketCheck":
                            PHS.HelpOn();
                            helpText = "We need to check if you bought the correct ticket. Once you have it, please, frame it with the phone on the side with patterns and words.";
                            break;

                        case "findTurnstile":
                            PHS.HelpOn();
                            helpText = "To continue you have to use the ticket and insert it in the turnstile. So, now you need to find the turnstile , which is usually coloured of orange and about 1 meter tall, and target it with your phone";
                            break;

                        case "autoStairs":
                            PHS.HelpOn();
                            helpText = "Follow the arrows that appear on the screen, and the signals on the wall, in order to reach the metro rail where you will be able to continue your trip";
                            break;

                        case "versoBignami":
                            PHS.HelpOn();
                            helpText = "Follow the arrows that appear on the screen, and the signals on the wall, in order to reach the metro rail where you will be able to continue your trip";
                            break;

                        case "metroDoor":
                            PHS.HelpOn();
                            helpText = "When you reach the external doors of the metro you have to frame the yellow signals that are sticked on it. To check what signals, wait for the assistant suggestions.";
                            break;

                        case "metroInDoor":
                            PHS.HelpOn();
                            helpText = "When you are inside the metro, please, get close to a door and frame with the phone the yellow signs that are sticked on it. To check what signals, wait for the assistant suggestions.";
                            break;

                        case "goOut":
                            PHS.HelpOn();
                            helpText = "Follow the arrows that appear on the screen, and the signals on the wall, in order to reach the exit where you will be able to end your trip";
                            break;

                        case "findTurnstileExit":
                            PHS.HelpOn();
                            helpText = "To continue you have to use the ticket and insert it in the turnstile. So, now you need to find the turnstile , which is usually coloured of orange and about 1 meter tall, and target it with your phone";
                            break;

                        case "finalExit":
                            PHS.HelpOn();
                            helpText = "After you overcome the turnstiles, please, keep following the arrow and try to find the stairs that will lead you out of the station and finish your experience"; 
                            break;

                        default:
                            break;
                    }

                    Debug.Log("------------------------------------------------------------");
                }
                if (intent.entities[i].type.ToLower() == "TicketEnt")
                {
                    phraseToSay = intent.entities[i].entity;
                }
                if (intent.entities[i].type.ToLower() == "turnstileent")
                {

                    stopUpdate = false;
                    phraseToSay = "turnstile";
                    //stopUpdate = TS.RespStopUpdateSpeech();


                }
                if (intent.entities[i].type.ToLower() == "metroent")
                {
                    stopUpdate = false;
                    phraseToSay = "metro";
                    //while (stopUpdate == false) { stopUpdate = TS.RespStopUpdateSpeech(); }
                }
                if (intent.entities[i].type.ToLower() == "ExitEnt")
                {
                    phraseToSay = intent.entities[i].entity;
                }
                if (intent.entities[i].type.ToLower() == "NoneEnt")
                {
                    phraseToSay = intent.entities[i].entity;
                }
            }
        }


    }

    public string PassPhrase()
    {
        return phraseToSay;
    }

    public void SetStart()
    {
         phraseToSay="hello! Do you know who i am?";
        Finish = false;
    }

    public void AutoPassNextTask()
    {

        Debug.Log("TIMER ------->" + rewardTimer);

        MSS = GameObject.FindObjectOfType<metroSignScript>();
        bool statoMetroSign = MSS.StatusMetroSign();
      


        if (statoMetroSign == true && passa == true)
        {
            rewardTimer = 0;

            MIMG.buttonMetroSignFalse();
            phraseToSay = MSS.StringaMetroSign();
            actualTask = "entranceSign";
            stepAutoConfirm=1;
            counter = 0;
            timer = 0f;
            passa = false;

        }

        if (rewardTimer <= 3.0f && passa == false || rewardTimer <= 3.0f && passa2 == false || rewardTimer <= 3.0f && passa12 == false 
            || rewardTimer <= 3.0f && passa11 == false || rewardTimer <= 3.0f && passa20 == false || rewardTimer <= 3.0f && passa22 == false)
        {
            RobotNormal.gameObject.SetActive(false);
            RobotAngry.gameObject.SetActive(false);
            RobotHappy.gameObject.SetActive(false);
            RobotReward.gameObject.SetActive(true);
        }
        else
        {
            if (rewardTimer <= 10) { 
            RobotNormal.gameObject.SetActive(false);
            RobotAngry.gameObject.SetActive(false);
            RobotHappy.gameObject.SetActive(true);
            RobotReward.gameObject.SetActive(false);
            }
        }

        SE = GameObject.FindObjectOfType<ScriptEntrata>();
        bool statoEntrata = SE.StatusEntrata();


        if (statoEntrata == true && passa1 == true)
        {
            
            phraseToSay = SE.StringaEntrata();
            if (phraseToSay == "Go down the stairs and find the turnstile. Can you find it?")
            {

                actualTask = "findTurnstile";
                stepAutoConfirm = 10;
                counter = 0;
                timer = 0f;
                passa1 = false;
                RobotNormal.gameObject.SetActive(false);
                RobotAngry.gameObject.SetActive(false);
                RobotHappy.gameObject.SetActive(true);
                RobotReward.gameObject.SetActive(false);


            }

            else if (phraseToSay == "Go down the stairs and find the ticket machine. Can you find it?")
            {

                actualTask = "findTicketMachine";
                timer = 0f;
                stepAutoConfirm=2;
                counter = 0;
                passa1 = false;
                RobotNormal.gameObject.SetActive(false);
                RobotAngry.gameObject.SetActive(false);
                RobotHappy.gameObject.SetActive(true);
                RobotReward.gameObject.SetActive(false);
            }

        }

        IMT0 = GameObject.FindObjectOfType<ticketMachineScript>();
        bool stato0 = IMT0.Status();

        if (stato0 == true && passa2 == true)
        {
            rewardTimer = 0;
            MIMG.buttonTicketMachineFalse();
            phraseToSay = IMT0.Stringa0();
            actualTask = "screen1";
            timer = 0f;
            stepAutoConfirm=3;
            counter = 0;
            passa2 = false;
            RobotNormal.gameObject.SetActive(false);
            RobotAngry.gameObject.SetActive(false);
            RobotHappy.gameObject.SetActive(true);
            RobotReward.gameObject.SetActive(false);
        }

        IMT1 = GameObject.FindObjectOfType<ScriptImageTarget1>();
        bool stato1 = IMT1.Status1();


        if (stato1 == true && passa3 == true)
        {
            phraseToSay = IMT1.Stringa1();
            actualTask = "screen2";
            timer = 0f;
            stepAutoConfirm=4;
            counter = 0;
            passa3 = false;
            RobotNormal.gameObject.SetActive(false);
            RobotAngry.gameObject.SetActive(false);
            RobotHappy.gameObject.SetActive(true);
            RobotReward.gameObject.SetActive(false);
        }

        IMT2 = GameObject.FindObjectOfType<ScriptImageTarget2>();
        bool stato2 = IMT2.Status2();


        if (stato2 == true && passa4 == true)
        {
            phraseToSay = IMT2.Stringa2();
            actualTask = "screen3";
            timer = 0f;
            stepAutoConfirm=5;
            counter = 0;
            passa4 = false;
            RobotNormal.gameObject.SetActive(false);
            RobotAngry.gameObject.SetActive(false);
            RobotHappy.gameObject.SetActive(true);
            RobotReward.gameObject.SetActive(false);
        }

        IMT3 = GameObject.FindObjectOfType<ScriptImageTarget3>();
        bool stato3 = IMT3.Status3();


        if (stato3 == true && passa5 == true)
        {
            phraseToSay = IMT3.Stringa3();
            actualTask = "screen4";
            timer = 0f;
            stepAutoConfirm=6;
            counter = 0;
            passa5 = false;
            RobotNormal.gameObject.SetActive(false);
            RobotAngry.gameObject.SetActive(false);
            RobotHappy.gameObject.SetActive(true);
            RobotReward.gameObject.SetActive(false);
        }

        IMT4 = GameObject.FindObjectOfType<ScriptImageTarget4>();
        bool stato4 = IMT4.Status4();


        if (stato4 == true && passa6 == true)
        {
            phraseToSay = IMT4.Stringa4();
            actualTask = "screen5";
            timer = 0f;
            stepAutoConfirm=7;
            counter = 0;
            passa6 = false;
            RobotNormal.gameObject.SetActive(false);
            RobotAngry.gameObject.SetActive(false);
            RobotHappy.gameObject.SetActive(true);
            RobotReward.gameObject.SetActive(false);
        }

        IMT5_Card = GameObject.FindObjectOfType<ScriptImageTarget5_Card>();
        bool stato5_Card = IMT5_Card.Status5_Card();
        IMT5_Cash = GameObject.FindObjectOfType<ScriptImageTarget5_Cash>();
        bool stato5_Cash = IMT5_Cash.Status5_Cash();

        if (stato5_Card == true && passa7 == true)
        {
            phraseToSay = IMT5_Card.Stringa5_Card();
            actualTask = "screen6";
            timer = 0f;
            stepAutoConfirm = 9;
            counter = 0;
            passa7 = false;
            RobotNormal.gameObject.SetActive(false);
            RobotAngry.gameObject.SetActive(false);
            RobotHappy.gameObject.SetActive(true);
            RobotReward.gameObject.SetActive(false);
        }
        else if (stato5_Cash == true && passa8 == true)
        {
            phraseToSay = IMT5_Cash.Stringa5_Cash();
            actualTask = "screen6";
            timer = 0f;
            stepAutoConfirm = 8;
            counter = 0;
            passa8 = false;
            RobotNormal.gameObject.SetActive(false);
            RobotAngry.gameObject.SetActive(false);
            RobotHappy.gameObject.SetActive(true);
            RobotReward.gameObject.SetActive(false);
        }

        IMT6 = GameObject.FindObjectOfType<ScriptImageTarget6>();
        bool stato6 = IMT6.Status6();


        if (stato6 == true && passa9 == true)
        {
            phraseToSay = IMT6.Stringa6();
            actualTask = "ticketCheck";
            timer = 0f;
            stepAutoConfirm = 10;
            counter = 0;
            passa9 = false;
            RobotNormal.gameObject.SetActive(false);
            RobotAngry.gameObject.SetActive(false);
            RobotHappy.gameObject.SetActive(true);
            RobotReward.gameObject.SetActive(false);
        }

        IMT7 = GameObject.FindObjectOfType<ScriptImageTarget7>();
        bool stato7 = IMT7.Status7();


        if (stato7 == true && passa10 == true)
        {
            phraseToSay = IMT7.Stringa7();
            actualTask = "ticketCheck";
            //phraseToSay = "Frame the ticket with your camera";
            timer = 0f;
            stepAutoConfirm=10;
            counter = 0;
            passa10 = false;
            RobotNormal.gameObject.SetActive(false);
            RobotAngry.gameObject.SetActive(false);
            RobotHappy.gameObject.SetActive(true);
            RobotReward.gameObject.SetActive(false);
        }

        IMTT = GameObject.FindObjectOfType<ScriptImageTargetTicket>();
        bool statoTicket = IMTT.StatusTicket();
        TSs = GameObject.FindObjectOfType<turnstilesScript>();
        bool statoTurnstiles = TSs.StatusTurn();
        bool hoGiaIlBiglietto = TSs.TicketAlreadyHave();

        if (statoTicket == true && passa11 == true)
        {
            rewardTimer = 0;
            MIMG.buttonTicketFalse();
            actualTask = "findTurnstile";
            phraseToSay = IMTT.StringaTicket();
            timer = 0f;
            stepAutoConfirm = 11;
            counter = 0;
            passa11 = false;

            RobotNormal.gameObject.SetActive(false);
            RobotAngry.gameObject.SetActive(false);
            RobotHappy.gameObject.SetActive(true);
            RobotReward.gameObject.SetActive(false);
            //rewardTimer = 0;
        }


        if (statoTurnstiles == true && passa12 == true)
        {
            rewardTimer = 0;
            MIMG.buttonTurnstileFalse();
            phraseToSay = TSs.StringaTurn();
            actualTask = "autoStairs";
            stepAutoConfirm=12;
            counter = 0;
            timer = 0f;
            // Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            passa12 = false;
            RobotNormal.gameObject.SetActive(false);
            RobotAngry.gameObject.SetActive(false);
            RobotHappy.gameObject.SetActive(true);
            RobotReward.gameObject.SetActive(false);


        }

        DSM = GameObject.FindObjectOfType<DiscesaScaleMobiliScript>();
        bool statoScaleMobili = DSM.StatusScaleMobili();


        if (statoScaleMobili == true && passa13 == true)
        {
            phraseToSay = DSM.StringaScaleMobili();
            actualTask = "versoBignami";
            timer = 0f;
            stepAutoConfirm=13;
            counter = 0;
            passa13 = false;
            RobotNormal.gameObject.SetActive(false);
            RobotAngry.gameObject.SetActive(false);
            RobotHappy.gameObject.SetActive(true);
            RobotReward.gameObject.SetActive(false);
        }

        //private Binario1_Bignami2 B1B2;

        // private ScriptBinario1_Bignami B1B;

        B1B = GameObject.FindObjectOfType<ScriptBinario1_Bignami>();
        bool statoBignami = B1B.StatusBignami();
        B2SS = GameObject.FindObjectOfType<ScriptBinario2_SanSiro>();
        bool statoSanSiro = B2SS.StatusSanSiro();
        B1B2 = GameObject.FindObjectOfType<Binario1_Bignami2>();
        bool statoBignami2 = B1B2.StatusBignami2();


        if (statoBignami == true && passa14 == true)
        {
            phraseToSay = B1B.StringaBignami();
            actualTask = "metroDoor";
            timer = 0f;
            stepAutoConfirm = 15;
            counter = 0;
            passa14 = false;
            RobotNormal.gameObject.SetActive(false);
            RobotAngry.gameObject.SetActive(false);
            RobotHappy.gameObject.SetActive(true);
            RobotReward.gameObject.SetActive(false);
        }
         if (statoSanSiro == true && passa15 == true)
        {
            phraseToSay = B2SS.StringaSanSiro();
            actualTask = "metroDoor";
            timer = 0f;
            stepAutoConfirm = 15;
            counter = 0;
            passa15 = false;
            RobotNormal.gameObject.SetActive(false);
            RobotAngry.gameObject.SetActive(false);
            RobotHappy.gameObject.SetActive(true);
            RobotReward.gameObject.SetActive(false);
        }
        if (statoBignami2 == true && passa16 == true)
        {
            phraseToSay = B1B2.StringaBignami2();
            actualTask = "metroDoor";
            timer = 0f;
            stepAutoConfirm = 15;
            counter = 0;
            passa16 = false;
            RobotNormal.gameObject.SetActive(false);
            RobotAngry.gameObject.SetActive(false);
            RobotHappy.gameObject.SetActive(true);
            RobotReward.gameObject.SetActive(false);
        }

        PEM = GameObject.FindObjectOfType<PortaExtMetroScript>();
        bool statoPortaExtMetro = PEM.StatusPortaExtMetro();


        if (statoPortaExtMetro == true && passa17 == true)
        {
            MIMG.buttonOutMetroFalse();
            phraseToSay = PEM.StringaPortaExtMetro();
            actualTask = "metroInDoor";
            timer = 0f;
            stepAutoConfirm=16;
            counter = 0;
            passa17 = false;
            RobotNormal.gameObject.SetActive(false);
            RobotAngry.gameObject.SetActive(false);
            RobotHappy.gameObject.SetActive(true);
            RobotReward.gameObject.SetActive(false);
        }

        PIM = GameObject.FindObjectOfType<PortaIntMetroScript>();
        bool statoPortaIntMetro = PIM.StatusPortaIntMetro();


        if (statoPortaIntMetro == true && passa18 == true)
        {
            MIMG.buttonInMetroFalse();
            phraseToSay = PIM.StringaPortaIntMetro();
            actualTask = "goOut";
            timer = 0f;
            stepAutoConfirm=17;
            counter = 0;
            passa18 = false;
            RobotNormal.gameObject.SetActive(false);
            RobotAngry.gameObject.SetActive(false);
            RobotHappy.gameObject.SetActive(true);
            RobotReward.gameObject.SetActive(false);
        }

        USC = GameObject.FindObjectOfType<UscitaScript>();
        bool statoUscita = USC.StatusExit();


        if (statoUscita == true && passa19 == true)
        {
            phraseToSay = USC.StringaExit();
            actualTask = "findTurnstileExit";
            timer = 0f;
            stepAutoConfirm=18;
            counter = 0;
            passa19 = false;
            RobotNormal.gameObject.SetActive(false);
            RobotAngry.gameObject.SetActive(false);
            RobotHappy.gameObject.SetActive(true);
            RobotReward.gameObject.SetActive(false);
        }

        TSE = GameObject.FindObjectOfType<turnstilesExitScript>();
        bool statoTurnstilesExit = TSE.StatusTurnstilesExit();


        if (statoTurnstilesExit == true && passa20 == true)
        {
            rewardTimer = 0;
            MIMG.buttonTurnstileFalse();
            phraseToSay = TSE.StringaTurnstilesExit();
            actualTask = "finalExit";
            timer = 0f;
            stepAutoConfirm=19;
            counter = 0;
            passa20 = false;
            RobotNormal.gameObject.SetActive(false);
            RobotAngry.gameObject.SetActive(false);
            RobotHappy.gameObject.SetActive(true);
            RobotReward.gameObject.SetActive(false);
        }

        UFS = GameObject.FindObjectOfType<UscitaFinaleScript>();
        bool statoUscitaFinale = UFS.StatusUscitaFinale();


        if (statoUscitaFinale == true && passa21 == true)
        {
            phraseToSay = UFS.StringaUscitaFinale();
            actualTask = "endGame";
            timer = 0f;
            stepAutoConfirm=20;
            counter = 0;
            passa21 = false;
            RobotNormal.gameObject.SetActive(false);
            RobotAngry.gameObject.SetActive(false);
            RobotHappy.gameObject.SetActive(true);
            RobotReward.gameObject.SetActive(false);
        }

        MSE = GameObject.FindObjectOfType<metroSignExit>();
        bool statoMetroSignExit = MSE.StatusMetroSignExit();


        if (statoMetroSignExit == true && passa22 == true)
        {
            rewardTimer = 0;
            MIMG.buttonMetroSignFalse();

            pageFinish.gameObject.SetActive(true);
            panel.gameObject.SetActive(false);
            panelUI.gameObject.SetActive(true);
           // HELL.SetVolume0();
            Finish = true;
            phraseToSay = MSE.StringaMetroSignExit();
            passa22 = false;

            RobotNormal.gameObject.SetActive(false);
            RobotNormal1.gameObject.SetActive(false);
            RobotAngry.gameObject.SetActive(false);
            RobotHappy.gameObject.SetActive(false);
            RobotReward.gameObject.SetActive(false);

        }
    }

    public void accendiMicrofono()
    {
        accendi = true;
    }

    public void spegniMicrofono()
    {
        accendi = false;
    }

    public string passTextHelp()
    {
       return  helpText;
    }

    public void turnRobotsOff()
    {
        RobotNormal.gameObject.SetActive(false);
        RobotAngry.gameObject.SetActive(false);
        RobotHappy.gameObject.SetActive(true);
        RobotReward.gameObject.SetActive(false);
    }

    public void turnRobotHappyOn()
    {
        RobotNormal.gameObject.SetActive(false);
        RobotAngry.gameObject.SetActive(false);
        RobotHappy.gameObject.SetActive(false);
        RobotReward.gameObject.SetActive(true);
    }

    public bool FinishFlag()
    {
        return Finish;
    }
}

