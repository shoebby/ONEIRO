using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSource436 : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private bool isRising;
    [SerializeField] private float risingSpeed;
    [SerializeField] private float fallingSpeed;

    private void Start()
    {
        source.volume = 0f;
        isRising = true;
    }

    private void Update()
    {
        if (source.volume < 1f && isRising)
            source.volume += risingSpeed * Time.deltaTime;
        else if (source.volume > 0f && !isRising)
            source.volume -= fallingSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            isRising = false;
    }
}
