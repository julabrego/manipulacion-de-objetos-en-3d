using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickActionIconToggler : MonoBehaviour
{
    [SerializeField] private GameObject leftClickActionIcon;
    [SerializeField] private GameObject rightClickActionIcon;

    public bool isBeingGrabbed = false;

    // Start is called before the first frame update
    void Start()
    {
        leftClickActionIcon.SetActive(false);
        rightClickActionIcon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isBeingGrabbed)
        {
            leftClickActionIcon.SetActive(false);
            rightClickActionIcon.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var playerGrabItem = other.GetComponent<GrabItem>();
            if (playerGrabItem != null)
            {
                if (playerGrabItem.grabbingItemWithLeftHand == null)
                {
                    leftClickActionIcon.SetActive(true);
                }
                if (playerGrabItem.grabbingItemWithRightHand == null)
                {
                    rightClickActionIcon.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            leftClickActionIcon.SetActive(false);
            rightClickActionIcon.SetActive(false);
        }
    }
}
