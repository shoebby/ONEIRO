using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using HauntedPSX.RenderPipelines.PSX.Runtime;

public class InteractibleDialogueSpecial965 : MonoBehaviour, IInteractable
{
    [Header("Prompt")]
    [SerializeField] private string prompt;
    [SerializeField] private string startPrompt;
    [SerializeField] private string nextDialoguePrompt;

    [Header("InteractionData")]
    [TextArea(3, 20)]
    [SerializeField] private string[] dialogueLines;
    [SerializeField] private int currentDialogue;
    [SerializeField] private Canvas canvas;
    [SerializeField] private TextMeshPro textMesh;
    [SerializeField] private Camera mainCam;
    [SerializeField] private float maxDist;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private BoxCollider interactionCollider;
    [SerializeField] private float colourAlpha;
    [SerializeField] private AudioSource pitchSource;

    [Header("Haunted PSX Variables")]
    [SerializeField] private Volume volume;
    [SerializeField] private CathodeRayTubeVolume CRTvolume;
    [SerializeField] private PrecisionVolume precVolume;

    [SerializeField] private float noiseIntensityModifier;
    [SerializeField] private float vertexWobbleModifier;
    [SerializeField] private float pitchVolumeModifier;

    public string InteractionPrompt => prompt;

    private void Start()
    {
        if (volume.profile.TryGet(out CathodeRayTubeVolume c))
            CRTvolume = c;

        if (volume.profile.TryGet(out PrecisionVolume p))
            precVolume = p;

        mainCam = Camera.main;
        colourAlpha = 1f;
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
            StartCoroutine(Goodbye());
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

    private IEnumerator Goodbye()
    {
        interactionCollider.enabled = false;
        pitchSource.Play();

        while (true)
        {
            if (sprite.color.a > 0)
            {
                colourAlpha -= 0.3f * Time.deltaTime;
                sprite.color = new Color(0, 0, 0, colourAlpha);
            }

            CRTvolume.noiseIntensity.value += noiseIntensityModifier * Time.deltaTime;
            pitchSource.volume += pitchVolumeModifier * Time.deltaTime;
            precVolume.geometry.value -= vertexWobbleModifier * Time.deltaTime;

            yield return null;
        }
    }

    private void ToggleCanvas(bool canvasState, string setPrompt)
    {
        prompt = setPrompt;
        canvas.enabled = canvasState;
        textMesh.enabled = canvasState;
    }
}
