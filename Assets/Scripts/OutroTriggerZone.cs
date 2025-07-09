using UnityEngine;

public class OutroTriggerZone : MonoBehaviour
{
    public GameObject outroObject;      // Object with Animator (disabled at start)

    private bool triggered = false;


    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            outroObject.SetActive(true); // Show the outro animation object

        }
    }
}
