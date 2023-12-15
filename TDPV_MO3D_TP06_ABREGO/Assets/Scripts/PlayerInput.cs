using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            GameEvents.TriggerRestart();

        if (Input.GetButtonDown("Jump"))
            GameEvents.TriggerStartCharging();

        if (Input.GetButtonUp("Jump"))
            GameEvents.TriggerShoot();
    }
}
