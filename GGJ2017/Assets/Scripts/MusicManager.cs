using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    [FMODUnity.EventRef]
    public string attack = "event:/attack_sfx";
   // FMOD.Studio.EventInstance attackEnv;
    //FMOD.Studio.ParameterInstance attackEndParam;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.I))
        {
            FMODUnity.RuntimeManager.PlayOneShot(attack, Vector3.zero);
            //attackEnv = FMODUnity.RuntimeManager.CreateInstance(attack);
            //PlayOneShot("blabl");

        }
	}
}
