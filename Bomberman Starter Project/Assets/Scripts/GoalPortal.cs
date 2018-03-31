using UnityEngine;

public class GoalPortal : MonoBehaviour {

    MenuScript menuScript;

    bool stopRepeating = false;

    void Start ()
    {
        menuScript = MenuScript.instance;
    }

	void OnTriggerEnter2D (Collider2D other)
    {
        if (stopRepeating)
        {
            return;
        }
        if (other.tag == "Player")
        {
            menuScript.GToggle();
            stopRepeating = true;
        }
    }

}
