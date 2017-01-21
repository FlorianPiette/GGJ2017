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
	[SerializeField]
	private float speed;
	[SerializeField]
	private int playerId;
    //public float tilt;
	[SerializeField]
    private Boundary boundary;
	private new Rigidbody rigidbody;

	void Awake()
	{
		rigidbody = GetComponent<Rigidbody>();
	}

    void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis ("J"+playerId+"Horizontal");
		float moveVertical = Input.GetAxis("J" + playerId + "Vertical");

        Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
        rigidbody.velocity = movement * speed;

        rigidbody.position = new Vector2 
        (
            Mathf.Clamp (rigidbody.position.x, boundary.xMin, boundary.xMax), 
            Mathf.Clamp (rigidbody.position.y, boundary.yMin, boundary.yMax)
        );
    }
}