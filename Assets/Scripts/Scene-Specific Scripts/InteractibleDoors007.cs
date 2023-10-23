using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleDoors007 : MonoBehaviour, IInteractable
{
    [Header("Prompt")]
    [SerializeField] private string prompt;
    [SerializeField] private string openPrompt;
    [SerializeField] private string openedPrompt;

    [Header("InteractionData")]
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip doorClip;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private GameObject coughingObject;

    private BoxCollider triggerCollider;

    public string InteractionPrompt => prompt;

    private void Start()
    {
        triggerCollider = GetComponent<BoxCollider>();
        prompt = openPrompt;
        particles.Stop();
        coughingObject.SetActive(false);
    }

    public bool Interact(Interactor interactor)
    {
        anim.Play("DoorsOpen");
        source.PlayOneShot(doorClip);
        particles.Play();
        coughingObject.SetActive(true);
        triggerCollider.enabled = false;
        return true;
    }
}
