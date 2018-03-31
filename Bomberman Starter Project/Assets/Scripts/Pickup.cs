using UnityEngine;

[CreateAssetMenu(fileName = "New Pick Up", menuName = "Power Up")]
public class Pickup : ScriptableObject {

    public bool addBomb;
    public bool minusBomb;
    public bool addFire;
    public bool minusFire;

    public Sprite icon;

    public bool enableKick;
}
