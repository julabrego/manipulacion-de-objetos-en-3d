using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private BoxCollider coll;

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 7f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector3(dirX * moveSpeed, rb.velocity.y, 0f);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0f);
        }
    }

    private bool IsGrounded()
    {
        float rayLength = coll.bounds.extents.y + 0.1f;
        return Physics.Raycast(transform.position, Vector3.down, rayLength);
    }


}
