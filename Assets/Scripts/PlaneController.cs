using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlaneController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float acceleration = 15f;
    public float deceleration = 20f;
    private float inputX;

    private Rigidbody rb;

    [Header("Footstep Sounds")]
    public AudioSource footstep1Sound;
    public AudioSource footstep2Sound;
    private bool playFirstFootstep = true;
    private float stepTimer = 0f;
    public float stepInterval = 1f; // Czas między krokami

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
    }

    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");

        bool isMoving = Mathf.Abs(inputX) > 0.1f;

        if (isMoving)
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f)
            {
                PlayFootstep();
                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = 0f; // Resetuje licznik gdy przestajesz się ruszać
        }
    }

    void FixedUpdate()
    {
        float targetVelocityX = inputX * moveSpeed;
        float velocityChangeX = targetVelocityX - rb.linearVelocity.x;

        float accelRate = Mathf.Abs(inputX) > 0.1f ? acceleration : deceleration;
        velocityChangeX = Mathf.Clamp(velocityChangeX, -accelRate * Time.fixedDeltaTime, accelRate * Time.fixedDeltaTime);

        rb.AddForce(new Vector3(velocityChangeX, 0f, 0f), ForceMode.VelocityChange);
    }

    void PlayFootstep()
    {
        if (playFirstFootstep && footstep1Sound != null)
            footstep1Sound.Play();
        else if (!playFirstFootstep && footstep2Sound != null)
            footstep2Sound.Play();

        playFirstFootstep = !playFirstFootstep;
    }
}