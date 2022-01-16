using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    Castle leftCastle;
    [SerializeField]
    Castle rightCastle;

    private void Update()
    {
        bool horizPressed = Input.GetButtonDown("Horizontal");
        if (horizPressed)
        {
            TryOpenDoor();
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
}
