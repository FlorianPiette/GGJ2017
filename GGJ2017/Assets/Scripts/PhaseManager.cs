using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
	public static PhaseManager Instance;
    public bool isInitiate = false;
    public bool startingPhase = false; //La phase pendant laquelle les 2 joueurs sont en défense.

    public GameObject[] mursP1;
    public GameObject[] mursP2;

    public enum Phase
	{
		None,
		Offense,
		Defense,
		HeartAttack
	}

	public PlayerScript[] Players;
	public ManaSpawner[] ManaSpawners;
	public float manaMaxLimitToSwitch = 100;

    public bool DebugIsOn = false;

    void Awake ()
	{
		Instance = this;
		InitiatePhase();
	}
	
	void Update ()
	{
		//Debug 
		if (Input.GetKeyDown(KeyCode.P) && DebugIsOn)
		{
            Debug.LogError("DEBUG MODE ACTIVATED : invert phase");
			AttributePhase();
		}
	}
    public void InitiatePhase()
    {
        isInitiate = false;
        startingPhase = true;
        Players[0].phase = Phase.Offense;
        Players[1].phase = Phase.Offense;

        foreach (GameObject mur in mursP1)
        {
            mur.GetComponent<ShieldScript>().DeactivateShield();
        }

        foreach (GameObject mur in mursP2)
        {
            mur.GetComponent<ShieldScript>().DeactivateShield();
        }
    }

    public void InitiateWinner(string winner)
    {
        if (winner == "p1")
        {
            Players[1].phase = Phase.Offense;
            Players[0].phase = Phase.Defense;

            Players[0].manaCount = 0f;
            Players[1].manaCount = Players[0].manaMax;

            foreach (GameObject mur in mursP1)
            {
                mur.GetComponent<ShieldScript>().ActivateShield();
            }

            ManaSpawners[0].GetComponent<ManaSpawner>().enabled = !ManaSpawners[0].GetComponent<ManaSpawner>().enabled;
        }
        else
        {
            Players[0].phase = Phase.Offense;
            Players[1].phase = Phase.Defense;

            Players[1].manaCount = 0f;
            Players[0].manaCount = Players[0].manaMax;

            foreach (GameObject mur in mursP2)
            {
                mur.GetComponent<ShieldScript>().ActivateShield();
            }

            ManaSpawners[1].GetComponent<ManaSpawner>().enabled = !ManaSpawners[1].GetComponent<ManaSpawner>().enabled;
        }
        isInitiate = true;
    }

    public void AttributePhase()
    {
        if (Players[0].phase != Phase.None)
		{
			if (Players[0].phase == Phase.Offense || Players[0].phase == Phase.HeartAttack)
			{
				Players[0].phase = Phase.Defense;
				Players[1].phase = Phase.Offense;

                Players[0].manaCount = 0f;
                Players[1].manaCount = Players[1].manaMax;
                
                foreach (GameObject mur in mursP1)
                {
                    mur.GetComponent<ShieldScript>().ActivateShield();
                }

                foreach (GameObject mur in mursP2)
                {
                    mur.GetComponent<ShieldScript>().DeactivateShield();
                }

            }
			else if (Players[1].phase == Phase.Offense || Players[1].phase == Phase.HeartAttack)
			{
				Players[0].phase = Phase.Offense;
				Players[1].phase = Phase.Defense;
                
                Players[1].manaCount = 0f;
                Players[0].manaCount = Players[0].manaMax;
                
                foreach (GameObject mur in mursP1)
                {
                    mur.GetComponent<ShieldScript>().ActivateShield();
                }

                foreach (GameObject mur in mursP2)
                {
                    mur.GetComponent<ShieldScript>().DeactivateShield();
                }
            }
            
            var children = new List<GameObject>();
            foreach (Transform child in ManaSpawners[0].transform)
            {
                children.Add(child.gameObject);
            }
            children.ForEach(child => Destroy(child));

            children = new List<GameObject>();
            foreach (Transform child in ManaSpawners[1].transform)
            {
                children.Add(child.gameObject);
            }
            children.ForEach(child => Destroy(child));

            ManaSpawners[0].GetComponent<ManaSpawner>().enabled = !ManaSpawners[0].GetComponent<ManaSpawner>().enabled;
			ManaSpawners[1].GetComponent<ManaSpawner>().enabled = !ManaSpawners[1].GetComponent<ManaSpawner>().enabled;
		}
		else
		{
            Debug.LogError("Should never be triggered; instead use the IniatiatePhase");
			int rnd = Random.Range(1, 3);
			if(rnd == 1)
			{
				Players[0].phase = Phase.Offense;
				Players[1].phase = Phase.Defense;
				ManaSpawners[1].GetComponent<ManaSpawner>().enabled = true;
			}
			else
			{
				Players[0].phase = Phase.Defense;
				Players[1].phase = Phase.Offense;
				ManaSpawners[0].GetComponent<ManaSpawner>().enabled = true;
			}
		}
		
	}
}
