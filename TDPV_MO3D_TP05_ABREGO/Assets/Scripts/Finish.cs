using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    Collider myCollider;

    private void Start()
    {
        myCollider = GetComponent<Collider>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 playerPosition = other.gameObject.transform.position;

            if (IsObjectFitting(other.gameObject))
            {
                GameEvents.TriggerVictory();
                other.gameObject.SetActive(false);
            }
        }
    }

    public bool IsObjectFitting(GameObject innerObject)
    {
        Collider innerCollider = innerObject.GetComponent<Collider>();

        if (innerCollider == null)
        {
            Debug.LogError("Both objects must have colliders.");
            return false;
        }

        // Compare the widths
        float innerWidth = innerCollider.bounds.size.x;
        float outerWidth = myCollider.bounds.size.x;

        return myCollider.bounds.Contains(innerCollider.bounds.min) && myCollider.bounds.Contains(innerCollider.bounds.max);
    }
}
