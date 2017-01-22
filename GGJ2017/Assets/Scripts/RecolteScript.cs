using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecolteScript : MonoBehaviour
{
	float newManaForPlayer;
    public int playerId = 1;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Collectibles")
		{
			float playerMana = transform.parent.GetComponent<PlayerScript>().manaCount;
			newManaForPlayer = other.GetComponent<CollectiblesScript>().GiveMana(playerMana);

			transform.parent.GetComponent<PlayerScript>().manaCount = newManaForPlayer;
            if (newManaForPlayer > transform.parent.GetComponent<PlayerScript>().manaMax)
                PhaseManager.Instance.InitiatePhase();

			other.GetComponent<CollectiblesScript>().Collect(playerId);
		}
	}
}
