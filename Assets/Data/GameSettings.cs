using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "GameSettings")]
public class GameSettings : ScriptableObject
{
    public float characterSpeed = 1f;
    public float knightDamage = 5f;
    public float merchantBonus = 2f;
    public float deterioratingHealthValue = 1;
    public float deterioratingHealthDelay = 1f;
    public float startingCastleHealth = 100f;
}
