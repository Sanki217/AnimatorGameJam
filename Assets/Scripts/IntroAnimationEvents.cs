using UnityEngine;

public class IntroAnimationEvents : MonoBehaviour
{
    public IntroManager introManager; // drag SceneManager here in Inspector

    public void OnWakeUpAnimationEnd()
    {
        introManager.OnWakeUpAnimationEnd();
    }
}
