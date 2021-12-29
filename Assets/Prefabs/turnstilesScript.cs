using UnityEngine;
using Vuforia;
using System.Collections;
using System.Collections.Generic;

public class turnstilesScript : DefaultObserverEventHandler
{

    public GameObject RobotAngry;
    public GameObject RobotHappy;
    public GameObject RobotNormal;
    public GameObject RobotReward;

    public ObserverBehaviour mTrackableBehaviour;
    private ScriptImageTargetTicket SITT;
    public bool statusTurn = false;
    public string stringaTurn = "Perfect, insert the ticket where is the green arrow and retrieve it where the red one is.  Then, find the correct sign direction. Can you do it?";
    private IntentRecognition IRC;

    // public bool HoGiaIlBiglietto = prendo il valore da UI, metterlo nell if come || nrllo ststo true;
    public bool HoGiaIlBiglietto;

   private metroSignScript metroSign;
//    private ticketMachineScript ticketMachine;

   public bool StopTicket = false;

           private Page5Script page5;

    void Update()
    {
        IRC = GameObject.FindObjectOfType<IntentRecognition>();
        mTrackableBehaviour = GetComponent<ObserverBehaviour>();
        SITT = GameObject.FindObjectOfType<ScriptImageTargetTicket>();
        bool statoTicket = SITT.StatusTicket();
        //Debug.Log("PASSAGGIO PARAMETRO E' " + stato);

        metroSign = GameObject.FindObjectOfType<metroSignScript>();
        bool statoMetroSign = metroSign.StatusMetroSign();

//        ticketMachine = GameObject.FindObjectOfType<ticketMachineScript>();

//        Debug.Log(statoMetroSign);

                page5 = GameObject.FindObjectOfType<Page5Script>();
                HoGiaIlBiglietto = page5.StatoTickett();

        if (statoTicket == false && HoGiaIlBiglietto == false || statoMetroSign == false)
        {
            mTrackableBehaviour.enabled = false;
        }
        if (statoMetroSign == true)
        {
                if(HoGiaIlBiglietto == true || statoTicket == true){
            mTrackableBehaviour.enabled = true;
            }

        }

    }

    protected override void OnTrackingFound()
        {

            statusTurn = true;
//            ticketMachine.statusFalse();
            StopTicket = true;
        // IRC.turnRobotHappyOn();
        //RobotNormal.gameObject.SetActive(false);
        //RobotAngry.gameObject.SetActive(false);
        //RobotHappy.gameObject.SetActive(false);
       // RobotReward.gameObject.SetActive(true);



    }

    public bool StatusTurn()
        {
            return statusTurn;
        }

    public string StringaTurn()
        {
             return stringaTurn;
         }

    public void StatusTurnFalse(){

             statusTurn = false;

        }

    public void StatusTurnTrue(){

             statusTurn = true;

        }

        public bool Stopp(){
            return StopTicket;
        }

    public bool TicketAlreadyHave()
    {
        return HoGiaIlBiglietto;
    }

}
