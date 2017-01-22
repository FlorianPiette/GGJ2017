using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartScript : MonoBehaviour
{
    public int playerID = 1;
	[SerializeField]
	private int life = 10;
	[SerializeField]
	private bool endGame = false;
	[SerializeField]
	private int damages = 1;

	public void HeartCollide()
	{
        if (PhaseManager.Instance.startingPhase == true)
        {
            PhaseManager.Instance.startingPhase = false;
            if (playerID == 1)
                PhaseManager.Instance.InitiateWinner("p0");
            else
                PhaseManager.Instance.InitiateWinner("p1");
        }

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
