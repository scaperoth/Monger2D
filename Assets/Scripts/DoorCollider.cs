using UnityEngine;
using UnityEngine.Events;

public class DoorCollider : MonoBehaviour
{
    [SerializeField]
    int[] _characterLayers;
    [SerializeField]
    UnityEvent<Character, int> OnCharacterKilledByDoor;

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(_characterLayers == null)
        {
            return;
        }

        if (ChracterLayersContain(collision.gameObject.layer))
        {
            Character other = collision.gameObject.GetComponent<Character>();
            other.Die();
            OnCharacterKilledByDoor.Invoke(other, gameObject.layer);
        }
    }

    public bool ChracterLayersContain(int layer)
    {
        for (int i = 0; i < _characterLayers.Length; i++)
        {
            if(_characterLayers[i] == layer)
            {
                return true;
            }
        }

        return false;
    }
}
