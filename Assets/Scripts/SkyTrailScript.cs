using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyTrailScript : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;

    void Update()
    {
        transform.position += Vector3.left * Time.deltaTime * speed;

        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0f)
            Destroy(gameObject);
    }
}
