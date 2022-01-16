using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    GameState _gameState;
    [SerializeField]
    GameSettings _gameSettings;
    [SerializeField]
    GameObject _endGameOverlay;

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

    private void Start()
    {
        leftCastle.Health = _gameSettings.startingCastleHealth;
        rightCastle.Health = _gameSettings.startingCastleHealth;
    }

    private void Update()
    {
        bool horizPressed = Input.GetButtonDown("Horizontal");
        if (horizPressed)
        {
            TryOpenDoor();
        }

        if(_lastAutomaticHealthUpdate + _gameSettings.deterioratingHealthDelay < Time.time)
        {
            leftCastle.Health -= _gameSettings.deterioratingHealthValue;
            rightCastle.Health -= _gameSettings.deterioratingHealthValue;
            _lastAutomaticHealthUpdate = Time.time;
        }

        if(leftCastle.Health <= 0 || rightCastle.Health <= 0)
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
    }

    public void OnObjectDestroyedByCastle(Character character, int layerOfCastle)
    {
        string characterLayer = LayerMask.LayerToName(character.gameObject.layer);
        string castleLayer = LayerMask.LayerToName(layerOfCastle);

        Debug.Log($"{characterLayer} destroyed by {castleLayer} inside");

        // character goes into left castle
        if(castleLayer == _leftCastleLayer)
        {
            // if character is knight then take away health
            if(characterLayer == _rightKnightLayer)
            {
                float newScore = leftCastle.Health - _gameSettings.knightDamage;
                leftCastle.Health = newScore;
            }

            // if character is merchant then give health
            if (characterLayer == _leftMerchantLayer)
            {
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
                float newScore = rightCastle.Health - _gameSettings.knightDamage;
                rightCastle.Health = newScore;
            }

            // if character is merchant then give health
            if (characterLayer == _rightMerchantLayer)
            {
                float newScore = rightCastle.Health + _gameSettings.merchantBonus;
                rightCastle.Health = newScore;
            }
        }
    }

    void LoseConditionReached()
    {
        Time.timeScale = 0;
        _endGameOverlay.SetActive(true);
    }

    public void Restart()
    {
        Debug.Log("WOH, RESTARTING");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit()
    {
        Debug.Log("WOH, EXITING");
        Application.Quit();
    }
}
