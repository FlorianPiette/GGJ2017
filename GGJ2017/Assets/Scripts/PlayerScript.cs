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
    }

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.name == "shot")
		{
			other.gameObject.GetComponent<ShieldScript>().ShieldCollide();
		}
	}
}