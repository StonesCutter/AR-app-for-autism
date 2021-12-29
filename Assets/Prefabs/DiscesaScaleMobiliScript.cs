using UnityEngine;
using Vuforia;
using System.Collections;
using System.Collections.Generic;

public class DiscesaScaleMobiliScript : DefaultObserverEventHandler
{
    public ObserverBehaviour mTrackableBehaviour;
    private turnstilesScript Turnstiles;
    public bool statusScaleMobili;
    public string stringaScaleMobili = "Insert the ticket in the turnstile and then follow the direction of the arrow to reach the metro";

    private ScriptBinario1_Bignami Binario1;
    private ScriptBinario2_SanSiro Binario2;
    private PortaExtMetroScript PortaExtMetro;
    private PortaIntMetroScript PortaIntMetro;
//    private UscitaScript Uscita;

       private metroSignScript metroSign;
       private Binario1_Bignami2 Bignami2;



    void Update()
    {
        mTrackableBehaviour = GetComponent<ObserverBehaviour>();
        Turnstiles = GameObject.FindObjectOfType<turnstilesScript>();
        bool statoTurnstiles = Turnstiles.StatusTurn();
        //Debug.Log("PASSAGGIO PARAMETRO E' " + stato);

        Binario1 = GameObject.FindObjectOfType<ScriptBinario1_Bignami>();
        Binario2 = GameObject.FindObjectOfType<ScriptBinario2_SanSiro>();
        PortaExtMetro = GameObject.FindObjectOfType<PortaExtMetroScript>();
        PortaIntMetro = GameObject.FindObjectOfType<PortaIntMetroScript>();
//        Uscita = GameObject.FindObjectOfType<UscitaScript>();

        metroSign = GameObject.FindObjectOfType<metroSignScript>();
        Bignami2 = GameObject.FindObjectOfType<Binario1_Bignami2>();

        bool statoPortaIntMetro = PortaIntMetro.StatusPortaIntMetro();

        if (statoTurnstiles == false || statoPortaIntMetro == true)
        {
            mTrackableBehaviour.enabled = false;
        }
        else if (statoTurnstiles == true)
        {
            mTrackableBehaviour.enabled = true;
        }

    }

    protected override void OnTrackingFound()
        {

            statusScaleMobili = true;

            Binario1.statusBignamiFalse();
            Binario2.statusSanSiroFalse();
            PortaExtMetro.statusPortaExtMetroFalse();
            PortaIntMetro.statusPortaIntMetroFalse();
            
//            Uscita.statusExitFalse();

            metroSign.statusMetroSignFalse();
            Bignami2.statusBignamiFalse2();

        }

    public bool StatusScaleMobili()
        {
            return statusScaleMobili;
        }

    public string StringaScaleMobili()
        {
             return stringaScaleMobili;
         }

    public void statusScaleMobiliFalse(){

             statusScaleMobili = false;

        }


    public void StatusScaleMobiliTrue(){

             statusScaleMobili = true;

        }

}





