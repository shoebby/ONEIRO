using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingingScript004 : MonoBehaviour
{
    [SerializeField] AudioSource source;

    // Update is called once per frame
    void Update()
    {
        source.pitch -= 0.01f * Time.deltaTime;
    }
}
