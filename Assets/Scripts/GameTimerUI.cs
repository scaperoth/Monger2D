using UnityEngine;
using TMPro;

public class GameTimerUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _timerUI;
    float _timer = 0f;

    private void Update()
    {
        _timer += Time.deltaTime;
        _timerUI.text = $"Timer {_timer.ToString("F2")}s";
    }
}
