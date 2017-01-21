using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
	[SerializeField]
	private int life=1;
    public bool is_first_hit = true;
    public string winnerIntro;

	public void ShieldCollide()
	{
        if (is_first_hit)
        {
            if (gameObject.transform.position.x<=0)
            {
                PhaseManager.Instance.InitiateWinner("p0");
            }
            else
            {
                PhaseManager.Instance.InitiateWinner("p1");
            }
            is_first_hit = false;
        }else { 
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
}
