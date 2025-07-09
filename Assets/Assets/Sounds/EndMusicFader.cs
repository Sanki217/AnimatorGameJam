using UnityEngine;
using System.Collections;

public class EndMusicFader : MonoBehaviour
{
    public AudioSource endMusic;
    public float fadeDelay = 5f;     // Seconds before fading starts
    public float fadeDuration = 3f;  // Duration of fade

    public void PlayAndFadeOut()
    {
        endMusic.volume = 1f;
        endMusic.Play();
        StartCoroutine(FadeOutCoroutine());
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
