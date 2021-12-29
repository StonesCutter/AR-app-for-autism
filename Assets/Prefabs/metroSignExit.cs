using UnityEngine;
using Vuforia;
using System.Collections;
using System.Collections.Generic;

public class metroSignExit : DefaultObserverEventHandler
{


    public ObserverBehaviour mTrackableBehaviour;
    private turnstilesExitScript TurnstilesExit;
    public bool statusMetroSignExit;
    public string stringaMetroSignExit = "Congratulations! You have completed your travel in the metro!";

    private UscitaFinaleScript UscitaFinale;



    void Update()
    {
        mTrackableBehaviour = GetComponent<ObserverBehaviour>();
        TurnstilesExit = GameObject.FindObjectOfType<turnstilesExitScript>();
        bool statoTurnstilesExit = TurnstilesExit.StatusTurnstilesExit();
        //Debug.Log("PASSAGGIO PARAMETRO E' " + stato);

        UscitaFinale = GameObject.FindObjectOfType<UscitaFinaleScript>();

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

            statusMetroSignExit = true;

            UscitaFinale.statusUscitaFinaleFalse();

        }

    public bool StatusMetroSignExit()
        {
            return statusMetroSignExit;
        }

    public string StringaMetroSignExit()
        {
             return stringaMetroSignExit;
         }

    public void statusMetroSignExitFalse(){

             statusMetroSignExit = false;

        }

    public void statusMetroSignExitTrue(){

             statusMetroSignExit = true;

        }

}
