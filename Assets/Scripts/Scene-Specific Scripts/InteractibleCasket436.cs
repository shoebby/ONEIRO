using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleCasket436 : MonoBehaviour, IInteractable
{
    [Header("Prompt")]
    [SerializeField] private string prompt;
    [SerializeField] private string enterPrompt;
    [SerializeField] private string exitPrompt;

    [Header("InteractionData")]
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioSource chantSource;
    [SerializeField] private Animator anim;
    [SerializeField] private Camera playerCam;
    [SerializeField] private Transform camPos;

    private bool isEntered;
    private GameObject camOriginalParent;
    private Vector3 camOriginalPos;

    private Rigidbody playerRb;
    private PlayerLook playerLook;
    private float sensX_startingValue;

    public string InteractionPrompt => prompt;

    private void Start()
    {
        prompt = enterPrompt;
        playerCam = Camera.main;
        camOriginalParent = playerCam.transform.parent.gameObject;
        camOriginalPos = playerCam.transform.localPosition;
        playerRb = camOriginalParent.GetComponent<Rigidbody>();
        playerLook = camOriginalParent.GetComponent<PlayerLook>();
        isEntered = false;
    }

    private void Update()
    {
        if (isEntered)
        {
            playerCam.transform.parent = camPos.transform;
            playerCam.transform.position = camPos.position;
        }
    }

    public bool Interact(Interactor interactor)
    {
        if (!isEntered)
            EnterCasket();
        else if (isEntered)
            ExitCasket();

        return true;
    }

    private void EnterCasket()
    {
        chantSource.Pause();
        source.Play();
        anim.Play("CasketClosing");
        prompt = exitPrompt;
        isEntered = true;
        playerRb.isKinematic = true;
        sensX_startingValue = playerLook.sensX;
        playerLook.sensX = 0f;
    }

    private void ExitCasket()
    {
        chantSource.Play();
        source.Stop();
        anim.Play("CasketOpening");
        prompt = enterPrompt;
        isEntered = false;
        playerCam.transform.parent = camOriginalParent.transform;
        playerCam.transform.localPosition = camOriginalPos;
        playerRb.isKinematic = false;
        playerLook.sensX = sensX_startingValue;
    }
}
