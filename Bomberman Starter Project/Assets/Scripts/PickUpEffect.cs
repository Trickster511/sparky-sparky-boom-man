using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpEffect : MonoBehaviour {

    public SpriteRenderer thisObject;
    public Pickup pickup;

    bool stopRepeating = false;

    // private PlayerController player;

	// Use this for initialization
	void Start ()
    {
        // Again, find a better way to do this vv
        // player = FindObjectOfType<PlayerController>();
        thisObject.sprite = pickup.icon;
        // Temporary option vv
        Destroy(gameObject, 15f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(stopRepeating)
        {
            return;
        }
        if (other.tag == "Player")
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (pickup.addBomb)
            {
                if(player.bombMax < 6)
                player.bombMax++;
            }

            if (pickup.addFire)
            {
                if(player.firePower < 4)
                player.firePower++;
            }
            if (pickup.minusBomb)
            {
                if (player.bombMax > 1)
                    player.bombMax--;
            }

            if (pickup.minusFire)
            {
                if (player.firePower > 1)
                    player.firePower--;
            }
            if(pickup.enableKick)
            {
                player.isKick = true;
            }
            stopRepeating = true;
            Destroy(gameObject);
        }
    }
}
