using UnityEngine;
using UnityEngine.EventSystems;

public class Events : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private IntentRecognition IC;
    private Musicmgr MMGR;
    private bool myBool;

    public void OnPointerDown(PointerEventData eventData)
    {
        myBool = true;
        Debug.Log("pointer down");
        IC = GameObject.FindObjectOfType<IntentRecognition>();
        IC.StartContinuous();
        //Handheld.Vibrate();
        MMGR = GameObject.FindObjectOfType<Musicmgr>();
        MMGR.PlayMusic();

        //IC.accendiMicrofono();

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IC = GameObject.FindObjectOfType<IntentRecognition>();
        myBool = false;
        Debug.Log("pointer up");
        IC.OnDisable();
        MMGR = GameObject.FindObjectOfType<Musicmgr>();
        MMGR.StopMusic();
        //IC.spegniMicrofono();
    }

}