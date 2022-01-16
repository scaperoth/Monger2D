using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "GameSettings")]
public class GameSettings : ScriptableObject
{
    public float characterSpeed = 1f;
    public float knightDamage = 5f;
    public float startingCastleHealth = 100f;
}
