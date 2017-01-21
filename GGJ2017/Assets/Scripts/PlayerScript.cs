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

	public float maxDashTime = 1.0f;
	public float dashSpeed = 10.0f;
	public float dashStoppingSpeed = 0.1f;
	private float dashDelay;
	public float dashDelayMax = 10;
	public bool dashOn = false;
	private float currentDashTime;
	private string dashInput;
    [SerializeField]
    private GameObject ball;

	void Awake()
	{
		rigidbody = GetComponent<Rigidbody2D>();
		currentDashTime = maxDashTime;
		dashInput = "J" + playerId + "Dash";
		dashDelay = dashDelayMax;
    }

    void FixedUpdate ()
    {
		_manaCount = manaCount;

        float moveHorizontal = Input.GetAxis ("J" + playerId + "Horizontal");
		float moveVertical = Input.GetAxis("J" + playerId + "Vertical");
 //       print(moveHorizontal + "," + moveVertical);

        Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
        rigidbody.velocity = movement * speed;

        rigidbody.position = new Vector2 
        (
            Mathf.Clamp (rigidbody.position.x, boundary.xMin, boundary.xMax), 
            Mathf.Clamp (rigidbody.position.y, boundary.yMin, boundary.yMax)
        );

		//dash
		if (Input.GetButtonDown(dashInput) && dashOn == false)
		{
			currentDashTime = 0.0f;
			dashOn = true;
		}

		if (currentDashTime < maxDashTime)
		{
			currentDashTime += dashStoppingSpeed;
			rigidbody.velocity = movement * dashSpeed;
		}
		else
		{
			movement = Vector2.zero;
		}

		if (dashOn)
		{
			dashDelay -= 0.1f;
		}

		if (dashDelay <= 0)
		{
			dashDelay = dashDelayMax;
			dashOn = false;
		}
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameObject balle;

//print(gameObject.transform.GetChild(0).transform.position);
            balle = Instantiate(ball);
            balle.transform.position = gameObject.transform.GetChild(0).transform.position;
            if (movement.x < 0)
                balle.GetComponent<BallScript>().setDirection(-movement);
            else
                balle.GetComponent<BallScript>().setDirection(movement);
            balle.GetComponent<BallScript>().setVitesse(2); //TODO Gestion de vitesse
            Physics2D.IgnoreCollision(balle.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.name == "shot")
		{
			other.gameObject.GetComponent<ShieldScript>().ShieldCollide();
		}
	}
}