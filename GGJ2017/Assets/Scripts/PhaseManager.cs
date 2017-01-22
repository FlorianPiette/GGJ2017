using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
	public static PhaseManager Instance;
    public bool isInitiate = false;

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
        Players[0].phase = Phase.Offense;
        Players[1].phase = Phase.Offense;
    }

    public void InitiateWinner(string winner)
    {
        
        if (winner == "p0")
        {
            Players[0].phase = Phase.Offense;
            Players[1].phase = Phase.Defense;
            ManaSpawners[0].GetComponent<ManaSpawner>().enabled = !ManaSpawners[0].GetComponent<ManaSpawner>().enabled;
        }
        else
        {
            Players[1].phase = Phase.Offense;
            Players[0].phase = Phase.Defense;
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
			}
			else if (Players[1].phase == Phase.Offense || Players[1].phase == Phase.HeartAttack)
			{
				Players[0].phase = Phase.Offense;
				Players[1].phase = Phase.Defense;
			}
			ManaSpawners[0].GetComponent<ManaSpawner>().enabled = !ManaSpawners[0].GetComponent<ManaSpawner>().enabled;
			ManaSpawners[1].GetComponent<ManaSpawner>().enabled = !ManaSpawners[1].GetComponent<ManaSpawner>().enabled;
		}
		else
		{
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
