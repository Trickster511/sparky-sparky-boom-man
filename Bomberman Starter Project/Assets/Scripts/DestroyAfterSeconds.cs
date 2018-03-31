using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        Destroy(gameObject, 0.6f);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }

        //if (other.tag == "Bomb")
        //{
        //    Debug.Log("I blew you up!!");
        //    other.GetComponent<Bomb>().Boom();
        //}
    }
}
