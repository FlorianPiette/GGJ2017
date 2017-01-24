using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesScript : MonoBehaviour
{
    public Vector3 player1ManaPos;
    public Vector3 player2ManaPos;
    public float speed = 1;

	public void Collect(int playerId)
	{
		if (transform.parent.GetComponent<ManaSpawner>().enabled)
		{
			transform.parent.GetComponent<ManaSpawner>().ReduceObjectSpawned();
		}
        
        this.GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine(GoToPlayerManaPool(playerId));
	}

    IEnumerator GoToPlayerManaPool(int _playerId)
    {
        Vector3 destination;
        if (_playerId == 1)
            destination = player1ManaPos;
        else
            destination = player2ManaPos;

        while (this.transform.position != destination)
        {
            speed *= 1.05f;
            this.transform.position = Vector3.MoveTowards(this.transform.position, destination, speed);
            yield return null;
        }

        Destroy(this.gameObject);
    }
}
