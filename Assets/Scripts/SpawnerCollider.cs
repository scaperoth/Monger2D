using UnityEngine;
using UnityEngine.Events;

public class SpawnerCollider : MonoBehaviour
{
    [SerializeField]
    int[] _destructionLayers;
    [SerializeField]
    UnityEvent<Character, int> OnCharacterEnterCastle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_destructionLayers == null)
        {
            return;
        }

        if (DestructionLayersContain(collision.gameObject.layer))
        {
            PooledObject other = collision.gameObject.GetComponent<PooledObject>();
            Character character = other.GetComponent<Character>();
            other.Finish();
            OnCharacterEnterCastle.Invoke(character, gameObject.layer);
        }
    }

    public bool DestructionLayersContain(int layer)
    {
        for (int i = 0; i < _destructionLayers.Length; i++)
        {
            if (_destructionLayers[i] == layer)
            {
                return true;
            }
        }

        return false;
    }
}
