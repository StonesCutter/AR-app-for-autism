using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Page8Script : MonoBehaviour
{

    public int Start;
    public int End;
    public string Direction;
    public GameObject textDirection;
    public GameObject buttonSubmit;
    public int Differenza;
    public bool VersoBignamii;

    void Update()
    {
        Differenza = End-Start;

        if(Differenza == 0 || End == 0 || Start == 0){
            Direction = "The entered values ​​are wrong, re-enter the stations";
            textDirection.GetComponent<Text>().text = Direction;
            buttonSubmit.gameObject.SetActive(false);
        }
        if(Differenza > 0 && End != 0 && Start != 0){
            Direction = "You are going to Bignami";
            VersoBignamii = true;
            textDirection.GetComponent<Text>().text = Direction;
            buttonSubmit.gameObject.SetActive(true);
        }
        if(Differenza < 0 && End != 0 && Start != 0){
            Direction = "You are going to SanSiro";
            VersoBignamii = false;
            textDirection.GetComponent<Text>().text = Direction;
            buttonSubmit.gameObject.SetActive(true);
        }

}

    public bool StatoVersoBignami(){
        return VersoBignamii;
    }


    public void StartLocation(int valStart)
    {
        if(valStart == 0){
            Start = 0;
        }
        if(valStart == 1){
                    Start = 1;
                }
        if(valStart == 2){
                    Start = 2;
                }
        if(valStart == 3){
                    Start = 3;
                }
        if(valStart == 4){
                    Start = 4;
                }
    }

    public  void EndLocation(int valEnd)
    {
        if(valEnd == 0){
            End = 0;
        }
        if(valEnd == 1){
                    End = 1;
                }
        if(valEnd == 2){
                    End = 2;
                }
        if(valEnd == 3){
                    End = 3;
                }
        if(valEnd == 4){
                    End = 4;
                }
    }



}
