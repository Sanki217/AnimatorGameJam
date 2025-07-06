using UnityEngine;

public class AnimationPreloader : MonoBehaviour
{
    public Sprite[] wakeUpFrames;

    void Start()
    {
        // Force Unity to load all wakeUp frames into memory
        foreach (var sprite in wakeUpFrames)
        {
            // This forces loading of the texture
            Texture2D tex = sprite.texture;
        }
    }
}
