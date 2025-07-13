using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverArea : MonoBehaviour
{
    public string collectableItemTag = "";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(collectableItemTag))
        {
            print("ritggering");
            GameEvents.TriggerAddItem();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(collectableItemTag))
        {
            GameEvents.TriggerSubstractItem();
        }
    }
}
