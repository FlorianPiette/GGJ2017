using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    public bool is_first_hit = true;
    public string winnerIntro;
    
    [FMODUnity.EventRef]
    public string hitShield_sfx_01 = "event:/hitShield_sfx_01";
    [FMODUnity.EventRef]
    public string hitShield_sfx_02 = "event:/hitShield_sfx_02";
    [FMODUnity.EventRef]
    public string hitShield_sfx_03 = "event:/hitShield_sfx_03";
    [FMODUnity.EventRef]
    public string repopShield_sfx = "event:/repopShield_sfx";

    public int shieldNumber = 1;
    private string soundToLaunchOnDestroy;

    void Awake ()
    {
        if (shieldNumber == 1 )
            soundToLaunchOnDestroy = hitShield_sfx_01;
        else if (shieldNumber == 2)
            soundToLaunchOnDestroy = hitShield_sfx_02;
        else 
            soundToLaunchOnDestroy = hitShield_sfx_03;
    }

    public void ActivateShield ()
    {
        FMODUnity.RuntimeManager.PlayOneShot(repopShield_sfx, Vector3.zero);
        this.GetComponent<SpriteRenderer>().enabled = true;
        this.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void DeactivateShield ()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void ShieldCollide()
    {
        FMODUnity.RuntimeManager.PlayOneShot(soundToLaunchOnDestroy, Vector3.zero);

        DeactivateShield();
    }
}
