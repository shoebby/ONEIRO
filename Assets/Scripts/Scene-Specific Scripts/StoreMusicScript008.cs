using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using HauntedPSX.RenderPipelines.PSX.Runtime;

public class StoreMusicScript008 : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip[] musicClips;
    [SerializeField] private AudioSource blackoutSource;
    [SerializeField] private AudioClip powerDownClip;
    [SerializeField] private AudioClip powerUpClip;
    [SerializeField] private Light mainLight;

    [Header("Haunted PSX Variables")]
    [SerializeField] private Volume volume;
    [SerializeField] private FogVolume fog;
    [SerializeField] private Color standardColour;
    [SerializeField] private Color blackoutColour;
    [SerializeField] private float standardFogFalloff = 0.2f;
    [SerializeField] private float blackoutFogFalloff = 0.7f;

    [Header("Other Variables")]
    [SerializeField] private bool blackedOut;
    [SerializeField] private int previousClip;
    [SerializeField] private float timeUntilBlackout;
    [SerializeField] private float standardDuration = 60f;
    [SerializeField] private float blackoutDuration = 10f;
    [SerializeField] private float blackoutPitch = 0.25f;
    [SerializeField] private float standardPitch = 0.85f;

    private void Start()
    {
        if (volume.profile.TryGet(out FogVolume fogVolume))
            fog = fogVolume;

        blackedOut = false;
        timeUntilBlackout = standardDuration;
    }

    void Update()
    {
        if (source.isPlaying == false)
        {
            int chosenClip = Random.Range(0, musicClips.Length);
            if (chosenClip == previousClip)
                return;
            source.PlayOneShot(musicClips[chosenClip]);
            previousClip = chosenClip;
        }

        if (timeUntilBlackout <= 0f && !blackedOut)
        {
            StartCoroutine(Blackout(-0.2f, false, blackoutColour, blackoutFogFalloff, powerDownClip, true, blackoutDuration));            
        } else if(timeUntilBlackout <= 0f && blackedOut)
        {
            StopAllCoroutines();
            StartCoroutine(Blackout(0.2f, true, standardColour, standardFogFalloff, powerUpClip, false, standardDuration));
        } else
            timeUntilBlackout -= Time.deltaTime;
    }

    private IEnumerator Blackout(float pitchModifier, bool lightsState, Color fogColour, float fogFalloffValue, AudioClip powerClip, bool blackedOutState, float timeUntilSwitch)
    {
        mainLight.enabled = lightsState;
        fog.color.value = fogColour;
        fog.fogFalloffCurve.value = fogFalloffValue;
        blackoutSource.PlayOneShot(powerClip);
        blackedOut = blackedOutState;
        timeUntilBlackout = timeUntilSwitch;

        while (true)
        {
            if (blackedOut && source.pitch >= blackoutPitch)
                source.pitch += pitchModifier * Time.deltaTime;
            else if (!blackedOut && source.pitch <= standardPitch)
                source.pitch += pitchModifier * Time.deltaTime;

            yield return null;
        }
    }
}
