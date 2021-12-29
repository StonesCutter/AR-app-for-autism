using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Page2Script: MonoBehaviour
{

    private Page1Script Name;
    public string textValue;
    public GameObject textDisplay2;
    public GameObject textDisplay3;



    public void ShowName2()
   {
       Name = GameObject.FindObjectOfType<Page1Script>();
       textValue = Name.StringaName();
        if (textValue != "")
        {
            textDisplay3.GetComponent<Text>().text = "Welcome " + textValue + "!";
            textDisplay2.GetComponent<Text>().text = "Welcome " + textValue + "!";
        }
        else { 
        textDisplay3.GetComponent<Text>().text = "Welcome, stranger ... ";
        textDisplay2.GetComponent<Text>().text = "Welcome, stranger ... ";
        }
    }


}