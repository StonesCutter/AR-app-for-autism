using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page5Script : MonoBehaviour
{

    public bool HoGiaIlBigliettoo;


    public bool StatoTickett(){
        return HoGiaIlBigliettoo;
    }


    public void TickettYes(){
        HoGiaIlBigliettoo = true;
//        Debug.Log(HoGiaIlBigliettoo);
    }

    public void TickettNo(){
        HoGiaIlBigliettoo = false;
//        Debug.Log(HoGiaIlBigliettoo);
    }
}
