using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateThePlane : MonoBehaviour
{
    [SerializeField] private Plane plane;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(plane.ActivateThePlane());
        }
    }
}
