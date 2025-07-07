using UnityEngine;

public class IntroManager : MonoBehaviour
{
    [Header("References")]
    public GameObject introObject; // <<< assign your animated object here
    public Animator introAnimator; // <<< assign Animator from that same object

    [Header("Audio")]
    public AudioSource alarmClockSound;
    public AudioSource parkAmbientSound;
    public AudioSource hospitalAmbientSound;
    public AudioSource heartMonitorSound;
    public GameObject playerObject; // Drag player object here
    private PlaneController playerController;

    private bool triggered = false;

    void Start()
    {
        parkAmbientSound.Play();
        playerController = playerObject.GetComponent<PlaneController>();
        playerController.canMove = false; // <<< Disable movement at start
    }

    void Update()
    {
        if (!triggered && Input.GetKeyDown(KeyCode.Space))
        {
            triggered = true;
            introAnimator.SetTrigger("WakeUp"); // trigger animation transition

            // Play sounds
            alarmClockSound.Play();
            parkAmbientSound.Stop();
            hospitalAmbientSound.Play();
            heartMonitorSound.Play();
        }
    }

    // This will be called via Animation Event
    public void OnWakeUpAnimationEnd()
    {
        introObject.SetActive(false); // <<< DISABLES the animated object
        playerController.canMove = true; // <<< Enable movement now
    }
}
