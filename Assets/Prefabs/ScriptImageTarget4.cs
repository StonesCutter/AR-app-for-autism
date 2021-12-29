using UnityEngine;
using Vuforia;
using System.Collections;
using System.Collections.Generic;

public class ScriptImageTarget4 : DefaultObserverEventHandler
{

    public ObserverBehaviour mTrackableBehaviour;
    private ScriptImageTarget3 SIT3;
    public bool status4;
    public string stringa4 = "seleziona 'Continue' per procedere al pagamento";

    void Update()
    {
        mTrackableBehaviour = GetComponent<ObserverBehaviour>();
        SIT3 = GameObject.FindObjectOfType<ScriptImageTarget3>();
        bool stato3 = SIT3.Status3();
        //Debug.Log("PASSAGGIO PARAMETRO E' " + stato);

        if (stato3 == false)
        {
            mTrackableBehaviour.enabled = false;
        }
        else if (stato3 == true)
        {
            mTrackableBehaviour.enabled = true;
        }

    }

    protected override void OnTrackingFound()
        {

            status4 = true;

        }

    public bool Status4()
        {
            return status4;
        }

    public string Stringa4()
        {
             return stringa4;
         }

    public void Status4False(){

             status4 = false;

        }

    public void Status4True(){

             status4 = true;

        }

}