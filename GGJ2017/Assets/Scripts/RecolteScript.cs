using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecolteScript : MonoBehaviour
{
    public int playerId = 1;

    [FMODUnity.EventRef]
    public string collectMana_sfxrnd = "event:/collectMana_sfxrnd";

    void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Collectibles")
		{
            FMODUnity.RuntimeManager.PlayOneShot(collectMana_sfxrnd, Vector3.zero);

            transform.parent.GetComponent<PlayerScript>().manaCount += 10f;
            if (transform.parent.GetComponent<PlayerScript>().manaCount >= transform.parent.GetComponent<PlayerScript>().manaMax)
                PhaseManager.Instance.AttributePhase();

			other.GetComponent<CollectiblesScript>().Collect(playerId);
		}
	}
}
