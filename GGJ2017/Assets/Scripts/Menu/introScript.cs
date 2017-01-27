using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class introScript : MonoBehaviour
{
	[SerializeField]
	public GameObject Titre;
	[SerializeField]
	public GameObject Commandes;
	[SerializeField]
	public GameObject Regles;
	[SerializeField]
	public GameObject Pass;

    bool titre = true;
    bool commandes = true;
    bool regles = true;
	bool menuManagerIsOn = false;
	
	void Update ()
	{
        if (Input.GetButtonDown("J1Dash") && titre)
        {
			Titre.SetActive(false);
            titre = false;
        }
        else if (Input.GetButtonDown("J1Dash") && commandes)
        {
			Commandes.SetActive(false);
            commandes = false;
        }
        else if (Input.GetButtonDown("J1Dash") && regles)
        {
			Regles.SetActive(false);
            regles = false;
        }
        /*else if (Input.GetButtonDown("J1Action"))
        {
            titre = false;
            commandes = false;
            regles = false;
        }*/
		if (!titre && !commandes && !regles && !menuManagerIsOn)
        {
			Pass.SetActive(false);
			menuManagerIsOn = true;
            //SceneManager.LoadScene("Menu");
        }
		if(menuManagerIsOn)
		{
			gameObject.GetComponent<MenuScript>().enabled = true;
			gameObject.GetComponent<introScript>().enabled = false;
		}
    }
}
