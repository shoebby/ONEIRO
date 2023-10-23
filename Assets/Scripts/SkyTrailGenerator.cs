using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyTrailGenerator : MonoBehaviour
{
    [SerializeField] private GameObject trailRenderer;

    [SerializeField] private float instantiationPosX;
    [SerializeField] private float instantiationPosY;

    [SerializeField] private float newTrailTimerCurrent;
    [SerializeField] private float newTrailTimerMax;

    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            transform.position = new Vector3(instantiationPosX, instantiationPosY, Random.Range(-4000, 4000));
            Instantiate(trailRenderer, transform.position, trailRenderer.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        newTrailTimerCurrent -= Time.deltaTime;

        if (newTrailTimerCurrent <= 0f)
        {
            transform.position = new Vector3(instantiationPosX, instantiationPosY, Random.Range(-4000, 4000));
            Instantiate(trailRenderer, transform.position, trailRenderer.transform.rotation);
            newTrailTimerCurrent = newTrailTimerMax;
        }
    }
}
