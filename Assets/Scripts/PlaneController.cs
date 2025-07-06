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
    public float stepInterval = 1f;

    [HideInInspector] public bool canMove = true;

    [Header("Visual & Animation")]
    public Transform visualTransform; // drag your Visual child here
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;

        if (visualTransform != null)
        {
            animator = visualTransform.GetComponent<Animator>();
            spriteRenderer = visualTransform.GetComponent<SpriteRenderer>();
        }
    }

    void Update()
    {
        if (!canMove)
        {
            inputX = 0f;
            return;
        }

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
            stepTimer = 0f;
        }

        // Animate
        if (animator != null)
            animator.SetFloat("Speed", Mathf.Abs(inputX));

        // Flip sprite
        if (spriteRenderer != null && Mathf.Abs(inputX) > 0.1f)
            spriteRenderer.flipX = inputX < 0;
    }

    void FixedUpdate()
    {
        if (!canMove) return;

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
