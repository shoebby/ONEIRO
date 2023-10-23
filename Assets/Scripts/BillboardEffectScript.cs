using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardEffectScript : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private Vector3 rotValues;

    void Update()
    {
        transform.LookAt(target.transform.position);
        transform.Rotate(rotValues);
    }
}
