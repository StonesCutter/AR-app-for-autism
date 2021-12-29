using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetroIMG : MonoBehaviour
{
    public GameObject metroSignImg;
    // Start is called before the first frame update
    void Start()
    {
        metroSignImg = GameObject.Find("metroSignImage");
        turnOff();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void turnOn()
    {
        metroSignImg.SetActive(true);
       
    }

    public void turnOff()
    {
        metroSignImg.SetActive(false); 
    }

}
