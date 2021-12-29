using UnityEngine;
using Vuforia;
using System.Collections;
using System.Collections.Generic;

public class PortaExtMetroScript : DefaultObserverEventHandler
{


    public ObserverBehaviour mTrackableBehaviour;
//    private ScriptBinario1_Bignami Bignami;
//    private ScriptBinario2_SanSiro SanSiro;
    private turnstilesScript Turnstiles;
    public bool statusPortaExtMetro;
    public string stringaPortaExtMetro = "Fantastic! When the metro arrives catch it and target the inside door.";

    private DiscesaScaleMobiliScript ScaleMobili;
    private ScriptBinario1_Bignami Binario1;
    private ScriptBinario2_SanSiro Binario2;
    private PortaIntMetroScript PortaIntMetro;
//    private UscitaScript Uscita;

   private metroSignScript metroSign;
       private Binario1_Bignami2 Bignami2;


/*    void Update()
    {
        mTrackableBehaviour = GetComponent<ObserverBehaviour>();
        Bignami = GameObject.FindObjectOfType<ScriptBinario1_Bignami>();
        bool statoBignami = Bignami.StatusBignami();
        SanSiro = GameObject.FindObjectOfType<ScriptBinario2_SanSiro>();
        bool statoSanSiro = SanSiro.StatusSanSiro();
        //Debug.Log("PASSAGGIO PARAMETRO E' " + stato);

        if (statoSanSiro == false && statoBignami == false)
        {
            mTrackableBehaviour.enabled = false;
        }
        else if (statoBignami == true || statoSanSiro == true)
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
        PortaIntMetro = GameObject.FindObjectOfType<PortaIntMetroScript>();
        metroSign = GameObject.FindObjectOfType<metroSignScript>();
        Bignami2 = GameObject.FindObjectOfType<Binario1_Bignami2>();
//        Uscita = GameObject.FindObjectOfType<UscitaScript>();

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

            statusPortaExtMetro = true;

            ScaleMobili.statusScaleMobiliFalse();
            Binario1.statusBignamiFalse();
            Binario2.statusSanSiroFalse();
            PortaIntMetro.statusPortaIntMetroFalse();
//            Uscita.statusExitFalse();
            metroSign.statusMetroSignFalse();
            Bignami2.statusBignamiFalse2();

        }

    public bool StatusPortaExtMetro()
        {
            return statusPortaExtMetro;
        }

    public string StringaPortaExtMetro()
        {
             return stringaPortaExtMetro;
         }

    public void statusPortaExtMetroFalse(){

             statusPortaExtMetro = false;

        }

    public void statusPortaExtMetroTrue(){

             statusPortaExtMetro = true;

        }

}


