using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    public bool isGrounded;
    public float groundCheckDistance = 0.1f;

    Quaternion targetRotation;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetRotation = transform.rotation; 
    }

    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance + 0.01f);
        print(isGrounded);
    }

    void FixedUpdate()
    {
        if (isGrounded)
        {
            float angleDiff = Quaternion.Angle(transform.rotation, targetRotation);

            if (angleDiff > 5f) 
            {
                rb.angularVelocity = Vector3.zero;

                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 1f);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
    }
}
