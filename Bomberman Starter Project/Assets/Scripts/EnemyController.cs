using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float speed = 15f;

    public bool isHorPatrol = true;
    [HideInInspector]
    public bool facingRight = true;

    [Header("If isHorPatrol is false, vv")]

    public SpriteRenderer thisObject;
    public Sprite up;
    public Sprite down;
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if(isHorPatrol)
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        if(!isHorPatrol)
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(isHorPatrol)
        {
            Flip();
        }
        else
        {
            FlipUp();
        }
        speed = speed * -1f;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Bomb")
        {
            if (isHorPatrol)
            {
                Flip();
            }
            else
            {
                FlipUp();
            }
            speed = speed * -1f;
        }
    }

    public void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void FlipUp()
    {
        if(speed > 0)
        {
            thisObject.sprite = down;
        }
        else if(speed < 0)
        {
            thisObject.sprite = up;
        }
    }
}
