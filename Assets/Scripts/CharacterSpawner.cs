using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public float spawnRate = 0.1f;
    public PooledObject character;
    [SerializeField]
    Transform _target;
    int count = 0;

    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;
        if (count < 1 && timer > spawnRate)
        {
            count++;
            timer -= spawnRate;

            // Spawn object with random 2D rotation.
            PooledObject instance = Pool.Instance.Spawn(character, transform.position, Quaternion.identity);
            // We can avoid GetComponent<>() for a frequently accessed component, which is nice.
            instance.As<Character>().SetTargetTransform(_target);

        }
    }
}
