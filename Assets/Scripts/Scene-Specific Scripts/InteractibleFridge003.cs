using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleFridge003 : MonoBehaviour, IInteractable
{
    [Header("Prompt")]
    [SerializeField] private string prompt;
    [SerializeField] private string openFridgePrompt;
    [SerializeField] private string closeFridgePrompt;

    [Header("InteractionData")]
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip fridgeOpenClip;
    [SerializeField] private AudioClip fridgeCloseClip;
    [SerializeField] private bool isOpen;
    [SerializeField] private GameObject fridgeLight;

    public string InteractionPrompt => prompt;

    void Start()
    {
        fridgeLight.SetActive(false);
        isOpen = false;
        prompt = openFridgePrompt;
    }

    public bool Interact(Interactor interactor)
    {
        if (!isOpen)
            ToggleFridge(true, "OpenFridge", fridgeOpenClip, closeFridgePrompt,true);
        else if (isOpen)
            ToggleFridge(false, "CloseFridge", fridgeCloseClip, openFridgePrompt,false);
        
        return true;
    }

    private void ToggleFridge(bool newState, string animName, AudioClip clip, string setPrompt, bool isLightOn)
    {
        isOpen = newState;
        anim.Play(animName);
        source.PlayOneShot(clip);
        prompt = setPrompt;
        fridgeLight.SetActive(isLightOn);
    }
}
