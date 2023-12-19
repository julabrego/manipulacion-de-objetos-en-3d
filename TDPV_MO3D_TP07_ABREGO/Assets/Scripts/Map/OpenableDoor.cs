using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenableDoor : MonoBehaviour
{
    private bool isOpen;
    public bool IsOpen { get => isOpen; set => isOpen = value; }

    private void Update()
    {
        if (isOpen && transform.position.y < 5.34)
        {
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            transform.position = newPosition;
        }

        if (!isOpen && transform.position.y > 0.5)
        {
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
            transform.position = newPosition;

        }
    }
}
