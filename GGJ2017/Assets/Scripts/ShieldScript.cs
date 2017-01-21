using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
	[SerializeField]
	private int life;

	public void ShieldCollide()
	{
		if (life >= 1)
		{
			life--;
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
