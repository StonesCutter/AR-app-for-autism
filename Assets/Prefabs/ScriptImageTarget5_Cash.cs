using UnityEngine;
using Vuforia;
using System.Collections;
using System.Collections.Generic;

public class ScriptImageTarget5_Cash : DefaultObserverEventHandler
{

    public ObserverBehaviour mTrackableBehaviour;
    private ScriptImageTarget4 SIT4;
    public bool status5_Cash;
    public string stringa5_Cash = "seleziona 'Cash'";

        public bool Card;

            private Page6Script page6;

    void Update()
    {
        mTrackableBehaviour = GetComponent<ObserverBehaviour>();
        SIT4 = GameObject.FindObjectOfType<ScriptImageTarget4>();
        bool stato4 = SIT4.Status4();
        //Debug.Log("PASSAGGIO PARAMETRO E' " + stato);

            page6 = GameObject.FindObjectOfType<Page6Script>();
            Card = page6.StatoCard();

        if (stato4 == false && Card == true)
        {
            mTrackableBehaviour.enabled = false;
        }
        else if (stato4 == true && Card == false)
        {
            mTrackableBehaviour.enabled = true;
        }

    }

    protected override void OnTrackingFound()
        {

            status5_Cash = true;

        }

    public bool Status5_Cash()
        {
            return status5_Cash;
        }

    public string Stringa5_Cash()
        {
             return stringa5_Cash;
         }

    public void Status5_CashFalse(){

             status5_Cash = false;

        }

    public void Status5_CashTrue(){

             status5_Cash = true;

        }

}
