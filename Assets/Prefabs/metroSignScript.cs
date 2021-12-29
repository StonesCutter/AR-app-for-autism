using UnityEngine;
using Vuforia;
using System.Collections;
using System.Collections.Generic;

public class metroSignScript : DefaultObserverEventHandler
{

    public ObserverBehaviour mTrackableBehaviour;
    public bool statusMetroSign = false;
    public string stringaMetroSign = "Well done! Now go down the stairs right below the sign of this stop";
    private IntentRecognition IRC;

    public GameObject RobotAngry;
    public GameObject RobotHappy;
    public GameObject RobotNormal;
    public GameObject RobotReward;


    private PortaIntMetroScript portaIntMetro;


    void Update()
    {
        IRC = GameObject.FindObjectOfType<IntentRecognition>();
        mTrackableBehaviour = GetComponent<ObserverBehaviour>();
        portaIntMetro = GameObject.FindObjectOfType<PortaIntMetroScript>();
        bool passaggio = portaIntMetro.Pass();

        if (passaggio == false)
        {
            mTrackableBehaviour.enabled = false;
        }
        else if (passaggio == true)
        {
            mTrackableBehaviour.enabled = true;
        }
    }


    protected override void OnTrackingFound()
        {

       // IRC = GameObject.FindObjectOfType<IntentRecognition>();
        statusMetroSign = true;

       // RobotNormal.gameObject.SetActive(false);
        //RobotAngry.gameObject.SetActive(false);
        //RobotHappy.gameObject.SetActive(false);
        //RobotReward.gameObject.SetActive(true);
        //IRC.turnRobotsOff();
        //IRC.turnRobotHappyOn();
    }



    public bool StatusMetroSign()
        {
            return statusMetroSign;
        }

    public string StringaMetroSign()
         {
            return stringaMetroSign;
          }

    public void statusMetroSignFalse(){

             statusMetroSign = false;

        }

    public void statusMetroSignTrue(){

             statusMetroSign = true;

        }

    }
