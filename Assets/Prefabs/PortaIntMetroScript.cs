using UnityEngine;
using Vuforia;
using System.Collections;
using System.Collections.Generic;

public class PortaIntMetroScript : DefaultObserverEventHandler
{


    public ObserverBehaviour mTrackableBehaviour;
//    private PortaExtMetroScript PortaExtMetro;
    private turnstilesScript Turnstiles;
    public bool statusPortaIntMetro = false;
    public string stringaPortaIntMetro = "Well done, exit at the right stop and follow the exit signs to go upstairs";

    private DiscesaScaleMobiliScript ScaleMobili;
    private ScriptBinario1_Bignami Binario1;
    private ScriptBinario2_SanSiro Binario2;
    private PortaExtMetroScript PortaExtMetro;

    private metroSignScript metroSign;
       private Binario1_Bignami2 Bignami2;
    //    private UscitaScript Uscita;
    public bool pass = true;


    /*    void Update()
        {
            mTrackableBehaviour = GetComponent<ObserverBehaviour>();
            PortaExtMetro = GameObject.FindObjectOfType<PortaExtMetroScript>();
            bool statoPortaExtMetro = PortaExtMetro.StatusPortaExtMetro();
            //Debug.Log("PASSAGGIO PARAMETRO E' " + stato);

            if (statoPortaExtMetro == false)
            {
                mTrackableBehaviour.enabled = false;
            }
            else if (statoPortaExtMetro == true)
            {
                mTrackableBehaviour.enabled = true;
            }
    */
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
//        Uscita = GameObject.FindObjectOfType<UscitaScript>();

        metroSign = GameObject.FindObjectOfType<metroSignScript>();
        Bignami2 = GameObject.FindObjectOfType<Binario1_Bignami2>();

        if (statoTurnstiles == false)
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

            statusPortaIntMetro = true;

            ScaleMobili.statusScaleMobiliFalse();
            Binario1.statusBignamiFalse();
            Binario2.statusSanSiroFalse();
            PortaExtMetro.statusPortaExtMetroFalse();
//            Uscita.statusExitFalse();

            metroSign.statusMetroSignFalse();
            Bignami2.statusBignamiFalse2();
        pass = false;

    }

    public bool StatusPortaIntMetro()
        {
            return statusPortaIntMetro;
        }

    public string StringaPortaIntMetro()
        {
             return stringaPortaIntMetro;
         }

    public void statusPortaIntMetroFalse(){

             statusPortaIntMetro = false;

        }

    public void statusPortaIntMetroTrue(){

             statusPortaIntMetro = true;

        }

    public bool Pass()
    {
        return pass;
    }

}



