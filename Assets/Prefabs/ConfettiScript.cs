using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfettiScript : MonoBehaviour
{
    [SerializeField] ParticleSystem collectParticle = null;
    public GameObject RobotAngry;
    public GameObject RobotHappy;
    public GameObject RobotNormal;
    public GameObject RobotReward;

    private void Update(){

        if(Input.GetKeyDown(KeyCode.Space)){
            Collect();

        }

    }

    public void Collect(){
        collectParticle.Play();
        //Debug.Log("ciao");
        RobotNormal.gameObject.SetActive(false);
        RobotAngry.gameObject.SetActive(false);
        RobotHappy.gameObject.SetActive(false);
        RobotReward.gameObject.SetActive(true);

    }

}