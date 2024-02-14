using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceMeter : MonoBehaviour
{
    private Transform playerTransform;
    private float distance ;
    private float currentDistance;
    [SerializeField] private Transform meterStartingPosition;

    private void Start()
    {
        playerTransform = PlayerController.Instance.gameObject.transform;
    }

    private void Update()
    {
        distance = Vector3.Distance(meterStartingPosition.position, playerTransform.position);
        float roundedNumber = Mathf.Round(distance);
        UIManager.Instance.UpdateScoreText(roundedNumber);
    }
}