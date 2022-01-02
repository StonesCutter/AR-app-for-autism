using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderVolumePage2 : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _sliderText;
    public float Volume;
    public bool Chosen = false;
    private HelloWorld TSPC;


    void Start()
    {
        TSPC = GameObject.FindObjectOfType<HelloWorld>();
        _slider.onValueChanged.AddListener((v) =>
        {
           // _sliderText.text = v.ToString("3");
        });
        Chosen = false;
    }


    void Update()
    {
        _slider.onValueChanged.AddListener((v) =>
        {
            _sliderText.text = v.ToString();
        });




        if(_sliderText.text == "0"){
            // Volume = 0.0f;
            TSPC.SetVolume0();
            Chosen = true;

        }

        if(_sliderText.text == "1"){
            //Volume = 0.25f;
            TSPC.SetVolume1();
            Chosen = true;
        }

        if(_sliderText.text == "2"){
            //Volume = 0.5f;
            TSPC.SetVolume2();
            Chosen = true;
        }

        if(_sliderText.text == "3"){
            //Volume = 0.75f;
            TSPC.SetVolume3();
            Chosen = true;
        }

        if(_sliderText.text == "4"){
            //Volume = 1.0f;
            TSPC.SetVolume4();
            Chosen = true;
        }

        //TSPC.SetVolume(Volume);


       //        Debug.Log(Volume);
    }

    public float Volume1(){
        return Volume;
    }

    public bool VolumeHasBeenChosen()
    {
        return Chosen;
    }
}
