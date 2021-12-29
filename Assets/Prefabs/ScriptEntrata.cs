using UnityEngine;
using Vuforia;
using System.Collections;
using System.Collections.Generic;

public class ScriptEntrata : DefaultObserverEventHandler
{

    public ObserverBehaviour mTrackableBehaviour;
    private metroSignScript MetroSign;
    public bool statusEntrata;
    public string stringaEntrata;

    private ticketMachineScript ticketMachine;

    public bool HoGiaIlBiglietto;

        private Page5Script page5;


    void Update()
    {

        page5 = GameObject.FindObjectOfType<Page5Script>();
        HoGiaIlBiglietto = page5.StatoTickett();

    if(HoGiaIlBiglietto == true){
        stringaEntrata = "Go down the stairs and find the turnstile. Can you find it?";
    }

    else if(HoGiaIlBiglietto == false){
            stringaEntrata = "Go down the stairs and find the ticket machine. Can you find it?";
        }
        mTrackableBehaviour = GetComponent<ObserverBehaviour>();
        MetroSign = GameObject.FindObjectOfType<metroSignScript>();
        bool statoMetroSign = MetroSign.StatusMetroSign();

        ticketMachine = GameObject.FindObjectOfType<ticketMachineScript>();


        if (statoMetroSign == false)
        {
            mTrackableBehaviour.enabled = false;
        }
        else if (statoMetroSign == true)
        {
            mTrackableBehaviour.enabled = true;

        }

    }

    protected override void OnTrackingFound()
        {

            statusEntrata = true;
            ticketMachine.statusFalse();


        }

    public bool StatusEntrata()
        {
            return statusEntrata;
        }

    public string StringaEntrata()
         {
            return stringaEntrata;
          }

    public void statusEntrataFalse(){

             statusEntrata = false;

        }

    public void statusEntrataTrue(){

             statusEntrata = true;

        }

}