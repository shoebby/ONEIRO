using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelGenerator421 : MonoBehaviour
{
    [SerializeField] private GameObject[] tunnelChunks;
    [SerializeField] private GameObject generatedChunk;
    [SerializeField] private GameObject lastChunk;
    [SerializeField] private Transform player;
    [SerializeField] private float instantiationStep;
    [SerializeField] private int startAmount;

    void Start()
    {
        GenerateChunks(startAmount);
    }

    void Update() { 
        if (player.position.z > transform.position.z - (instantiationStep * 3))
        {
            GenerateChunks(3);
        }
    }

    private void GenerateChunks(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            generatedChunk = tunnelChunks[Random.Range(0, tunnelChunks.Length)];

            while (generatedChunk == lastChunk)
            {
                Debug.Log("Generated a double, regenerating");
                generatedChunk = tunnelChunks[Random.Range(0, tunnelChunks.Length)];
            }
                
            Instantiate(generatedChunk, transform.position, transform.rotation);
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + instantiationStep);

            lastChunk = generatedChunk;
        }
    }
}
