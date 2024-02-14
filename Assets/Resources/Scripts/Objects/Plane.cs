using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    [SerializeField] private Transform planeStartingPosition;
    [SerializeField] private Transform planeEndPosition;
    [SerializeField] private float lerpTime; 

    private bool isActive;
 
    private void Update()
    {
        if (isActive)
        {
            transform.position = Vector3.Lerp(transform.position, planeEndPosition.position, lerpTime * Time.deltaTime);
        }
    }

    public IEnumerator ActivateThePlane()
    {
        isActive = true;
        yield return new WaitForSeconds(6f);
        isActive = false;
        this.transform.position = planeStartingPosition.position;
    }
}