using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class PlayerScript : MonoBehaviour
{
	public bool attackLoad;
	public float TimerLoad;
	public int looseMana;

	private float _manaCount;
	public float manaCount;
	[SerializeField]
	private float speed;
	[SerializeField]
	private int playerId;
	public PhaseManager.Phase phase;
    //public float tilt;
	[SerializeField]
    private Boundary boundary;
	private new Rigidbody2D rigidbody;
	private Vector2 savedMovement;

	private string playerName;
	public bool startOn = true;
	public float maxDashTime = 1.0f;
	public float dashSpeed = 10.0f;
	public float dashStoppingSpeed = 0.1f;
	private float dashDelay;
	public float dashDelayMax = 10;
	public bool dashOn = false;
	public bool canDash = true;
	private float currentDashTime;
	private string throwInput;
	private bool throwOn = false;
	private bool chargeOn = false;
	private string dashInput;
    [SerializeField]
    private GameObject ball;

	[HideInInspector]
	public Animator animator;

    private bool movementIsBlocked = false; //During block animation

    [FMODUnity.EventRef]
    public string golemActivation = "event:/golemActivation_sfx";

    public float surcharge;

    void Awake()
	{
		rigidbody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		playerName = transform.name;

		currentDashTime = maxDashTime;
		throwInput = "J" + playerId + "Action";
		dashInput = "J" + playerId + "Dash";
		dashDelay = dashDelayMax;

    }

    void FixedUpdate ()
    {
		_manaCount = manaCount;

		float moveHorizontal = Input.GetAxis("J" + playerId + "Horizontal");
		float moveVertical = Input.GetAxis("J" + playerId + "Vertical");
 //       print(moveHorizontal + "," + moveVertical);

		Vector2 movement = new Vector2(moveHorizontal, moveVertical);

		if (!dashOn && !chargeOn && !throwOn && movement != Vector2.zero && !movementIsBlocked)
		{
			rigidbody.velocity = movement * speed;
			animator.Play(playerName + "_Run");
		}
		else
			rigidbody.velocity = Vector2.zero;

        rigidbody.position = new Vector2 
        (
            Mathf.Clamp (rigidbody.position.x, boundary.xMin, boundary.xMax), 
            Mathf.Clamp (rigidbody.position.y, boundary.yMin, boundary.yMax)
        );

		//dash
		if (Input.GetButtonDown(dashInput) && dashOn == false && canDash == true)
		{
			currentDashTime = 0.0f;
			dashOn = true;
			animator.Play(playerName + "_Dash");
			canDash = false;
			savedMovement = movement;
		}

		if (currentDashTime < maxDashTime)
		{
			currentDashTime += dashStoppingSpeed;
			rigidbody.velocity = savedMovement * dashSpeed;
		}
		else
		{
			dashOn = false;
			//movement = Vector2.zero;
		}

		if (!canDash)
		{
			dashDelay -= 0.1f;
		}

		if (dashDelay <= 0)
		{
			dashDelay = dashDelayMax;
			canDash = true;
		}
		if (Input.GetButtonUp(throwInput) && chargeOn && !throwOn)
		{
			chargeOn = false;
			throwOn = true;
			animator.Play(playerName + "_Throw");
			int multiplier = 1;
			if (TimerLoad > 2.5f)
				multiplier = 2;
            if (TimerLoad < surcharge)
            {
                GameObject balle;

                //print(gameObject.transform.GetChild(0).transform.position);
                balle = Instantiate(ball);
                balle.transform.position = gameObject.transform.GetChild(0).transform.position;
                if (movement.x < 0)
                    balle.GetComponent<BallScript>().setDirection(-movement);
                else if (movement.x == 0 && movement.y == 0)
                    balle.GetComponent<BallScript>().setDirection(new Vector2(1, 0));
                else
                    balle.GetComponent<BallScript>().setDirection(movement);
                balle.GetComponent<BallScript>().setVitesse(looseMana * multiplier);
                Physics2D.IgnoreCollision(balle.GetComponent<Collider2D>(), GetComponent<Collider2D>());

                throwOn = false;
            }

            TimerLoad = 0;
            attackLoad = false;
            manaCount -= looseMana;
        }

        if (Input.GetButtonDown(throwInput) && phase != PhaseManager.Phase.Defense && manaCount >= 2)
		{
			attackLoad = true;
			looseMana = 2;
			chargeOn = true;
			animator.Play(playerName + "_RunBall");
            //TODO Stop rechargement
		}

		if (attackLoad == true) {
			TimerLoad += Time.deltaTime;
		}

		if (TimerLoad >= 0.5f && manaCount >= 4) {
			looseMana = 4;
        }
		if (TimerLoad >= 1f && manaCount >= 6) {
			looseMana = 6;
        }
		if (TimerLoad >= 1.5f && manaCount >= 8) {
			looseMana = 8;
        }
		if (TimerLoad >= 2f && manaCount >= 10) {
			looseMana = 10;
  //TODO SURCHARGE
        }
    }

    void IntroSoundPlayer ()
    {
        if (playerId == 1)
            FMODUnity.RuntimeManager.PlayOneShot(golemActivation, Vector3.zero);
    }
    
    public void BlockBullet()
    {
        animator.Play("Gredd_Guard");
        ShakeScript.Instance.Shake(ShakeScript.ScreenshakeTypes.Medium);

        movementIsBlocked = true;
        //Bloqué jusqu'à la fin de la garde. Au cas où ça buge, on réactive les fonctionnalités via une Coroutine (qui ne sera généralement pas activée).
        StopCoroutine("ReactivateMovement");
        StartCoroutine(PendingForReactivateMovement(1.5f));
    }

    IEnumerator PendingForReactivateMovement(float duration)
    {
        yield return new WaitForSeconds(duration);
        ReactivateMovement();
    }

    public void ReactivateMovement()
    {
        movementIsBlocked = false;
    }
}