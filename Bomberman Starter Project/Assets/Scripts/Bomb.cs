using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public float countdown = 2f;
    private float fp;

    [HideInInspector]
    public PlayerController player;
    [HideInInspector]
    public Vector3 direct;
    [HideInInspector]
    public float speed;
    [HideInInspector]
    public bool isKicked;

    MapDestroyer mapDestroyer;

    void Start()
    {
        mapDestroyer = MapDestroyer.instance;
        // Find a better solution then one below vv
        // player = FindObjectOfType<PlayerController>();
        fp = player.firePower;
    }
	
	// Update is called once per frame
	void Update ()
    {
        countdown -= Time.deltaTime;

        if(countdown <= 0f)
        {
            Boom();
        }
	}

    void FixedUpdate()
    {
        if(isKicked)
        {
            Slide(direct, speed);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        isKicked = false;
        Debug.Log("isKicked is Disabled");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Explosion")
        {
            Debug.Log("You blew me up!!");
            countdown = 0f;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            this.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    public void Slide(Vector3 dir, float sp)
    {
        Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
        rb.WakeUp();
        if(dir.x > 0.2 || dir.x < -0.2)
        {
            dir.y = 0;
        }
        if(dir.y > 0.2 || dir.y < -0.2)
        {
            dir.x = 0;
        }
        Debug.Log("Speed of Bomb " + direct);
        transform.Translate(-dir.normalized * sp * Time.deltaTime);
        rb.isKinematic = false;
    }

    public void Boom ()
    {
        mapDestroyer.Explode(transform.position, fp);
        if (player != null)
        {
            player.LowerBombCount();
        }
        Destroy(gameObject);
    }
}
