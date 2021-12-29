using UnityEngine;
using Vuforia;
using System.Collections;
using System.Collections.Generic;

public class ScriptImageTargetTicket : DefaultObserverEventHandler
{
    public ObserverBehaviour mTrackableBehaviour;
    private ScriptImageTarget6 SIT6;
    private ScriptImageTarget7 SIT7;
    public bool statusTicket;
    public string stringaTicket = "Bravo hai il biglietto, ora cerca i tornelli";


    void Update()
    {
        mTrackableBehaviour = GetComponent<ObserverBehaviour>();
        SIT6 = GameObject.FindObjectOfType<ScriptImageTarget6>();
        SIT7 = GameObject.FindObjectOfType<ScriptImageTarget7>();
        bool stato6 = false;
        bool stato7 = false;
         stato6 = SIT6.Status6();
         stato7 = SIT7.Status7();


        if (stato6 == false && stato7 == false)
        {
            mTrackableBehaviour.enabled = false;
        }

        else  if (stato6 == true || stato7 == true)
        {
            mTrackableBehaviour.enabled = true;

        }

    }

    protected override void OnTrackingFound()
        {

            statusTicket = true;

        }

    public bool StatusTicket()
        {
            return statusTicket;
        }

    public string StringaTicket()
         {
            return stringaTicket;
          }

    public void StatusTicketFalse(){

             statusTicket = false;

        }

    public void StatusTicketTrue(){

             statusTicket = true;

        }

}
