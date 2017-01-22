using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public Text ResultatJ1;
    public Text ResultatJ2;

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

            //Je gère tout ça ici comme un sale parce que PLUS LE TEMPS et nuit blanche o/
            Debug.LogWarning("VICTOIRE !");
            ResultatJ1.enabled = true;
            ResultatJ2.enabled = true;
            StartCoroutine(AutoRestart());

            if (playerID == 1)
            {
                ResultatJ1.text = "DÉFAITE :<";
                ResultatJ2.text = "VICTOIRE !";
            }
            else
            {
                ResultatJ2.text = "DÉFAITE :<";
                ResultatJ1.text = "VICTOIRE !";
            }
		}
	}

    IEnumerator AutoRestart()
    {
        yield return new WaitForSeconds (10f);

        SceneManager.LoadScene("Menu");
    }
}
