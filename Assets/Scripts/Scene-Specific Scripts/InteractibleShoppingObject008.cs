using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractibleShoppingObject008 : MonoBehaviour, IInteractable
{
    [Header("Prompt Text")]
    [SerializeField] private string prompt;

    [Header("Components")]
    [SerializeField] private GameObject shelfObject;
    [SerializeField] private GameObject cartObject;
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private AudioSource source;
    [SerializeField] private BoxCollider interactCollider;

    [Header("Variables")]
    [SerializeField] private string objectName;
    [SerializeField] private AudioClip pickupClip;

    public string InteractionPrompt => prompt;

    void Start()
    {
        shelfObject.SetActive(true);
        cartObject.SetActive(false);
        textMesh.enabled = false;
    }

    public bool Interact(Interactor interactor)
    {
        shelfObject.SetActive(false);
        cartObject.SetActive(true);

        StartCoroutine(GotObjectSequence());

        interactCollider.enabled = false;

        return true;
    }

    private IEnumerator GotObjectSequence()
    {
        textMesh.text = "GOT " + objectName + "!";
        textMesh.enabled = true;
        source.PlayOneShot(pickupClip);

        yield return new WaitForSeconds(3f);

        textMesh.enabled = false;

        yield return null;
    }
}
