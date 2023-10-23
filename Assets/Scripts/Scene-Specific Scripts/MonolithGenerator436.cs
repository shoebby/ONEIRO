using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonolithGenerator436 : MonoBehaviour
{
    [SerializeField] private GameObject monolith;
    [SerializeField] private float monolithAmount;
    [SerializeField] private float minHeight;
    [SerializeField] private float maxHeight;

    void Start()
    {
        for (int i = 0; i < monolithAmount; i++)
        {
            transform.position = new Vector3(Random.Range(-2000, 2000), Random.Range(minHeight, maxHeight), Random.Range(-2000, 2000));
            transform.Rotate(new Vector3(0, Random.Range(0, 359), 0));
            Instantiate(monolith, transform.position, transform.rotation);
        }
    }
}
