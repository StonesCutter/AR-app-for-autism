using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShowImages : MonoBehaviour
{

    public GameObject buttonMetroSign;
    public GameObject buttonTicketMachine;
    public GameObject buttonTurnstile;
    public GameObject buttonTicket;
    public GameObject buttonOutMetro;
    public GameObject buttonInMetro;

    // Start is called before the first frame update
    void Start()
    {
        //buttonMetroSignFalse();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void buttonMetroSignTrue(){
        buttonMetroSign.gameObject.SetActive(true);
    }

    public void buttonMetroSignFalse(){
        buttonMetroSign.gameObject.SetActive(false);
    }

    public void buttonTicketMachineTrue(){
        buttonTicketMachine.gameObject.SetActive(true);
    }

    public void buttonTicketMachineFalse(){
        buttonTicketMachine.gameObject.SetActive(false);
    }

    public void buttonTurnstileTrue(){
        buttonTurnstile.gameObject.SetActive(true);
    }

    public void buttonTurnstileFalse(){
        buttonTurnstile.gameObject.SetActive(false);
    }

    public void buttonTicketTrue()
    {
        buttonTicket.gameObject.SetActive(true);
    }

    public void buttonTicketFalse()
    {
        buttonTicket.gameObject.SetActive(false);
    }

    public void buttonOutMetroTrue()
    {
        buttonOutMetro.gameObject.SetActive(true);
    }

    public void buttonOutMetroFalse()
    {
        buttonOutMetro.gameObject.SetActive(false);
    }

    public void buttonInMetroTrue()
    {
        buttonInMetro.gameObject.SetActive(true);
    }

    public void buttonInMetroFalse()
    {
        buttonInMetro.gameObject.SetActive(false);
    }

}
