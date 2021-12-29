using UnityEngine;
using Vuforia;
using System.Collections;
using System.Collections.Generic;

public class ScriptImageTarget3 : DefaultObserverEventHandler
{

    public ObserverBehaviour mTrackableBehaviour;
    private ScriptImageTarget2 SIT2;
    public bool status3;
    public string stringa3 = "seleziona il tipo di biglietto";


    void Update()
    {
        mTrackableBehaviour = GetComponent<ObserverBehaviour>();
        SIT2 = GameObject.FindObjectOfType<ScriptImageTarget2>();
        bool stato2 = SIT2.Status2();
        //Debug.Log("PASSAGGIO PARAMETRO E' " + stato);

        if (stato2 == false)
        {
            mTrackableBehaviour.enabled = false;
        }
        else if (stato2 == true)
        {
            mTrackableBehaviour.enabled = true;
        }

    }

    protected override void OnTrackingFound()
        {

            status3 = true;

        }

    public bool Status3()
        {
            return status3;
        }

    public string Stringa3()
        {
             return stringa3;
         }

    public void Status3False(){

             status3 = false;

        }

    public void Status3True(){

             status3 = true;

        }

}