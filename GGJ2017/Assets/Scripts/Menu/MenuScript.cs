using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

    [SerializeField]
    Image PressAToJoinPlayer1;
    [SerializeField]
    Image PressAToJoinPlayer2;

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
            PlayerOneIsReady = !PlayerOneIsReady;
            PressAToJoinPlayer1.sprite = PlayerOneIsReady ? Ready : PressAToJoin;
        }

        if (Input.GetButtonDown("J2Dash"))
        {
            PlayerTwoIsReady = !PlayerTwoIsReady;
            PressAToJoinPlayer2.sprite = PlayerTwoIsReady ? Ready : PressAToJoin;
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
            SceneManager.LoadScene("FloSceneBase");
    }

    /*public void Quit()
    {
        Application.Quit();
    }*/

}
