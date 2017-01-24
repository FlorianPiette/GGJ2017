using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
	public static MusicManager Instance;

    [FMODUnity.EventRef]
    public string heartFight = "event:/heartFight_music";
    [FMODUnity.EventRef]
    public string fight_music = "event:/fight_music";

    EventInstance heartFightMusic;
    EventInstance fight_musicInstance;

    bool currentlyInHeartMode = false;

    public bool debugModeActivated = false;

    
	void Awake()
	{
		Instance = this;
	}

    void Start ()
	{
        fight_musicInstance = FMODUnity.RuntimeManager.CreateInstance(fight_music);
        fight_musicInstance.start();

        heartFightMusic = FMODUnity.RuntimeManager.CreateInstance(heartFight);
        heartFightMusic.start();
        heartFightMusic.setVolume(0);
    }
	
    public void StopMusic ()
    {
        fight_musicInstance.setVolume(0);
    }

	// Update is called once per frame
	void Update ()
	{
        if (debugModeActivated)
        {
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
}
