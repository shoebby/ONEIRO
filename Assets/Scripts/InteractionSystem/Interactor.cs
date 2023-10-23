using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask interactibleMask = 1 << 8;
    [SerializeField] private KeyCode interactKey = KeyCode.Mouse0;
    [SerializeField] private InteractionPromptUI interactionPromptUI;

    private readonly Collider[] colliders = new Collider[3]; //can be raised if there are more interactibles in a single scene
    [SerializeField] private int numFound;

    private IInteractable interactible;

    private void Update()
    {
        numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionPointRadius, colliders, interactibleMask);

        if (numFound > 0)
        {
            interactible = colliders[0].GetComponent<IInteractable>();

            if (interactible != null)
            {
                interactionPromptUI.UpdatePrompt(interactible.InteractionPrompt);

                if (!interactionPromptUI.isDisplayed)
                    interactionPromptUI.SetUp(interactible.InteractionPrompt);

                if (Input.GetKeyDown(interactKey))
                    interactible.Interact(this);
            }
        }
        else
        {
            if (interactible != null)
                interactible = null;

            if (interactionPromptUI.isDisplayed)
                interactionPromptUI.Close();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionPointRadius);
    }
}
