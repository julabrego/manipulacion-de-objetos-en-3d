using Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabItem : MonoBehaviour
{
    public Camera playerCamera;

    public GameObject grabbingItemWithRightHand;
    public GameObject grabbingItemWithLeftHand;
    private GameObject itemToGrab;

    public FPSController playerController;
    private bool isInGrabArea = false;

    int LEFT_BUTTON = 0;
    int RIGHT_BUTTON = 1;

    void Update()
    {
        if (!GameManager.Instance.GetIsPlaying())
        {
            return;
        }

        else if (grabbingItemWithRightHand != null && !Input.GetMouseButton(RIGHT_BUTTON))
        {
            Rigidbody rb = grabbingItemWithRightHand.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = true;

                // TODO: Extract this blockto a self descriptive function
                Vector3 throwDirection = playerCamera.transform.forward.normalized;
                // Obtener la velocidad real del jugador
                Vector3 playerVelocity = playerController.currentVelocity;
                // Usar la magnitud de la velocidad como fuerza hacia donde mira
                float launchSpeed = playerVelocity.magnitude;
                rb.velocity = throwDirection * launchSpeed + playerVelocity * 0.6f; // opcional: suma parte de la velocidad
            }

            grabbingItemWithRightHand = null;
        }
        else if (grabbingItemWithLeftHand != null && !Input.GetMouseButton(LEFT_BUTTON))
        {
            Rigidbody rb = grabbingItemWithLeftHand.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = true;

                // TODO: Extract this blockto a self descriptive function
                Vector3 throwDirection = playerCamera.transform.forward.normalized;
                // Obtener la velocidad real del jugador
                Vector3 playerVelocity = playerController.currentVelocity;
                // Usar la magnitud de la velocidad como fuerza hacia donde mira
                float launchSpeed = playerVelocity.magnitude;
                rb.velocity = throwDirection * launchSpeed + playerVelocity * 0.6f; // opcional: suma parte de la velocidad
            }

            grabbingItemWithLeftHand = null;
        }
        else
        {
            if (isInGrabArea && Input.GetMouseButton(RIGHT_BUTTON) && grabbingItemWithRightHand == null)
            {
                grabbingItemWithRightHand = itemToGrab;
            }
            if (isInGrabArea && Input.GetMouseButton(LEFT_BUTTON) && grabbingItemWithLeftHand == null)
            {
                grabbingItemWithLeftHand = itemToGrab;
            }
        }

        if (grabbingItemWithRightHand != null || grabbingItemWithLeftHand != null)
        {
            bool hasItemWithTwoHands = grabbingItemWithLeftHand == grabbingItemWithRightHand;
            Vector3 holdOffset;

            GameObject itemBeingGrabbed = grabbingItemWithRightHand != null ? grabbingItemWithRightHand : grabbingItemWithLeftHand;

            if (hasItemWithTwoHands)
            {
                holdOffset = playerCamera.transform.forward * 0.5f
                    + playerCamera.transform.up * -0.3f
                    + playerCamera.transform.right * 0.02f;

                itemBeingGrabbed.transform.SetPositionAndRotation(
                playerCamera.transform.position + holdOffset,
                Quaternion.LookRotation(playerCamera.transform.forward) * Quaternion.Euler(-45f, 0f, 0f));
            }
            else
            {
                float rightOffset;
                float YRotation;

                if (grabbingItemWithRightHand)
                {
                    rightOffset = 0.3f;
                    YRotation = -75f;

                    // TODO: Extract this block to a self descriptive function
                    holdOffset = playerCamera.transform.forward * 0.5f
                        + playerCamera.transform.up * -0.3f
                        + playerCamera.transform.right * rightOffset;
                    grabbingItemWithRightHand.transform.SetPositionAndRotation(
                        playerCamera.transform.position + holdOffset,
                        Quaternion.LookRotation(playerCamera.transform.forward) * Quaternion.Euler(-45f, YRotation, 0f)
                    );
                }

                if (grabbingItemWithLeftHand)
                {
                    rightOffset = -0.3f;
                    YRotation = 75f;

                    // TODO: Extract this block to a self descriptive function
                    holdOffset = playerCamera.transform.forward * 0.5f
                        + playerCamera.transform.up * -0.3f
                        + playerCamera.transform.right * rightOffset;
                    grabbingItemWithLeftHand.transform.SetPositionAndRotation(
                        playerCamera.transform.position + holdOffset,
                        Quaternion.LookRotation(playerCamera.transform.forward) * Quaternion.Euler(-45f, YRotation, 0f)
                    );

                }

            }
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
