using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesScript : MonoBehaviour
{
	public float manaPoints = 10.0f;

	void Start ()
	{
		
	}
	
	void Update ()
	{
		
	}

	public float GiveMana(float playerMana)
	{
		float amountOfMana = playerMana + manaPoints;
		return amountOfMana;
	}

	public void Kill()
	{
		GameObject.Destroy(this.gameObject);
	}
}
