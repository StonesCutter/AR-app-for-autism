using UnityEngine;
using Vuforia;
using System.Collections;
using System.Collections.Generic;

public class ScriptImageTarget6 : DefaultObserverEventHandler

{
    public ObserverBehaviour mTrackableBehaviour;
    private ScriptImageTarget5_Cash SIT5_Cash;
    public bool status6;
    public string stringa6 = "insert the cash in the machine, get the ticket and frame it with the phone";


    void Update()
    {
        mTrackableBehaviour = GetComponent<ObserverBehaviour>();
        SIT5_Cash = GameObject.FindObjectOfType<ScriptImageTarget5_Cash>();
        bool stato5_Cash = SIT5_Cash.Status5_Cash();


        if (stato5_Cash == false)
        {
            mTrackableBehaviour.enabled = false;
        }
        else if (stato5_Cash == true)
        {
            mTrackableBehaviour.enabled = true;

        }

    }

    protected override void OnTrackingFound()
        {

            status6 = true;

        }

    public bool Status6()
        {
            return status6;
        }

    public string Stringa6()
         {
            return stringa6;
          }

    public void Status6False(){

             status6 = false;

        }

    public void Status6True(){

             status6 = true;

        }

}
