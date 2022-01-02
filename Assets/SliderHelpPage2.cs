using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderHelpPage2 : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _sliderText;
    int Help;
    private HelloWorld TSPC;
    private Events MIC;
    public GameObject ButtonMicrophone;
    public GameObject PanelHelp;




    void Start()
    {
        TSPC = GameObject.FindObjectOfType<HelloWorld>();
        MIC = GameObject.FindObjectOfType<Events>();
        _slider.onValueChanged.AddListener((v) =>
        {
            _sliderText.text = v.ToString("3");
        });
    }


    void Update()
    {
        _slider.onValueChanged.AddListener((v) =>
        {
            _sliderText.text = v.ToString();
        });

       // SPC = GameObject.FindObjectOfType<Speech>();

        if (_sliderText.text == "1"){
            Help = 1;
            // SPC.SpeakerOff();
           // MIC.TurnMicOff();
            TSPC.MuteSpeaker();
            ButtonMicrophone.gameObject.SetActive(false);
            PanelHelp.gameObject.SetActive(false);
        }

        if(_sliderText.text == "2"){
            Help = 2;
            TSPC.MuteSpeaker();
            // MIC.TurnMicOff();
            //SPC.SpeakerOff();
            ButtonMicrophone.gameObject.SetActive(false);
            PanelHelp.gameObject.SetActive(true);
        }

        if(_sliderText.text == "3"){
            Help = 3;
            //SPC.SpeakerOn();
            TSPC.UnMuteSpeaker();
            //MIC.TurnMicOn();
            ButtonMicrophone.gameObject.SetActive(true);
            PanelHelp.gameObject.SetActive(true);
        }


        Debug.Log(_sliderText.text);
    }

    public int Help1()
    {
        return Help;
    }


}
