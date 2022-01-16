using UnityEngine;
using TMPro;

public class CastleHealthText : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI healthText;

    public void SetHealth(float newValue)
    {
        healthText.text = $"Health {(int)newValue}";
    }
}
