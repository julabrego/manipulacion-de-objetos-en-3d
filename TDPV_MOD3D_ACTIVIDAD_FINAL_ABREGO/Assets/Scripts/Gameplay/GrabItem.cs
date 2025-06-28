using Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabItem : MonoBehaviour
{
    public GameObject grabbingItem;
    public Camera playerCamera;

    private bool isInGrabArea = false;
    private GameObject itemToGrab;
    public FPSController playerController; 

    // Update is called once per frame
    void Update()
    {
        if (isInGrabArea && Input.GetMouseButtonDown(0) && grabbingItem == null)
        {
            grabbingItem = itemToGrab;
        }
        else if (grabbingItem != null && Input.GetMouseButtonDown(0))
        {
            Rigidbody rb = grabbingItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = true;

                Vector3 throwDirection = playerCamera.transform.forward.normalized;

                // Obtener la velocidad real del jugador
                Vector3 playerVelocity = playerController.currentVelocity;

                // Usar la magnitud de la velocidad como fuerza hacia donde mira
                float launchSpeed = playerVelocity.magnitude;

                rb.velocity = throwDirection * launchSpeed + playerVelocity * 0.6f; // opcional: suma parte de la velocidad
            }

            grabbingItem = null;
        }

        if (grabbingItem != null)
        {
            Vector3 holdOffset = playerCamera.transform.forward * 0.5f
                + playerCamera.transform.up * -0.3f
                + playerCamera.transform.right * 0.3f;
            grabbingItem.transform.SetPositionAndRotation(
                playerCamera.transform.position + holdOffset,
                Quaternion.LookRotation(playerCamera.transform.forward) * Quaternion.Euler(-45f, -75f, 0f)
            );
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GrabArea"))
        {
            isInGrabArea = true;

            if (other.CompareTag("GrabArea") && other.transform.parent != null)
            {
                itemToGrab = other.transform.parent.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GrabArea"))
        {
            isInGrabArea = false;
            itemToGrab = null;
        }
    }
}
