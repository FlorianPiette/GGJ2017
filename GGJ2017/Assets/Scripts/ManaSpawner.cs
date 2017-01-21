using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaSpawner : MonoBehaviour
{
	private int objectSpawnedLimit = 3;
	private int objectSpawned;
	public GameObject manaObject;

	void OnEnable ()
	{
		objectSpawned = transform.childCount;
		StartCoroutine("PopMana");
	}

	public void ReduceObjectSpawned()
	{
		objectSpawned -= 1;
		StopCoroutine("PopMana");
		StartCoroutine("PopMana");
	}

	IEnumerator PopMana()
	{
		yield return new WaitForSeconds(2);

		if(objectSpawned <= objectSpawnedLimit)
		{
			//Vector3 newPosition = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0);
			Vector3 randomPos = Random.insideUnitSphere * 2;
			randomPos.z = 0;
			GameObject newObj = Instantiate(manaObject, transform.position + randomPos, Quaternion.identity);
			newObj.transform.SetParent(this.transform);

			objectSpawned += 1;
		}
		else
		{
			StopCoroutine("PopMana");
		}

		yield return StartCoroutine("PopMana");
	}

	void OnDisable()
	{
		StopCoroutine("PopMana");
	}
}
