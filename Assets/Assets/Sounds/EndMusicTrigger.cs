using UnityEngine;
using System.Collections;

public class EndMusicTrigger : MonoBehaviour
{
    public AudioSource endMusic;
    public float fadeDelay = 5f;     // Time after play before fade starts
    public float fadeDuration = 3f;  // Fade-out time in seconds

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return;

        if (other.CompareTag("Player"))
        {
            hasTriggered = true;
            endMusic.volume = 0.5f;
            endMusic.Play();
            StartCoroutine(FadeOutCoroutine());
        }
    }

    private IEnumerator FadeOutCoroutine()
    {
        yield return new WaitForSeconds(fadeDelay);

        float startVolume = endMusic.volume;
        float time = 0f;

        while (time < fadeDuration)
        {
            endMusic.volume = Mathf.Lerp(startVolume, 0f, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        endMusic.volume = 0f;
        endMusic.Stop();
    }
}
