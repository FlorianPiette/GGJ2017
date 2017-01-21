using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesScript : MonoBehaviour
{
	public float manaPoints = 10.0f;

	public float GiveMana(float playerMana)
	{
		float amountOfMana = playerMana + manaPoints;
		return amountOfMana;
	}

	public void Kill()
	{
		if(transform.parent.GetComponent<ManaSpawner>().enabled)
		{
			transform.parent.GetComponent<ManaSpawner>().ReduceObjectSpawned();
		}
		Destroy(this.gameObject);
	}
}
