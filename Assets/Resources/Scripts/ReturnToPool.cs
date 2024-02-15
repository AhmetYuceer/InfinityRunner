using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToPool : MonoBehaviour
{
    [SerializeField] List<GameObject> childs = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (var item in childs)
            {
                item.SetActive(true);
            }
            ObjectPoolManager.Instance.ReturnToPool(this.gameObject);
        }
    }

}