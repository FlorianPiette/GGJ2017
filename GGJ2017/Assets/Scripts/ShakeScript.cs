using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShakeScript : Singleton<ShakeScript> {
    //Le script est un Singleton : il n'en existe qu'un seul, qui est accessible de n'importe où. 
    //Pour appeler un script, utiliser ShakeScript.Instance.Shake(ShakeScript.ScreenshakeTypes.Weak); 
    protected ShakeScript() {
    }

    // Il est recommandé d'utiliser des valeurs entre 1 et 4 pour la puissance, et 0.2 et 0.7 pour la duree
    // Note : De nombreuses parties sont désactivés car le script contient 3 scripts différents (dont un seul activé)
    [SerializeField]
    private float baseStrength = 15f;
    [SerializeField]
    private float baseDuration = 0.6f;
    [SerializeField]
    private float baseDecreaseFactor = 0.95f;

    public bool DebugActivated = false;

    private float Strength;
    private float Duration;
    private float DecreaseFactor;

    private Vector3 SavePosCamera;
    private bool _isShaking = false;
    
    public enum ScreenshakeTypes {
        Weak,
        Medium,
        Strong,
        VeryStrong
    }

    
    void Update ()
    {
        if (DebugActivated)
        {
            //To test the screenshake
            if (Input.GetKeyDown(KeyCode.A))
                Shake(7f, .5f, 0.95f);
            if (Input.GetKeyDown(KeyCode.Z))
                Shake(15f, .7f, 0.95f);
            if (Input.GetKeyDown(KeyCode.E))
                Shake(25f, .5f, 0.95f);
            if (Input.GetKeyDown(KeyCode.R))
                Shake(25f, 1f, 0.95f);
            if (Input.GetKeyDown(KeyCode.T))
                Shake(25f, .7f, 0.95f);
            if (Input.GetKeyDown(KeyCode.Y))
                Shake(25f, .5f, 0.95f);
            if (Input.GetKeyDown(KeyCode.U))
                Shake(15f, 1f, 0.8f);
            if (Input.GetKeyDown(KeyCode.I))
                Shake(15f, 1f, 0.9f);
        }
    }

    /// <summary>
    ///Create a shake of the camera. You can let all parameters empty for basic effect.
    ///</summary>
    /// <param name ="str">Strength of the shake. Should be between [1-30]</param>
    /// <param name ="dur">Duration of the shake. Should be between [0.1-3f]</param>
    /// <param name ="_decreaseFactor">Speed at which the shake fades away. Should be between [0.7-0.99]</param>
    public void Shake(float str = -1f, float dur = -1f, float _decreaseFactor = -1f) {
        //Les parametres sont Nullable. On fixe donc les valeurs de base uniquement si une valeur a été donnée.
        if (str != -1)
            Strength = str;
        else
            Strength = baseStrength;

        if (dur != -1)
            Duration = dur;
        else
            Duration = baseDuration;

        if (_decreaseFactor != -1)
            DecreaseFactor = _decreaseFactor;
        else
            DecreaseFactor = baseDecreaseFactor;

        if (_isShaking)
        {
            StopAllCoroutines();
            _isShaking = false;
            Camera.main.transform.position = SavePosCamera;
        }

        StartCoroutine("ShakeIt");
    }

    public void Shake(ScreenshakeTypes type) {
        //Reglable
        if (type == ScreenshakeTypes.Weak)
        {
            Strength = 7f;
            Duration = .5f;
            DecreaseFactor = 0.95f;
        } else if (type == ScreenshakeTypes.Medium)
        {
            Strength = 15f;
            Duration = .7f;
            DecreaseFactor = 0.95f;
        } else if (type == ScreenshakeTypes.Strong)
        {
            Strength = 25f;
            Duration = .7f;
            DecreaseFactor = 0.95f;
        } else if (type == ScreenshakeTypes.VeryStrong)
        {
            Strength = 50f;
            Duration = 1.2f;
            DecreaseFactor = 0.95f;
        }

        if (_isShaking)
        {
            StopAllCoroutines();
            _isShaking = false;
            Camera.main.transform.position = SavePosCamera;
        }

        StartCoroutine("ShakeIt");
    }

    private IEnumerator ShakeIt () {
        _isShaking = true;

        Camera gameCamera = Camera.main;

       // gameCamera.gameObject.GetComponentInChildren<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        SavePosCamera = gameCamera.transform.position;

        while (Duration > 0) {
            Duration -= Time.deltaTime;

            gameCamera.transform.position = SavePosCamera + UnityEngine.Random.insideUnitSphere * (Strength/40f);
            Duration -= Time.deltaTime;

            Strength *= DecreaseFactor;
            yield return null;
        }

        gameCamera.transform.position = SavePosCamera;
        //gameCamera.gameObject.GetComponentInChildren<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;

        _isShaking = false;
    }
}
