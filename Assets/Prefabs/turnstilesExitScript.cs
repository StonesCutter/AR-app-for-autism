using UnityEngine;
using Vuforia;
using System.Collections;
using System.Collections.Generic;

public class turnstilesExitScript : DefaultObserverEventHandler
{


    public ObserverBehaviour mTrackableBehaviour;
    private PortaIntMetroScript PortaIntMetro;
    public bool statusTurnstilesExit;
    public string stringaTurnstilesExit = "Pass the turnstiles and keep following the exit signs";

    private UscitaScript Uscita;


    void Update()
    {
        mTrackableBehaviour = GetComponent<ObserverBehaviour>();
        PortaIntMetro = GameObject.FindObjectOfType<PortaIntMetroScript>();
        bool statoPortaIntMetro = PortaIntMetro.StatusPortaIntMetro();
        //Debug.Log("PASSAGGIO PARAMETRO E' " + stato);

        Uscita = GameObject.FindObjectOfType<UscitaScript>();


        if (statoPortaIntMetro == false)
        {
            mTrackableBehaviour.enabled = false;
        }
        else if (statoPortaIntMetro == true)
        {
            mTrackableBehaviour.enabled = true;
        }

    }

    protected override void OnTrackingFound()
        {

            statusTurnstilesExit = true;

            Uscita.statusExitFalse();

        }

    public bool StatusTurnstilesExit()
        {
            return statusTurnstilesExit;
        }

    public string StringaTurnstilesExit()
        {
             return stringaTurnstilesExit;
         }

    public void statusTurnstilesExitFalse(){

             statusTurnstilesExit = false;

        }

    public void statusTurnstilesExitTrue(){

             statusTurnstilesExit = true;

        }

}


