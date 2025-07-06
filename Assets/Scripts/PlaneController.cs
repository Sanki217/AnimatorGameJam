using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlaneController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float acceleration = 15f;
    public float deceleration = 20f;
    private float inputX;
    private Vector3 currentVelocity;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Zamra�amy rotacj� i ruch w osi Z, czyli tylko X si� porusza
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
    }

    void Update()
    {
        // Tylko ruch w osi X
        inputX = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        // Ruch tylko w osi X
        float targetVelocityX = inputX * moveSpeed;
        float velocityChangeX = targetVelocityX - rb.linearVelocity.x;

        float accelRate = Mathf.Abs(inputX) > 0.1f ? acceleration : deceleration;
        velocityChangeX = Mathf.Clamp(velocityChangeX, -accelRate * Time.fixedDeltaTime, accelRate * Time.fixedDeltaTime);

        rb.AddForce(new Vector3(velocityChangeX, 0f, 0f), ForceMode.VelocityChange);
    }
}