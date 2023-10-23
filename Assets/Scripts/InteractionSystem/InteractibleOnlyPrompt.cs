using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleOnlyPrompt : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    
    public string InteractionPrompt => prompt;

    public bool Interact(Interactor interactor)
    {
        Debug.Log("interacting with object!");
        
        return true;
    }
}
