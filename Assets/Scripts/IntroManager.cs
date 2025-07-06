using UnityEngine;

public class IntroManager : MonoBehaviour
{
    [Header("Animator Setup")]
    public Animator introAnimator; // reference to Animator on your intro object

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
            introAnimator.SetTrigger("WakeUp"); // Set up this trigger in your Animator
            // Play sounds
            alarmClockSound.Play();
            parkAmbientSound.Stop();
            hospitalAmbientSound.Play();
            heartMonitorSound.Play();

          
             
        }
    }
}
