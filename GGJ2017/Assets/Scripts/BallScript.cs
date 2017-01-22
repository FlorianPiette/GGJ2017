using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private Vector2 direction;
    private float vitesse;
	private Collider2D collider;

	void Start()
	{
		collider = GetComponent<Collider2D>();
		StartCoroutine("EnabledCollider");
	}

	IEnumerator EnabledCollider()
	{
		float timer = 1;
		while(timer < 0)
		{
			timer -= Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		collider.enabled = true;
		yield return null;
	}

	
	// Update is called once per frame
	void Update () {
        transform.Translate(direction * vitesse * Time.deltaTime);
    }

    public void setDirection(Vector2 direction)
    {
        this.direction = direction;
    }

    public void setVitesse(float vitesse)
    {
        this.vitesse = vitesse;
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		//collide wall change l'angle de la balle
		if (collision.gameObject.name == "up_collider" || collision.gameObject.name == "down_collider")
		{
			direction.y *= -1;
		}
		else
		{
			//collide avec le reste et la destroy
			if (collision.gameObject.tag == "Shield")
            {
                ShakeScript.Instance.Shake(ShakeScript.ScreenshakeTypes.Medium);
                collision.gameObject.GetComponent<ShieldScript>().ShieldCollide();
			}
			if (collision.gameObject.tag == "Heart")
			{
                ShakeScript.Instance.Shake(ShakeScript.ScreenshakeTypes.Strong);
                collision.gameObject.GetComponent<HeartScript>().HeartCollide();
			}
			if(collision.gameObject.tag == "Player")
            {
                ShakeScript.Instance.Shake(ShakeScript.ScreenshakeTypes.Weak);
                collision.gameObject.GetComponent<PlayerScript>().BlockBullet();
			}
			if(collision.gameObject.tag == "Ball")
            {
                return;
            }
			Destroy(gameObject);
		}
	}
}

