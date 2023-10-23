using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleLever005 : MonoBehaviour, IInteractable
{
    [Header("Prompt")]
    [SerializeField] private string prompt;

    [Header("InteractionData")]
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip leverPullClip;
    [SerializeField] private bool isDown;
    [SerializeField] private IEnumerator coroutine;
    [SerializeField] private GameObject[] lights;

    public string InteractionPrompt => prompt;

    private void Start()
    {
        isDown = true;
        coroutine = LightsOn();
        StartCoroutine(coroutine);
    }

    public bool Interact(Interactor interactor)
    {
        if (!isDown)
        {
            isDown = true;
            coroutine = LightsOn();
            StartCoroutine(coroutine);

            anim.Play("LeverDown");
        }
        else if (isDown)
        {
            isDown = false;
            StopCoroutine(coroutine);

            for (int i = 0; i < lights.Length; i++)
                lights[i].SetActive(false);

            anim.Play("LeverUp");
        }
        source.PlayOneShot(leverPullClip);
        return true;
    }

    IEnumerator LightsOn()
    {
        float timeUntilFlickering = Random.Range(.1f,2f);

        for (int i = 0; i< lights.Length; i++)
            lights[i].SetActive(true);

        while (true)
        {
            timeUntilFlickering -= Time.deltaTime;

            if (timeUntilFlickering <= 0f)
            {
                int randomLight = Random.Range(0, lights.Length);
                lights[randomLight].SetActive(false);

                yield return new WaitForSeconds(0.1f);

                lights[randomLight].SetActive(true);
                timeUntilFlickering = Random.Range(.1f, 2f);
            }
        }
    }
}
