using UnityEngine;
using System.Collections;

public class IntroManager : MonoBehaviour
{
    public GameObject introObject;
    public GameObject mainGameObject;
    public AudioSource alarmClockSound;
    public AudioSource parkAmbientSound;

    private bool triggered = false;

    void Start()
    {
        introObject.SetActive(true);
        mainGameObject.SetActive(false);
        parkAmbientSound.Play();
    }

    void Update()
    {
        if (!triggered && Input.GetKeyDown(KeyCode.Space))
        {
            triggered = true;
            alarmClockSound.Play();
            StartCoroutine(SwitchToMainGame());
        }
    }

    IEnumerator SwitchToMainGame()
    {
        yield return new WaitForSeconds(5f);
        introObject.SetActive(false);
        parkAmbientSound.Stop();
        mainGameObject.SetActive(true);
    }
}