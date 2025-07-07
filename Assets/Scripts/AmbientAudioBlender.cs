using UnityEngine;

public class AmbientAudioBlender : MonoBehaviour
{
    public AudioSource cityAmbientSource;
    public AudioSource parkAmbientSource;
    public Transform player;
    public GameObject intro;

    [Header("Blending Settings")]
    public float startBlendX = 10f;
    public float endBlendX = 30f;

    private bool cityStarted = false;

    private void Update()
    {
        // Je?li intro jest wy??czone i d?wi?k miasta jeszcze nie zacz?? gra?
        if (!intro.activeInHierarchy && !cityStarted)
        {
            cityAmbientSource.volume = 1f;
            cityAmbientSource.Play();
            cityStarted = true;
        }

        // Blendujemy tylko je?li cityAmbient ju? gra
        if (cityStarted)
        {
            float playerX = player.position.x;
            float t = Mathf.InverseLerp(startBlendX, endBlendX, playerX);

            float parkVolume = Mathf.SmoothStep(0f, 1f, t);
            float cityVolume = 1f - parkVolume;

            cityAmbientSource.volume = cityVolume;
            parkAmbientSource.volume = parkVolume;

            if (!parkAmbientSource.isPlaying)
                parkAmbientSource.Play();
        }
    }
}