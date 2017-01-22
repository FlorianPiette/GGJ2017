using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartScript : MonoBehaviour
{
    public int playerID = 1;
	[SerializeField]
	private int life = 7;
    [SerializeField]
    private GameObject HPBar;
    //[SerializeField]
    private bool endGame = false;
    [SerializeField]
	private int damages = 1;

	public void HeartCollide()
	{
        if (PhaseManager.Instance.startingPhase == true)
        {
            PhaseManager.Instance.startingPhase = false;

            if (playerID == 1)
                PhaseManager.Instance.InitiateWinner("p1");
            else
                PhaseManager.Instance.InitiateWinner("p0");
        }

        if (life > damages)
		{
			life -= damages;
            HPBar.transform.localScale = new Vector3 ((life / 7f), 1f, 1f);
		}
		else
        {
            life -= damages;
            HPBar.transform.localScale = new Vector3((life / 7f), 1f, 1f);
            endGame = true;
            Debug.LogError("VICTOIRE");
		}
	}
}
