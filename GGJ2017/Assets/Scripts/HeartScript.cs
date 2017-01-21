using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartScript : MonoBehaviour
{

	[SerializeField]
	private int life;
	[SerializeField]
	private bool endGame = false;
	[SerializeField]
	private int damages;

	public void HeartCollide()
	{
		if (life >= 1)
		{
			life -= damages;
		}
		else
		{
			endGame = true;
		}
	}
}
