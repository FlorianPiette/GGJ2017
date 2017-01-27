using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HeartScript : MonoBehaviour
{
	public int playerID = 1;
	private float life;
	[SerializeField]
	private float maxLife = 10;
    [SerializeField]
    private GameObject HPBar;
    [SerializeField]
	private int damages = 1;

    [FMODUnity.EventRef]
    public string hitCore_sfx = "event:/hitCore_sfx";
    [FMODUnity.EventRef]
    public string heartExplode_sfx = "event:/heartExplode_sfx";
    [FMODUnity.EventRef]
    public string win_music = "event:/win_music";

    //C'est sale :<
    public GameObject otherHeart;

	void Start()
	{
		life = maxLife;
		if(HPBar == null)
		{
			switch(playerID)
			{
				case 1:
					HPBar = UIManager.Instance.HeartLife_J1;
					break;
				case 2:
					HPBar = UIManager.Instance.HeartLife_J2;
					break;
			}
		}
		
	}

    public void HeartCollide()
	{
        if (life <= 0)
            return;

		FMODUnity.RuntimeManager.PlayOneShot(hitCore_sfx, Vector3.zero);

        if (PhaseManager.Instance.startingPhase == true)
        {
            PhaseManager.Instance.startingPhase = false;

            if (playerID == 1)
                PhaseManager.Instance.InitiateWinner("p1");
            else
                PhaseManager.Instance.InitiateWinner("p0");
			return;
        }

        if (life > damages)
		{
			life -= damages;
			HPBar.GetComponent<Image>().fillAmount = life / maxLife;
		}
		else
        {
			life -= damages;
			HPBar.GetComponent<Image>().fillAmount = life / maxLife;
            PhaseManager.Instance.BlockPlayerMovement();           

            FMODUnity.RuntimeManager.PlayOneShot(heartExplode_sfx, Vector3.zero);
            this.GetComponent<Animator>().SetTrigger("explose");

            //Couper la musique
			MusicManager.Instance.StopMusic();
            StartCoroutine(DelayWinMusic());

            if (playerID == 1)
			{
				UIManager.Instance.Victoire_J2.SetActive(true);
            }
            else
			{
				UIManager.Instance.Victoire_J1.SetActive(true);
            }
		}
	}
    
    IEnumerator DelayWinMusic()
    {
        yield return new WaitForSeconds(3f);

        otherHeart.GetComponent<Animator>().SetTrigger("victoire");
        FMODUnity.RuntimeManager.PlayOneShot(win_music, Vector3.zero);
        
        yield return new WaitForSeconds(10.99f);

        SceneManager.LoadScene("Menu");
    }
}
