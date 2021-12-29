using UnityEngine;
using Vuforia;
using System.Collections;
using System.Collections.Generic;

public class Binario1_Bignami2 : DefaultObserverEventHandler
{

    //se si va verso bignami come direzione la variabile versoBignami è true, se si va verso San Siro è false
    public bool versoBignami;

    public ObserverBehaviour mTrackableBehaviour;
//    private DiscesaScaleMobiliScript ScaleMobili;
    private turnstilesScript Turnstiles;
    public bool statusBignami2;
    public string stringaBignami2 = "Follow the sign Binario 1 or Bignami to reach the metro. Then, target the external door.";

    private DiscesaScaleMobiliScript ScaleMobili;
    private ScriptBinario2_SanSiro Binario2;
    private PortaExtMetroScript PortaExtMetro;
    private PortaIntMetroScript PortaIntMetro;
//    private UscitaScript Uscita;

   private metroSignScript metroSign;
   private ScriptBinario1_Bignami Binario1;

       private Page8Script page8;


/*    void Update()
    {
        mTrackableBehaviour = GetComponent<ObserverBehaviour>();
        ScaleMobili = GameObject.FindObjectOfType<DiscesaScaleMobiliScript>();
        bool statoScaleMobili = ScaleMobili.StatusScaleMobili();
        //Debug.Log("PASSAGGIO PARAMETRO E' " + stato);

        if (statoScaleMobili == false && versoBignami == false)
        {
            mTrackableBehaviour.enabled = false;
        }
        else if (statoScaleMobili == true && versoBignami == true)
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
        Binario2 = GameObject.FindObjectOfType<ScriptBinario2_SanSiro>();
        PortaExtMetro = GameObject.FindObjectOfType<PortaExtMetroScript>();
        PortaIntMetro = GameObject.FindObjectOfType<PortaIntMetroScript>();
//        Uscita = GameObject.FindObjectOfType<UscitaScript>();

        metroSign = GameObject.FindObjectOfType<metroSignScript>();
        Binario1 = GameObject.FindObjectOfType<ScriptBinario1_Bignami>();

        bool statoPortaIntMetro = PortaIntMetro.StatusPortaIntMetro();

            page8 = GameObject.FindObjectOfType<Page8Script>();
            versoBignami = page8.StatoVersoBignami();



        if (statoTurnstiles == false || versoBignami == false || statoPortaIntMetro == true)
        {
            mTrackableBehaviour.enabled = false;
        }
        else if (statoTurnstiles == true && versoBignami == true)
        {
            mTrackableBehaviour.enabled = true;
        }
    }

    protected override void OnTrackingFound()
        {

            statusBignami2 = true;

            ScaleMobili.statusScaleMobiliFalse();
            Binario2.statusSanSiroFalse();
            PortaExtMetro.statusPortaExtMetroFalse();
            PortaIntMetro.statusPortaIntMetroFalse();

//            Uscita.statusExitFalse();

            metroSign.statusMetroSignFalse();
            Binario1.statusBignamiFalse();



        }

    public bool StatusBignami2()
        {
            return statusBignami2;
        }

    public string StringaBignami2()
        {
             return stringaBignami2;
         }

    public void statusBignamiFalse2(){

             statusBignami2 = false;

        }


    public void StatusBignamiTrue2(){

             statusBignami2 = true;

        }

}
