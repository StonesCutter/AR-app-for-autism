using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageHelpScript : MonoBehaviour
{

    public GameObject PageHelp;
    public GameObject textHelp;
    public string textValue;
    private IntentRecognition IRC;

    void  Update() { 
    IRC = GameObject.FindObjectOfType<IntentRecognition>();
    
    //public void ShowHelp()
   //    {
   //        textValue =

        textValue = IRC.passTextHelp();
        textHelp.GetComponent<Text>().text = textValue;
      // }

      // */
     }

public void HelpOn()
    {
        PageHelp.gameObject.SetActive(true);
    }

}
