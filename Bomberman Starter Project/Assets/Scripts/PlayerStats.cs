using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public int bombMax = 2;
    public int bombCount { get; private set; }
    public int firePower = 1;
    public bool isKick = false;

    void Awake()
    {
        bombCount = 0;
    }

    public void HigherBombCount ()
    {
        bombCount++;
    }

    public void LowerBombCount ()
    {
        bombCount--;
    }
}
