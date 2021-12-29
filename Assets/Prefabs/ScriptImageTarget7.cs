using UnityEngine;
using Vuforia;
using System.Collections;
using System.Collections.Generic;

public class ScriptImageTarget7 : DefaultObserverEventHandler

{
    public ObserverBehaviour mTrackableBehaviour;
    private ScriptImageTarget5_Card SIT5_Card;
    public bool status7;
    public string stringa7 = "insert the card in the machine, get the ticket and frame it with the phone";



    void Update()
    {
        mTrackableBehaviour = GetComponent<ObserverBehaviour>();
        SIT5_Card = GameObject.FindObjectOfType<ScriptImageTarget5_Card>();
        bool stato5_Card = SIT5_Card.Status5_Card();


        if (stato5_Card == false)
        {
            mTrackableBehaviour.enabled = false;
        }
        else if (stato5_Card == true)
        {
            mTrackableBehaviour.enabled = true;

        }

    }

    protected override void OnTrackingFound()
        {

            status7 = true;

        }

    public bool Status7()
        {
            return status7;
        }

    public string Stringa7()
         {
            return stringa7;
          }

    public void Status7False(){

             status7 = false;

        }

    public void Status7True(){

             status7 = true;

        }

}