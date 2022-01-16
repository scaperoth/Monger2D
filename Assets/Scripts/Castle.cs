using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    [SerializeField]
    Door _door;
    [SerializeField]
    CharacterSpawner _spawner;

    float _spawnDelay = 5f;
    float _lastSpawnTime = 0;
    float _minspawnRate = 10f;
    float _maxspawnRate = 20f;

    bool _doorChanging = false;

    private void Start()
    {
        _spawnDelay = Random.Range(1f, 5f);
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
    }

    public void SpawnKnights()
    {
        if(_spawner == null || _spawner.enabled == false)
        {
            return;
        }

        if (_lastSpawnTime + _spawnDelay < Time.time)
        {
            _spawner.Spawn(5, .5f, 1f);
            _spawnDelay = Random.Range(_minspawnRate, _maxspawnRate);
            ResetSeed();
            _lastSpawnTime = Time.time;
        }
    }

    void ResetSeed()
    {
        int value = (int)(_spawnDelay * 10);
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
