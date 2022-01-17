using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    GameSettings _gameSettings;
    [SerializeField]
    GameObject _endGameOverlay;
    [SerializeField]
    GameObject _startGameOverlay;
    [SerializeField]
    SoundManager _soundManager;
    [SerializeField]
    AudioSource _musicAudio;
    [SerializeField]
    AudioSource _sfxAudio;

    // layer names
    string _leftKnightLayer = "Left Knight";
    string _rightKnightLayer = "Right Knight";
    string _leftMerchantLayer = "Left Merchant";
    string _rightMerchantLayer = "Right Merchant";
    string _leftCastleLayer = "Left Castle";
    string _rightCastleLayer = "Right Castle";

    [SerializeField]
    Castle leftCastle;
    [SerializeField]
    Castle rightCastle;

    float _lastAutomaticHealthUpdate = 0f;
    float _timer = 0f;

    private void Start()
    {
        Time.timeScale = 0;
        leftCastle.Health = _gameSettings.startingCastleHealth;
        rightCastle.Health = _gameSettings.startingCastleHealth;

        _musicAudio.playOnAwake = true;
        _musicAudio.loop = true;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        bool horizPressed = Input.GetButtonDown("Horizontal");
        if (horizPressed)
        {
            TryOpenDoor();
        }

        if (_lastAutomaticHealthUpdate + _gameSettings.deterioratingHealthDelay < _timer)
        {
            leftCastle.Health -= _gameSettings.deterioratingHealthValue;
            rightCastle.Health -= _gameSettings.deterioratingHealthValue;
            _lastAutomaticHealthUpdate = _timer;
        }

        if (leftCastle.Health <= 0 || rightCastle.Health <= 0)
        {
            LoseConditionReached();
        }
    }

    private void TryOpenDoor()
    {
        float horizValue = Input.GetAxis("Horizontal");
        if (horizValue < 0)
        {
            leftCastle.ChangeDoor();
        }
        else if (horizValue > 0)
        {
            rightCastle.ChangeDoor();
        }
    }

    public void OnObjectDestroyedByDoor(Character character, int layerOfCastle)
    {
        string characterLayer = LayerMask.LayerToName(character.gameObject.layer);
        string castleLayer = LayerMask.LayerToName(layerOfCastle);

        Debug.Log($"{characterLayer} destroyed by {castleLayer} door");
        _sfxAudio.PlayOneShot(_soundManager.characterHurtSound, .3f);
    }

    public void OnObjectDestroyedByCastle(Character character, int layerOfCastle)
    {
        string characterLayer = LayerMask.LayerToName(character.gameObject.layer);
        string castleLayer = LayerMask.LayerToName(layerOfCastle);

        Debug.Log($"{characterLayer} destroyed by {castleLayer} inside");

        // character goes into left castle
        if (castleLayer == _leftCastleLayer)
        {
            // if character is knight then take away health
            if (characterLayer == _rightKnightLayer)
            {
                _sfxAudio.PlayOneShot(_soundManager.knightSound);
                float newScore = leftCastle.Health - _gameSettings.knightDamage;
                leftCastle.Health = newScore;
            }

            // if character is merchant then give health
            if (characterLayer == _leftMerchantLayer)
            {
                _sfxAudio.PlayOneShot(_soundManager.merchantSound);
                float newScore = leftCastle.Health + _gameSettings.merchantBonus;
                leftCastle.Health = newScore;
            }
        }

        // character goes into left castle
        if (castleLayer == _rightCastleLayer)
        {
            // if character is knight then take away health
            if (characterLayer == _leftKnightLayer)
            {
                _sfxAudio.PlayOneShot(_soundManager.knightSound);
                float newScore = rightCastle.Health - _gameSettings.knightDamage;
                rightCastle.Health = newScore;
            }

            // if character is merchant then give health
            if (characterLayer == _rightMerchantLayer)
            {
                _sfxAudio.PlayOneShot(_soundManager.merchantSound);
                float newScore = rightCastle.Health + _gameSettings.merchantBonus;
                rightCastle.Health = newScore;
            }
        }
    }

    void LoseConditionReached()
    {
        _sfxAudio.Stop();
        _musicAudio.Stop();
        _musicAudio.PlayOneShot(_soundManager.gameOverSound);
        Time.timeScale = 0;
        _endGameOverlay.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Play()
    {
        _musicAudio.Stop();
        _musicAudio.PlayOneShot(_soundManager.mainGameSound);
        Time.timeScale = 1;
        _startGameOverlay.SetActive(false);

    }

    public void DoorStateChanged(bool isOpen)
    {
        if (!isOpen)
        {
            _sfxAudio.PlayOneShot(_soundManager.doorSlamSound);
        }
        else
        {
            _sfxAudio.PlayOneShot(_soundManager.doorOpenSound, .5f);
        }
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
         Application.OpenURL(webplayerQuitURL);
#else
         Application.Quit();
#endif
    }
}
