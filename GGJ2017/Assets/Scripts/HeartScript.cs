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

    public GameObject musicManager;

    [FMODUnity.EventRef]
    public string hitCore_sfx = "event:/hitCore_sfx";
    [FMODUnity.EventRef]
    public string heartExplode_sfx = "event:/heartExplode_sfx";
    [FMODUnity.EventRef]
    public string win_music = "event:/win_music";

    //C'est sale :<
    public GameObject otherHeart;

    public void HeartCollide()
	{
        if (life <= 0)
            return;

        if (PhaseManager.Instance.startingPhase == true)
        {
            PhaseManager.Instance.startingPhase = false;

            if (playerID == 1)
                PhaseManager.Instance.InitiateWinner("p1");
            else
                PhaseManager.Instance.InitiateWinner("p0");
        }

        FMODUnity.RuntimeManager.PlayOneShot(hitCore_sfx, Vector3.zero);

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
            PhaseManager.Instance.BlockPlayerMovement();           

            FMODUnity.RuntimeManager.PlayOneShot(heartExplode_sfx, Vector3.zero);
            this.GetComponent<Animator>().SetTrigger("explose");

            //Couper la musique 
            musicManager.GetComponent<MusicManager>().StopMusic();
            StartCoroutine(DelayWinMusic());

            //Je gère tout ça ici comme un sale parce que PLUS LE TEMPS et nuit blanche o/
            Debug.LogWarning("VICTOIRE !");
            ResultatJ1.enabled = true;
            ResultatJ2.enabled = true;

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
    
    IEnumerator DelayWinMusic()
    {
        yield return new WaitForSeconds(3f);

        otherHeart.GetComponent<Animator>().SetTrigger("victoire");
        FMODUnity.RuntimeManager.PlayOneShot(win_music, Vector3.zero);
        
        yield return new WaitForSeconds(11.077f);

        SceneManager.LoadScene("Menu");
    }
}
