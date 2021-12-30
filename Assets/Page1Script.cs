using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Page1Script : MonoBehaviour
{
    public string Name;
    public GameObject  inputField;
    public string stringaName;


    public void StoreName()
    {
        Name = inputField.GetComponent<Text>().text;
        Debug.Log("Text: " + Name);

        stringaName = Name;



    }

    public string StringaName()
    {
        return stringaName;
    }

    //Prova



}