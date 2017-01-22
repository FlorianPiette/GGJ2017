using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoOffGameObject : MonoBehaviour
{
	void OnEnable()
	{
		StartCoroutine("GoOffCoroutine");
	}
	void Update()
	{

	}

	IEnumerator GoOffCoroutine()
	{
		yield return new WaitForSeconds(0.5f);
		gameObject.SetActive(!gameObject.activeSelf);
	}
}
