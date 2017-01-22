using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class introScript : MonoBehaviour {

    bool titre = true;
    bool commandes = true;
    bool regles = true;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("J1Dash") && titre)
        {
            gameObject.transform.GetChild(3).gameObject.SetActive(false);
            titre = false;
        }
        else if (Input.GetButtonDown("J1Dash") && commandes)
        {
            gameObject.transform.GetChild(2).gameObject.SetActive(false);
            commandes = false;
        }
        else if (Input.GetButtonDown("J1Dash") && regles)
        {
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            regles = false;
        }
        else if (Input.GetButtonDown("J1Action"))
        {
            titre = false;
            commandes = false;
            regles = false;
        }
        if (!titre && !commandes && !regles)
        {
            gameObject.transform.GetChild(4).gameObject.SetActive(false);
            SceneManager.LoadScene("Menu");
        }
    }
}
