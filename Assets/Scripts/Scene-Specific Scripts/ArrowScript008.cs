using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript008 : MonoBehaviour
{
    [SerializeField] private List<Transform> shoppingObjects;
    [SerializeField] private int chosenObject;
    [SerializeField] private Transform targetObject;
    [SerializeField] private float minDistance = 4f;

    void Start()
    {
        SetTargetObject();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, targetObject.position) <= minDistance)
            SetTargetObject();

        transform.LookAt(targetObject, Vector3.up);
        transform.Rotate(90, 0, 0);
    }

    void SetTargetObject()
    {
        chosenObject = Random.Range(0, shoppingObjects.Count);
        targetObject = shoppingObjects[chosenObject];
        shoppingObjects.Remove(targetObject);
    }
}