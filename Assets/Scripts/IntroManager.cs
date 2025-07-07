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

    private bool triggered = false;

    void Start()
    {
        parkAmbientSound.Play();
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
    }
}
