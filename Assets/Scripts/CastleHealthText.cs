using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CastleHealthText : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI healthText;

    [SerializeField]
    Slider healthSlider;

    public void SetHealth(float newValue)
    {
        if (healthText != null)
        {
            healthText.text = $"Population {(int)newValue}";
        }

        if (healthSlider != null)
        {
            healthSlider.value = newValue;
        }
    }
}
