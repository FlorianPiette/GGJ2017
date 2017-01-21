using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private Vector2 direction;
    private int vitesse;

	// Use this for initialization
	void Start () {
        direction = new Vector2(2, 5);
        vitesse = 1;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(direction * vitesse * Time.deltaTime);
    }

    public void setDirection(Vector2 direction)
    {
        this.direction = direction;
    }

    public void setVitesse(int vitesse)
    {
        this.vitesse = vitesse;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "up_collider" || collision.gameObject.name == "down_collider")
            direction.y *= -1;
        else
            direction.x *= -1;
    }
}

