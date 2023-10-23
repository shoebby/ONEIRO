using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPlayerCollider : MonoBehaviour
{
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private Vector3 gizmoScale;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireMesh(meshFilter.sharedMesh, 0, transform.position, transform.rotation, gizmoScale);
    }
}
