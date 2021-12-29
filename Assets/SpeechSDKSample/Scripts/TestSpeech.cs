using UnityEngine;
//using UnityLibrary;

public class TestSpeech : MonoBehaviour
{
    public string sayAtStart;
    //public string sayAtStar = "Welcome!";
    public string textValue;
    public string confrontText="aaa";
    private IntentRecognition IR;
    //float timer = 0f;
    //float timeStamp = 0f;
    public bool stopUpdate;// = false;
    public bool itsOn = false;
    AudioSource audio;
    private SliderVolumePage2 VOL;

    // Start is called before the first frame update
    void Start()
    {
        // TEST speech
        //Speech.instance.Say(sayAtStart, TTSCallback);
        audio = GetComponent<AudioSource>();
        //VOL = GameObject.FindObjectOfType<SliderVolumePage2>();

    }

    public void StartSpeech()
    {
        StartFirstSpeech();
    }

    private async void StartFirstSpeech()
    {
        itsOn = true;
        //Speech.instance.Say(sayAtStart, TTSCallback);
    }
        // Update is called once per frame
        void Update()
    {

        //timer += Time.deltaTime;
        //Debug.Log("the time is: " + timer);
        // test pressing any keys to say that character
        // if (Input.anyKeyDown)
        //  {
        if(itsOn == true) {

            //VOL = GameObject.FindObjectOfType<SliderVolumePage2>();
            //audio.volume = VOL.Volume1();

            IR = GameObject.FindObjectOfType<IntentRecognition>();
        //textValue = IR.PassPhrase();
        //stopUpdate = IR.StopUpdateSpeech();

        if (confrontText!=textValue)
        {
            confrontText = textValue;
            //Speech.instance.Say(textValue, TTSCallback);
            //timeStamp = timer;
            //stopUpdate = true;
        }



        }
    }

    void TTSCallback(string message, AudioClip audio) {
        AudioSource source = GetComponent<AudioSource>();
        if(source == null) {
            source = gameObject.AddComponent<AudioSource>();
        }

        source.clip = audio;
        source.Play();
    }

   /* public bool RespStopUpdateSpeech()
    {
        return stopUpdate;
    }*/

    public void MuteSpeaker()
    {
        audio.mute = true;
    }

    public void UnMuteSpeaker()
    {
        audio.mute = false;
    }

    public void SetVolume0()
    {
        audio.volume = 0.0f;
    }

    public void SetVolume1()
    {
        audio.volume = 0.25f;
    }

    public void SetVolume2()
    {
        audio.volume = 0.50f;
    }

    public void SetVolume3()
    {
        audio.volume = 0.75f;
    }

    public void SetVolume4()
    {
        audio.volume = 1.0f;
    }
}
