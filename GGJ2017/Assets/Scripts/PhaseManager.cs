using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
	public static PhaseManager Instance;

	public enum Phase
	{
		None,
		Offense,
		Defense,
		HeartAttack
	}

	public PlayerScript[] Players;
	public ManaSpawner[] ManaSpawners;

	void Awake ()
	{
		Instance = this;
		AttributePhase();
	}
	
	void Update ()
	{
		//Debug 
		if(Input.GetKeyDown(KeyCode.Return))
		{
			AttributePhase();
		}
	}

	public void AttributePhase()
	{
		if(Players[0].phase != Phase.None)
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
