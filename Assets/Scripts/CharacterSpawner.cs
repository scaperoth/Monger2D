using UnityEngine;
using System.Collections;

public class CharacterSpawner : MonoBehaviour
{
    public int knightLayer = 0;
    public PooledObject character;
    [SerializeField]
    Transform _target;

    private float timer = 0f;

    public void Spawn()
    {
        float spawnYOffset = Random.Range(-.1f, .1f);
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + spawnYOffset, transform.position.z);
        // Spawn object with random 2D rotation.
        PooledObject instance = Pool.Instance.Spawn(character, spawnPosition, Quaternion.identity);
        // We can avoid GetComponent<>() for a frequently accessed component, which is nice.
        instance.As<Character>().Init(_target, knightLayer);
    }


    public void Spawn(int number, float spawnRate)
    {
        StartCoroutine(SpawnNumber(number, spawnRate));
    }

    public void Spawn(int number, float minInterval, float maxInterval)
    {
        StartCoroutine(SpawnNumber(number, minInterval, maxInterval));
    }

    IEnumerator SpawnNumber(int number, float interval)
    {
        int count = 0;

        while (count < number)
        {
            count++;
            Spawn();
            yield return new WaitForSecondsRealtime(interval);
        }
    }

    IEnumerator SpawnNumber(int number, float minInterval, float maxInterval)
    {
        int count = 0;
        float spawnInterval = Random.Range(minInterval, maxInterval);

        while (count < number)
        {
            count++;
            Spawn();
            yield return new WaitForSecondsRealtime(spawnInterval);
            spawnInterval = Random.Range(minInterval, maxInterval);
        }
    }
}
