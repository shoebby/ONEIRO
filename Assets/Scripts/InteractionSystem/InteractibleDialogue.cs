using UnityEngine;
using TMPro;

public class InteractibleDialogue : MonoBehaviour, IInteractable
{
    [Header("Prompt")]
    [SerializeField] private string prompt;
    [SerializeField] private string startPrompt;
    [SerializeField] private string nextDialoguePrompt;

    [Header("InteractionData")]
    [TextArea(5, 20)]
    [SerializeField] private string[] dialogueLines;
    [SerializeField] private int currentDialogue;
    [SerializeField] private Canvas canvas;
    [SerializeField] private TextMeshPro textMesh;
    [SerializeField] private Camera mainCam;
    [SerializeField] private float maxDist;

    public string InteractionPrompt => prompt;

    private void Start()
    {
        mainCam = Camera.main;
        ResetDialogue();
    }

    private void Update()
    {
        if (canvas.enabled && Vector3.Distance(transform.position, mainCam.transform.position) >= maxDist)
            ResetDialogue();
    }

    public bool Interact(Interactor interactor)
    {
        if (canvas.enabled == false)
            ToggleCanvas(true, nextDialoguePrompt);
        
        if (currentDialogue == dialogueLines.Length)
        {
            ResetDialogue();
            return true;
        }

        textMesh.text = dialogueLines[currentDialogue];
        currentDialogue += 1;

        return true;
    }

    private void ResetDialogue()
    {
        ToggleCanvas(false, startPrompt);
        currentDialogue = 0;
        textMesh.text = dialogueLines[currentDialogue];
    }

    private void ToggleCanvas(bool canvasState, string setPrompt)
    {
        prompt = setPrompt;
        canvas.enabled = canvasState;
        textMesh.enabled = canvasState;
    }
}
