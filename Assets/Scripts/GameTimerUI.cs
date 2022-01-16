using UnityEngine;
using TMPro;

public class GameTimerUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _timerUI;

    private void Update()
    {
        _timerUI.text = $"Timer {Time.time.ToString("F2")}s";
    }
}
