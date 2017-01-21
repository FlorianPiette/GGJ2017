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
    //public float tilt;
	[SerializeField]
    private Boundary boundary;
	private Rigidbody2D rigidbody;

	void Awake()
	{
		rigidbody = GetComponent<Rigidbody2D>();
	}

    void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");

        Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
        rigidbody.velocity = movement * speed;

        rigidbody.position = new Vector2 
        (
            Mathf.Clamp (rigidbody.position.x, boundary.xMin, boundary.xMax), 
            Mathf.Clamp (rigidbody.position.y, boundary.yMin, boundary.yMax)
        );

        //rigidbody.rotation = Quaternion.Euler (0.0f, 0.0f, rigidbody.velocity.x * -tilt);
    }
}