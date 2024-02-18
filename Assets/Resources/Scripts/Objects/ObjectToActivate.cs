using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEditor.Progress;

public class ObjectToActivate : MonoBehaviour
{
    [SerializeField] private List<GameObject> objects = new List<GameObject>();
    private List<Vector3> objectsStartPositions = new List<Vector3>();

    [SerializeField] private float speed;

    [SerializeField] private bool isActive;

    private void Start()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            objectsStartPositions.Add(objects[i].transform.localPosition);
        }

    }

    void Update()
    {
        if (isActive)
        {
            int objectsReachedTargetCount = 0;

            for (int i = 0; i < objects.Count; i++)
            {
                var pos = objects[i].transform.localPosition;
                pos.z = Mathf.MoveTowards(pos.z, transform.localPosition.z, speed * Time.deltaTime);
                objects[i].transform.localPosition = pos;

                if (Mathf.Approximately(pos.z, transform.localPosition.z))
                {
                    objects[i].SetActive(false);
                    objectsReachedTargetCount++;
                    objects[i].transform.localPosition = objectsStartPositions[i];
                }
            }

            if (objectsReachedTargetCount == objects.Count)
            {
                isActive = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isActive = true;
        }        
    }

}