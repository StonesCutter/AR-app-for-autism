using UnityEngine;
using Vuforia;
using System.Collections;
using System.Collections.Generic;

public class ScriptImageTarget5_Card : DefaultObserverEventHandler
{

    public ObserverBehaviour mTrackableBehaviour;
    private ScriptImageTarget4 SIT4;
    public bool status5_Card;
    public string stringa5_Card = "seleziona 'Card'";

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

        if (stato4 == false && Card == false)
        {
            mTrackableBehaviour.enabled = false;
        }
        else if (stato4 == true && Card == true)
        {
            mTrackableBehaviour.enabled = true;
        }

    }

    protected override void OnTrackingFound()
        {

            status5_Card = true;

        }

    public bool Status5_Card()
        {
            return status5_Card;
        }

    public string Stringa5_Card()
        {
             return stringa5_Card;
         }

    public void Status5_CardFalse(){

             status5_Card = false;

        }

    public void Status5_CardTrue(){

             status5_Card = true;

        }

}