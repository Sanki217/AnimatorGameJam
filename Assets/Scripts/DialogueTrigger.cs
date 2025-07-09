using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("References")]
    public GameObject interactionPrompt;       // Icon or text saying "Press E"
    public GameObject[] dialoguePlanes;        // Dialogue GameObjects (planes with textures)

    [Header("Final NPC Options")]
    public bool isFinalNPC = false;            // Toggle this in the Inspector
    public GameObject outroObject;             // Assign the outro animation object here
    public GameObject playerObject;            // Assign the player here (to disable movement)

    private bool isPlayerNear = false;
    private bool isDialogueActive = false;
    private int currentDialogueIndex = 0;
    public EndMusicFader endMusicFader; // Drag reference in Inspector


    private void Start()
    {
        interactionPrompt.SetActive(false);
        foreach (GameObject plane in dialoguePlanes)
            plane.SetActive(false);

        if (outroObject != null)
            outroObject.SetActive(false);
    }

    private void Update()
    {
        if (!isPlayerNear) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isDialogueActive)
            {
                // Begin dialogue
                interactionPrompt.SetActive(false);
                dialoguePlanes[0].SetActive(true);
                currentDialogueIndex = 0;
                isDialogueActive = true;
            }
            else
            {
                // Continue dialogue
                dialoguePlanes[currentDialogueIndex].SetActive(false);
                currentDialogueIndex++;

                if (currentDialogueIndex >= dialoguePlanes.Length)
                {
                    isDialogueActive = false; // End dialogue
                    Destroy(interactionPrompt);

                    if (isFinalNPC)
                        TriggerOutro();
                    endMusicFader.PlayAndFadeOut();
                }
                else
                {
                    dialoguePlanes[currentDialogueIndex].SetActive(true);
                }
            }
        }
    }

    private void TriggerOutro()
    {
        if (outroObject != null)
            outroObject.SetActive(true);

        if (playerObject != null)
        {
            // Disable all movement scripts on player
            MonoBehaviour[] components = playerObject.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour comp in components)
            {
                if (comp.enabled && comp.GetType().Name.ToLower().Contains("move"))
                    comp.enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isDialogueActive)
        {
            isPlayerNear = true;
            interactionPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            interactionPrompt.SetActive(false);

            if (isDialogueActive)
            {
                foreach (GameObject plane in dialoguePlanes)
                    plane.SetActive(false);
                isDialogueActive = false;
            }
        }
    }
}
