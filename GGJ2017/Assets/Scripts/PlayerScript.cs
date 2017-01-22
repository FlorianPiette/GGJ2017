﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class PlayerScript : MonoBehaviour
{
    public bool isEndGame=true;
	public bool attackLoad;
	public float TimerLoad;
	public int looseMana;

	private float _manaCount;
	public float manaMax;
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

    private bool movementIsLimited = false; //During block animation and throw animation

    [FMODUnity.EventRef]
    public string golemActivation = "event:/golemActivation_sfx";

    public float surcharge;

	public float intervalleRecharge;
	private float timeBeforeRecharging;

    private GameObject jauge;
    private Vector2 sizeOrigin;
    private Vector2 posOrigin;

    void Awake()
	{
		rigidbody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		playerName = transform.name;

		currentDashTime = maxDashTime;
		throwInput = "J" + playerId + "Action";
		dashInput = "J" + playerId + "Dash";
		dashDelay = dashDelayMax;
		manaCount = manaMax;
		timeBeforeRecharging = intervalleRecharge;

        jauge = GameObject.Find("jauge" + playerId);
        sizeOrigin = jauge.GetComponent<RectTransform>().sizeDelta;
        posOrigin = jauge.transform.position;
    }

    void FixedUpdate ()
    {
		_manaCount = manaCount;
        float moveHorizontal = Input.GetAxis("J" + playerId + "Horizontal");
		float moveVertical = Input.GetAxis("J" + playerId + "Vertical");
 //       print(moveHorizontal + "," + moveVertical);

		Vector2 movement = new Vector2(moveHorizontal, moveVertical);

		if (!dashOn && !chargeOn && !throwOn && movement != Vector2.zero && !isEndGame)
		{
            if (movementIsLimited)
            {
                rigidbody.velocity = movement * speed / 8f;
            } else 
            {
                rigidbody.velocity = movement * speed;
                animator.Play(playerName + "_Run");
            }


            //Retourner le personnage
            if ((movement.x >= 0 && transform.position.x < 0) || (movement.x > 0 && transform.position.x > 0))
                this.GetComponent<SpriteRenderer>().flipX = true;
            else
                this.GetComponent<SpriteRenderer>().flipX = false;
        }
		else
			rigidbody.velocity = Vector2.zero;

        rigidbody.position = new Vector2 
        (
            Mathf.Clamp (rigidbody.position.x, boundary.xMin, boundary.xMax), 
            Mathf.Clamp (rigidbody.position.y, boundary.yMin, boundary.yMax)
        );

		//dash
		if (Input.GetButtonDown(dashInput) && dashOn == false && canDash == true && !isEndGame)
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
		else if (dashOn)
		{
            //Recup time après un dash
            movementIsLimited = true;
            //Bloqué jusqu'à la fin de la garde. Au cas où ça buge, on réactive les fonctionnalités via une Coroutine (qui ne sera généralement pas activée).
            StopCoroutine("PendingForReactivateMovement");
            StartCoroutine(PendingForReactivateMovement(.5f));

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

		if (Input.GetButtonUp(throwInput) && chargeOn && !throwOn && !isEndGame)
		{
			chargeOn = false;
			throwOn = true;
			animator.Play(playerName + "_Throw");

            if (playerId == 2)
                this.GetComponent<SpriteRenderer>().flipX = false;
            else
                this.GetComponent<SpriteRenderer>().flipX = true;

            movementIsLimited = true;
            //Bloqué jusqu'à la fin de la garde. Au cas où ça buge, on réactive les fonctionnalités via une Coroutine (qui ne sera généralement pas activée).
            StopCoroutine("PendingForReactivateMovement");
            StartCoroutine(PendingForReactivateMovement(1f));

            int multiplier = 1;
			if (TimerLoad > 2.5f)
				multiplier = 2;


            if (TimerLoad < surcharge)
            {
                LaunchBullet(movement, multiplier);
            } 

            throwOn = false;
            chargeOn = false;
            TimerLoad = 0;
            attackLoad = false;
        }

        if (Input.GetButtonDown(throwInput) && phase != PhaseManager.Phase.Defense && manaCount >= 2 && !isEndGame)
		{
			attackLoad = true;
			looseMana = 2;
			chargeOn = true;
			animator.Play(playerName + "_RunBall");

            if (playerId == 2)
                this.GetComponent<SpriteRenderer>().flipX = false;
            else
                this.GetComponent<SpriteRenderer>().flipX = true;
           
        }


        if (attackLoad == true) {
			TimerLoad += Time.deltaTime;
		}

		if (TimerLoad >= 0.5f && manaCount >= 4) {
            looseMana = 4;
        }
        else if (TimerLoad >= 0.5f && manaCount < 4)
            LaunchBullet(movement, 1);
        if (TimerLoad >= 1f && manaCount >= 6) {
            looseMana = 6;
        }
        else if (TimerLoad >= 1f && manaCount < 6)
            LaunchBullet(movement, 1);
        if (TimerLoad >= 1.5f && manaCount >= 8) {
            looseMana = 8;
        }
        else if (TimerLoad >= 1.5f && manaCount < 8)
            LaunchBullet(movement, 1);
        if (TimerLoad >= 2f && manaCount >= 10) {
            looseMana = 10;
  //TODO SURCHARGE
        }
        else if (TimerLoad >= 2f && manaCount < 10)
            LaunchBullet(movement, 1);

        if (!attackLoad && manaCount < manaMax)
        {
            intervalleRecharge -= Time.deltaTime;
            if (intervalleRecharge <= 0 && phase == PhaseManager.Phase.Offense)
            {
                manaCount++;
                intervalleRecharge = timeBeforeRecharging;
            }
        }

        jauge.GetComponent<RectTransform>().sizeDelta = new Vector2(manaCount * sizeOrigin.x / manaMax, sizeOrigin.y);
        //jauge.transform.position = new Vector3((transform.position.x < 0 ? posOrigin.x : posOrigin.x + sizeOrigin.x - jauge.GetComponent<RectTransform>().sizeDelta.x), posOrigin.y);
    }

public void LaunchBullet(Vector2 movement, int multiplier)
	{
		GameObject balle;

		//print(gameObject.transform.GetChild(0).transform.position);
		balle = Instantiate(ball);
		balle.transform.position = gameObject.transform.GetChild(0).transform.position;
        if ((movement.x < 0 && transform.position.x < 0) || (movement.x > 0 && transform.position.x > 0))
            balle.GetComponent<BallScript>().setDirection(-movement);
		else if (movement.x == 0 && movement.y == 0)
            balle.GetComponent<BallScript>().setDirection((transform.position.x < 0 ? Vector2.right : Vector2.left));
        else
            balle.GetComponent<BallScript>().setDirection(movement);
		balle.GetComponent<BallScript>().setVitesse(looseMana * multiplier * 2);
		Physics2D.IgnoreCollision(balle.GetComponent<Collider2D>(), GetComponent<Collider2D>());

		manaCount -= looseMana;
	}


    void IntroSoundPlayer ()
    {
        if (playerId == 1)
            FMODUnity.RuntimeManager.PlayOneShot(golemActivation, Vector3.zero);
    }
    
    public void BlockBullet()
    {
		animator.Play(playerName + "_Guard");

        movementIsLimited = true;
        //Bloqué jusqu'à la fin de la garde. Au cas où ça buge, on réactive les fonctionnalités via une Coroutine (qui ne sera généralement pas activée).
        StopCoroutine("PendingForReactivateMovement");
        StartCoroutine(PendingForReactivateMovement(1.5f));
    }

    IEnumerator PendingForReactivateMovement(float duration)
    {
        yield return new WaitForSeconds(duration);
        ReactivateMovement();
    }

    public void ReactivateMovement()
    {
        movementIsLimited = false;
    }
    public void BlockMovementEnd()
    {
        isEndGame = true;
    }
}