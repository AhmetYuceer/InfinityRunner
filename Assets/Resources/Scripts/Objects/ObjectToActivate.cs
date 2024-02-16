using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEditor.Progress;

public class ObjectToActivate : MonoBehaviour
{
    [SerializeField] private List<GameObject> objects = new List<GameObject>();
    [SerializeField] private List<Vector3> objectsStartPositions = new List<Vector3>();

    [SerializeField] private float speed;

    [SerializeField] private bool isActive;

    int activateObjectCount = 0;

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
            if (activateObjectCount <= objects.Count)
            {
                for (int i = 0; i < objects.Count; i++)
                {
                    var pos = objects[i].transform.localPosition;

                    pos.z = Mathf.MoveTowards(pos.z, transform.localPosition.z, speed * Time.deltaTime);

                    objects[i].transform.localPosition = pos;

                    if (Mathf.Abs(pos.z - transform.localPosition.z) < 0.1f)
                    {
                        activateObjectCount++;
                        objects[i].transform.localPosition = objectsStartPositions[i];
                        objects[i].SetActive(false);
                    }
                }
            }
            else
            {
                isActive = false;
                activateObjectCount = 0;
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