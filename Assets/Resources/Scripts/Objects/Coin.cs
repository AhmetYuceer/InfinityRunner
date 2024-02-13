using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Renderer coinRenderer;

    private const float waitTime = 3f;
    private const float rotateSpeed = 90f;

    private void Start()
    {
        coinRenderer =  GetComponent<Renderer>();
    }

    private void Update()
    {
        this.transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (coinRenderer.enabled)
            {
                CollectibleObjectManager.Instance.CollectCoins();
                StartCoroutine(ToggleObjectVisibilityWithDelay());
            }
        }
    }

    // Objeyi tekrar tekrar oluþturmak yerine görünümünü kapatýp tekrar açýyoruz
    private IEnumerator ToggleObjectVisibilityWithDelay() 
    {
        this.coinRenderer.enabled = false;
        yield return new WaitForSeconds(waitTime);
        this.coinRenderer.enabled = true;
    }
}