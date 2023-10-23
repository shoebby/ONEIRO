using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleDisplayObject : MonoBehaviour, IInteractable
{
    [Header("Prompt Text")]
    [SerializeField] private string prompt;
    [SerializeField] private string displayPrompt;
    [SerializeField] private string returnPrompt;

    [Header("Display Variables")]
    [SerializeField] private GameObject displayedObject;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Camera mainCam;
    [SerializeField] private float maxDist;

    public string InteractionPrompt => prompt;

    private void Start()
    {
        Toggle(false, displayPrompt, false);

        mainCam = Camera.main;
    }

    private void Update()
    {
        displayedObject.transform.RotateAround(displayedObject.transform.position, transform.up, Time.deltaTime * 45f);

        if (canvas.enabled && Vector3.Distance(transform.position, mainCam.transform.position) >= maxDist)
            Toggle(false, displayPrompt, false);
    }

    public bool Interact(Interactor interactor)
    {
        Debug.Log("interacting with object!");

        if (!canvas.enabled)
            Toggle(true, returnPrompt, true);
        else
            Toggle(false, displayPrompt, false);

        return true;
    }

    private void Toggle(bool canvasState, string setPrompt, bool objectState)
    {
        prompt = setPrompt;
        displayedObject.SetActive(objectState);
        canvas.enabled = canvasState;
    }
}