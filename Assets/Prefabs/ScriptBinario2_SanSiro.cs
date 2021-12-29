using UnityEngine;
using Vuforia;
using System.Collections;
using System.Collections.Generic;

public class ScriptBinario2_SanSiro : DefaultObserverEventHandler
{

    //se si va verso bignami come direzione la variabile versoBignami è true, se si va verso San Siro è false
    public bool versoBignami;

    public ObserverBehaviour mTrackableBehaviour;
//    private DiscesaScaleMobiliScript ScaleMobili;
    private turnstilesScript Turnstiles;
    public bool statusSanSiro;
    public string stringaSanSiro = "Follow the sign Binario 2 or San Siro to reach the metro. Then, target the external door.";

    private DiscesaScaleMobiliScript ScaleMobili;
    private ScriptBinario1_Bignami Binario1;
    private PortaExtMetroScript PortaExtMetro;
    private PortaIntMetroScript PortaIntMetro;
//    private UscitaScript Uscita;

              private Page8Script page8;


/*    void Update()
    {
        mTrackableBehaviour = GetComponent<ObserverBehaviour>();
        ScaleMobili = GameObject.FindObjectOfType<DiscesaScaleMobiliScript>();
        bool statoScaleMobili = ScaleMobili.StatusScaleMobili();
        //Debug.Log("PASSAGGIO PARAMETRO E' " + stato);

        if (statoScaleMobili == false  && versoBignami == true)
        {
            mTrackableBehaviour.enabled = false;
        }
        else if (statoScaleMobili == true && versoBignami == false)
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
        PortaExtMetro = GameObject.FindObjectOfType<PortaExtMetroScript>();
        PortaIntMetro = GameObject.FindObjectOfType<PortaIntMetroScript>();
//        Uscita = GameObject.FindObjectOfType<UscitaScript>();

        bool statoPortaIntMetro = PortaIntMetro.StatusPortaIntMetro();


            page8 = GameObject.FindObjectOfType<Page8Script>();
            versoBignami = page8.StatoVersoBignami();

        if (statoTurnstiles == false && versoBignami == true || statoPortaIntMetro == true)
        {
            mTrackableBehaviour.enabled = false;
        }
        else if (statoTurnstiles == true && versoBignami == false)
        {
            mTrackableBehaviour.enabled = true;
        }
    }

    protected override void OnTrackingFound()
        {

            statusSanSiro = true;

            ScaleMobili.statusScaleMobiliFalse();
            Binario1.statusBignamiFalse();
            PortaExtMetro.statusPortaExtMetroFalse();
            PortaIntMetro.statusPortaIntMetroFalse();
//            Uscita.statusExitFalse();

        }

    public bool StatusSanSiro()
        {
            return statusSanSiro;
        }

    public string StringaSanSiro()
        {
             return stringaSanSiro;
         }

    public void statusSanSiroFalse(){

             statusSanSiro = false;

        }


    public void StatusSanSiroTrue(){

             statusSanSiro = true;

        }

}