using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoughingScript007 : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip[] coughingClips;
    [SerializeField] private int lastCough;
    [SerializeField] private float timeUntilCough;

    void Update()
    {
        if (timeUntilCough <= 0f)
        {
            int cough = Random.Range(0, coughingClips.Length);
            if (cough == lastCough)
                return;
            source.PlayOneShot(coughingClips[cough]);
            timeUntilCough = Random.Range(5, 10);
        } else if (timeUntilCough > 0f)
            timeUntilCough -= Time.deltaTime;
    }
}
