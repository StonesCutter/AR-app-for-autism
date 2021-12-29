using UnityEngine;
using Vuforia;
using System.Collections;
using System.Collections.Generic;

public class ScriptImageTarget1 : DefaultObserverEventHandler
{

    public ObserverBehaviour mTrackableBehaviour;
    private ticketMachineScript Tick;
    public bool status1;
    public string stringa1 = "schiaccia lo schermo per procedere";


    void Update()
    {
        mTrackableBehaviour = GetComponent<ObserverBehaviour>();
        Tick = GameObject.FindObjectOfType<ticketMachineScript>();
        bool stato = Tick.Status();
        

        if (stato == false)
        {
            mTrackableBehaviour.enabled = false;
        }
        else if (stato == true)
        {
            mTrackableBehaviour.enabled = true;

        }

    }

    protected override void OnTrackingFound()
        {

            status1 = true;

        }

    public bool Status1()
        {
            return status1;
        }

    public string Stringa1()
         {
            return stringa1;
          }

    public void Status1False(){

             status1 = false;

        }

    public void Status1True(){

             status1 = true;

        }

}