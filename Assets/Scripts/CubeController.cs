using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    private CubeBehaviour cubeBehaviourComponent;

    private void Start()
    {
        cubeBehaviourComponent = gameObject.GetComponent<CubeBehaviour>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            cubeBehaviourComponent.RotateClockwise();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            cubeBehaviourComponent.RotateCounterclockwise();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            cubeBehaviourComponent.StopRotation();
        }

        if(cubeBehaviourComponent.RotationSpeed != 0)
        {
            cubeBehaviourComponent.UpdateRotation();
        }
    }
}
