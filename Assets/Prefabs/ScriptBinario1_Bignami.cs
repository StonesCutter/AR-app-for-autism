using UnityEngine;
using Vuforia;
using System.Collections;
using System.Collections.Generic;

public class ScriptBinario1_Bignami : DefaultObserverEventHandler
{

    //se si va verso bignami come direzione la variabile versoBignami è true, se si va verso San Siro è false
    public bool versoBignami;

    public ObserverBehaviour mTrackableBehaviour;
//    private DiscesaScaleMobiliScript ScaleMobili;
    private turnstilesScript Turnstiles;
    public bool statusBignami;
    public string stringaBignami = "Follow the sign Binario 1 or Bignami to reach the metro. Then, target the external door.";

    private DiscesaScaleMobiliScript ScaleMobili;
    private ScriptBinario2_SanSiro Binario2;
    private PortaExtMetroScript PortaExtMetro;
    private PortaIntMetroScript PortaIntMetro;
//    private UscitaScript Uscita;

   private metroSignScript metroSign;
       private Binario1_Bignami2 Bignami2;

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
        Bignami2 = GameObject.FindObjectOfType<Binario1_Bignami2>();


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

            statusBignami = true;

            ScaleMobili.statusScaleMobiliFalse();
            Binario2.statusSanSiroFalse();
            PortaExtMetro.statusPortaExtMetroFalse();
            PortaIntMetro.statusPortaIntMetroFalse();

//            Uscita.statusExitFalse();

            metroSign.statusMetroSignFalse();
            Bignami2.statusBignamiFalse2();



        }

    public bool StatusBignami()
        {
            return statusBignami;
        }

    public string StringaBignami()
        {
             return stringaBignami;
         }

    public void statusBignamiFalse(){

             statusBignami = false;

        }


    public void StatusBignamiTrue(){

             statusBignami = true;

        }

}



