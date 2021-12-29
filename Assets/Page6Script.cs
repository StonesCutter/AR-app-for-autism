using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page6Script : MonoBehaviour
{

    public bool theCard;


    public bool StatoCard(){
        return theCard;
    }


    public void Card(){
        theCard = true;
//        Debug.Log(theCard);
    }

    public void Cash(){
        theCard = false;
//        Debug.Log(theCard);
    }
}