using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Castle : MonoBehaviour
{
    [SerializeField]
    Door _door;
    [SerializeField]
    CharacterSpawner _knightSpawner;
    [SerializeField]
    CharacterSpawner _merchantSpawner;

    public UnityEvent<float> OnScoreChanged;
    public UnityEvent<bool> OnDoorStateChanged;

    float _health;
    public float Health
    {
        get
        {
            return _health;
        }
        set
        {
            float clampedValue = Mathf.Clamp(value, 0, 999);
            _health = clampedValue;
            OnScoreChanged.Invoke(clampedValue);
        }
    }

    float _knightSpawnDelay = 5f;
    float _lastKnightSpawnTime = 0;
    float _minKnightSpawnRate = 10f;
    float _maxKnightSpawnRate = 20f;


    float _merchantSpawnDelay = 5f;
    float _lastMerchantSpawnTime = 0;
    float _minMerchantSpawnRate = 10f;
    float _maxMerchantSpawnRate = 20f;

    bool _doorChanging = false;

    float _timer = 0f;

    private void Start()
    {
        _knightSpawnDelay = Random.Range(1f, 5f);
        ResetSeed();
    }

    bool DoorOpen()
    {
        return _door.isOpen;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (!_doorChanging && _door.isOpen)
        {
            SpawnKnights();
        }

        SpawnMerchants();
    }

    public void SpawnKnights()
    {
        if (_knightSpawner == null || _knightSpawner.enabled == false)
        {
            return;
        }

        if (_lastKnightSpawnTime + _knightSpawnDelay < _timer)
        {
            _knightSpawner.Spawn(5, .5f, 1f);
            _knightSpawnDelay = Random.Range(_minKnightSpawnRate, _maxKnightSpawnRate);
            ResetSeed();
            _lastKnightSpawnTime = _timer;
        }
    }

    public void SpawnMerchants()
    {
        if (_merchantSpawner == null || _merchantSpawner.enabled == false)
        {
            return;
        }

        if (_lastMerchantSpawnTime + _merchantSpawnDelay < _timer)
        {
            _merchantSpawner.Spawn(5, .5f, 1f);
            _merchantSpawnDelay = Random.Range(_minMerchantSpawnRate, _maxMerchantSpawnRate);
            ResetSeed();
            _lastMerchantSpawnTime = _timer;
        }
    }

    void ResetSeed()
    {
        int value = (int)(_knightSpawnDelay * 10);
        Random.InitState(value);
    }

    public void OnDoorChange(Door door)
    {
        _doorChanging = false;
    }

    public void OpenDoor(bool open)
    {
        if (_door.isOpen == open)
        {
            return;
        }
        _doorChanging = true;
        _door.Open(open);
    }

    public void ChangeDoor()
    {
        if (_doorChanging)
        {
            return;
        }

        if (_door.isOpen == true)
        {
            StartCoroutine(PlayDoorSound());
        }
        else
        {
            OnDoorStateChanged.Invoke(true);
        }
        OpenDoor(!_door.isOpen);
    }

    IEnumerator PlayDoorSound()
    {
        yield return new WaitForSecondsRealtime(.5f);

        OnDoorStateChanged.Invoke(false);
    }
}
