using UnityEngine;
using Vuforia;
using System.Collections;
using System.Collections.Generic;

public class ticketMachineScript : DefaultObserverEventHandler
{

    public ObserverBehaviour mTrackableBehaviour;
    private metroSignScript MetroSign;
    public bool status;
    public string stringa0 = "Well done! Now follow the instruftions to purchase the ticket";

    private ScriptEntrata Entrata;

    // public bool HoGiaIlBiglietto = prendo il valore da UI, metterlo nell else if come && nello stato false;
    public bool HoGiaIlBiglietto;
    private turnstilesScript turnstiles;

        private Page5Script page5;


    void Update()
    {
        mTrackableBehaviour = GetComponent<ObserverBehaviour>();
        MetroSign = GameObject.FindObjectOfType<metroSignScript>();
        bool statoMetroSign = MetroSign.StatusMetroSign();

        Entrata = GameObject.FindObjectOfType<ScriptEntrata>();

        turnstiles = GameObject.FindObjectOfType<turnstilesScript>();
        bool stopTicketMachine = turnstiles.Stopp();

                page5 = GameObject.FindObjectOfType<Page5Script>();
                HoGiaIlBiglietto = page5.StatoTickett();

        if (statoMetroSign == false || HoGiaIlBiglietto == true || stopTicketMachine == true)
        {
            mTrackableBehaviour.enabled = false;
        }
        else if (statoMetroSign == true  && HoGiaIlBiglietto == false && stopTicketMachine == false)
        {
            mTrackableBehaviour.enabled = true;

        }

    }

    protected override void OnTrackingFound()
    {

        status = true;
        Entrata.statusEntrataFalse();

    }

    public bool Status()
    {
        return status;
    }

    public string Stringa0()
             {
                return stringa0;
              }

    public void statusFalse(){

             status = false;

        }

    public void statusTrue(){

             status = true;

        }



}
