using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    [FMODUnity.EventRef]
    public string heartFight = "event:/heartFight_music";
    EventInstance heartFightMusic;
    bool currentlyInHeartMode = false;

    // Use this for initialization
    void Start () {

        Debug.LogError("CAUTION : Supprimer le debug avec I");
        heartFightMusic = FMODUnity.RuntimeManager.CreateInstance(heartFight);
        heartFightMusic.start();
        heartFightMusic.setVolume(0);
    }
	
	// Update is called once per frame
	void Update () {

        //Debug
		if (Input.GetKeyDown(KeyCode.I))
        {
            if (currentlyInHeartMode)
            {
                currentlyInHeartMode = false;
                heartFightMusic.setVolume(0f);
            }
            else if (!currentlyInHeartMode)
            {
                currentlyInHeartMode = true;
                heartFightMusic.setVolume(1f);
            }
        }
	}
}
