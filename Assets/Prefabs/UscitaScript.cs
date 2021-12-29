using UnityEngine;
using Vuforia;
using System.Collections;
using System.Collections.Generic;

public class UscitaScript : DefaultObserverEventHandler
{


    public ObserverBehaviour mTrackableBehaviour;
    private PortaIntMetroScript PortaIntMetro;
//    private turnstilesScript Turnstiles;
    public bool statusExit;
    public string stringaExit = "Follow the EXIT directions and reach the turnstiles";

    private turnstilesExitScript turnstilesExit;
/*
    private DiscesaScaleMobiliScript ScaleMobili;
    private ScriptBinario1_Bignami Binario1;
    private ScriptBinario2_SanSiro Binario2;
    private PortaExtMetroScript PortaExtMetro;
    private PortaIntMetroScript PortaIntMetro;
*/

    void Update()
    {
        mTrackableBehaviour = GetComponent<ObserverBehaviour>();
        PortaIntMetro = GameObject.FindObjectOfType<PortaIntMetroScript>();
        bool statoPortaIntMetro = PortaIntMetro.StatusPortaIntMetro();
        //Debug.Log("PASSAGGIO PARAMETRO E' " + stato);

        turnstilesExit = GameObject.FindObjectOfType<turnstilesExitScript>();

        if (statoPortaIntMetro == false)
        {
            mTrackableBehaviour.enabled = false;
        }
        else if (statoPortaIntMetro == true)
        {
            mTrackableBehaviour.enabled = true;
        }

/*
  void Update()
    {
        mTrackableBehaviour = GetComponent<ObserverBehaviour>();
        Turnstiles = GameObject.FindObjectOfType<turnstilesScript>();
        bool statoTurnstiles = Turnstiles.StatusTurn();
        //Debug.Log("PASSAGGIO PARAMETRO E' " + stato);

        ScaleMobili = GameObject.FindObjectOfType<DiscesaScaleMobiliScript>();
        Binario1 = GameObject.FindObjectOfType<ScriptBinario1_Bignami>();
        Binario2 = GameObject.FindObjectOfType<ScriptBinario2_SanSiro>();
        PortaExtMetro = GameObject.FindObjectOfType<PortaExtMetroScript>();
        PortaIntMetro = GameObject.FindObjectOfType<PortaIntMetroScript>();

        if (statoTurnstiles == false)
        {
            mTrackableBehaviour.enabled = false;
        }
        else if (statoTurnstiles == true)
        {
            mTrackableBehaviour.enabled = true;
        }
*/
    }

    protected override void OnTrackingFound()
        {

            statusExit = true;

            turnstilesExit.statusTurnstilesExitFalse();
/*
            ScaleMobili.statusScaleMobiliFalse();
            Binario1.statusBignamiFalse();
            Binario2.statusSanSiroFalse();
            PortaExtMetro.statusPortaExtMetroFalse();
            PortaIntMetro.statusPortaIntMetroFalse();
*/
        }

    public bool StatusExit()
        {
            return statusExit;
        }

    public string StringaExit()
        {
             return stringaExit;
         }

    public void statusExitFalse(){

             statusExit = false;

        }

    public void statusExitTrue(){

             statusExit = true;

        }

}
