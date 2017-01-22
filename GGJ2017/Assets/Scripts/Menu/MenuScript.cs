using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

    [SerializeField]
    Text PressAToJoinPlayer1;
    [SerializeField]
    Text PressAToJoinPlayer2;

    [SerializeField]
    Sprite PressAToJoin;
    [SerializeField]
    Sprite Ready;

    [SerializeField]
    Text StartInText;

    bool PlayerOneIsReady = false;
    bool PlayerTwoIsReady = false;

    float CountDown = 4;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("J1Dash"))
        {
            PlayerOneIsReady = true;
            PressAToJoinPlayer1.text = PlayerOneIsReady ? "Ready !" : "Press A to join";
        }

        if (Input.GetButtonDown("J2Dash"))
        {
            PlayerTwoIsReady = true;
            PressAToJoinPlayer2.text = PlayerTwoIsReady ? "Ready !" : "Press A to join";
        }
       
        if (PlayerTwoIsReady && PlayerOneIsReady)
        {
            StartInText.text = "Start in " + (int)CountDown;
            CountDown -= Time.deltaTime;
        }
        else
        {
            CountDown = 4;
            StartInText.text = " Waiting for players";
        }

        if (CountDown < 1)
            SceneManager.LoadScene("Arena");
    }

    /*public void Quit()
    {
        Application.Quit();
    }*/

}
