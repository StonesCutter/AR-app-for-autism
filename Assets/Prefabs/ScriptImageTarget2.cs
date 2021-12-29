using UnityEngine;
using Vuforia;
using System.Collections;
using System.Collections.Generic;

public class ScriptImageTarget2 : DefaultObserverEventHandler
{

    public ObserverBehaviour mTrackableBehaviour;
    private ScriptImageTarget1 SIT1;
    public bool status2;
    public string stringa2 = "seleziona la lingua";

    void Update()
    {
        mTrackableBehaviour = GetComponent<ObserverBehaviour>();
        SIT1 = GameObject.FindObjectOfType<ScriptImageTarget1>();
        bool stato1 = SIT1.Status1();
        //Debug.Log("PASSAGGIO PARAMETRO E' " + stato);

        if (stato1 == false)
        {
            mTrackableBehaviour.enabled = false;
        }
        else if (stato1 == true)
        {
            mTrackableBehaviour.enabled = true;
        }

    }

    protected override void OnTrackingFound()
        {

            status2 = true;

        }

    public bool Status2()
        {
            return status2;
        }

    public string Stringa2()
        {
             return stringa2;
         }

    public void Status2False(){

             status2 = false;

        }

    public void Status2True(){

             status2 = true;

        }

}