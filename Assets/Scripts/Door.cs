using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    Animator _animator;
    public UnityEvent<Door> OnDoorChange;
    public bool isOpen { get; private set; }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Open(bool doorOpen)
    {
        isOpen = doorOpen;
        _animator.SetBool("DoorOpen", doorOpen);
    }

    public void ChangeComplete(int open)
    {
        OnDoorChange.Invoke(this);
    }
}
