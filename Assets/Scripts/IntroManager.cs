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

    [Header("Environment")]
    public GameObject terrainObject; // Drag your Terrain here in the Inspector

    private bool triggered = false;

    void Start()
    {
        parkAmbientSound.Play();
        playerController = playerObject.GetComponent<PlaneController>();
        playerController.canMove = false; // <<< Disable movement at start
        terrainObject.SetActive(false); // Hide terrain during intro

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
        terrainObject.SetActive(true);
    }
}
