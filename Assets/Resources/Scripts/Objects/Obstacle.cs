using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private const float waitTime = 3f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Player"))
        {
            StartCoroutine(TakeDamage());
        }
    }

    private IEnumerator TakeDamage()
    {
        PlayerHealthSystem.Instance.TakeDamage();
        this.gameObject.SetActive(false);
        yield return new WaitForSeconds(waitTime);
        this.gameObject.SetActive(true);
    }
}