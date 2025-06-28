using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneDirectionDoor : MonoBehaviour
{
    [SerializeField] private OpenableDoor targetDoor;
    [SerializeField] private bool mustHold = true;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetDoor.IsOpen = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && mustHold)
        {
            targetDoor.IsOpen = false;
        }
    }
}
