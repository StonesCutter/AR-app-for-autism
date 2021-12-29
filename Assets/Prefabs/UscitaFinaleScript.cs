using UnityEngine;
using Vuforia;
using System.Collections;
using System.Collections.Generic;

public class UscitaFinaleScript : DefaultObserverEventHandler
{


    public ObserverBehaviour mTrackableBehaviour;
    private turnstilesExitScript TurnstilesExit;
    public bool statusUscitaFinale;
    public string stringaUscitaFinale = "Great, keep following the signs to reach the exit";

    private metroSignExit metroExit;


    void Update()
    {
        mTrackableBehaviour = GetComponent<ObserverBehaviour>();
        TurnstilesExit = GameObject.FindObjectOfType<turnstilesExitScript>();
        bool statoTurnstilesExit = TurnstilesExit.StatusTurnstilesExit();
        //Debug.Log("PASSAGGIO PARAMETRO E' " + stato);

        metroExit = GameObject.FindObjectOfType<metroSignExit>();

        if (statoTurnstilesExit == false)
        {
            mTrackableBehaviour.enabled = false;
        }
        else if (statoTurnstilesExit == true)
        {
            mTrackableBehaviour.enabled = true;
        }

    }

    protected override void OnTrackingFound()
        {

            statusUscitaFinale = true;

            metroExit.statusMetroSignExitFalse();

        }

    public bool StatusUscitaFinale()
        {
            return statusUscitaFinale;
        }

    public string StringaUscitaFinale()
        {
             return stringaUscitaFinale;
         }

    public void statusUscitaFinaleFalse(){

             statusUscitaFinale = false;

        }

    public void statusUscitaFinaleTrue(){

             statusUscitaFinale = true;

        }

}
