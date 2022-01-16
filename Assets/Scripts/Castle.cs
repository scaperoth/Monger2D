using UnityEngine;
using UnityEngine.Events;

public class Castle : MonoBehaviour
{
    [SerializeField]
    Door _door;
    [SerializeField]
    CharacterSpawner _knightSpawner;
    [SerializeField]
    CharacterSpawner _merchantSpawner;

    public UnityEvent<float> OnScoreChanged;

    float _health;
    public float Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            OnScoreChanged.Invoke(value);
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
        if(!_doorChanging && _door.isOpen)
        {
            SpawnKnights();
        }

        SpawnMerchants();
    }

    public void SpawnKnights()
    {
        if(_knightSpawner == null || _knightSpawner.enabled == false)
        {
            return;
        }

        if (_lastKnightSpawnTime + _knightSpawnDelay < Time.time)
        {
            _knightSpawner.Spawn(5, .5f, 1f);
            _knightSpawnDelay = Random.Range(_minKnightSpawnRate, _maxKnightSpawnRate);
            ResetSeed();
            _lastKnightSpawnTime = Time.time;
        }
    }

    public void SpawnMerchants()
    {
        if (_merchantSpawner == null || _merchantSpawner.enabled == false)
        {
            return;
        }

        if (_lastMerchantSpawnTime + _merchantSpawnDelay < Time.time)
        {
            _merchantSpawner.Spawn(5, .5f, 1f);
            _merchantSpawnDelay = Random.Range(_minMerchantSpawnRate, _maxMerchantSpawnRate);
            ResetSeed();
            _lastMerchantSpawnTime = Time.time;
        }
    }

    void ResetSeed()
    {
        int value = (int)(_knightSpawnDelay * 10);
        Random.InitState(value);
    }

    // Start is called before the first frame update
    public void OnDoorChange(Door door)
    {
        _doorChanging = false;
    }

    public void OpenDoor(bool open)
    {
        if(_door.isOpen == open)
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

        OpenDoor(!_door.isOpen);
    }
}
