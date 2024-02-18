using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    public static PlayerHealthSystem Instance;

    private const int heartCount = 3;
    private const int damage = 1;
    private const float duration = 2f;

    [SerializeField] private float blinkSpeed = 0.2f;
    private int currentHeartCount;
    private Material characterMaterial;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        currentHeartCount = heartCount;
        characterMaterial = PlayerCustomization.Instance.characterMaterial;
        UIManager.Instance.SetHearts(currentHeartCount);
        characterMaterial.SetFloat("_Metallic", 0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Obstacle"))
        {
            collision.collider.gameObject.SetActive(false);
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        currentHeartCount -= damage;
        UIManager.Instance.SetHearts(currentHeartCount);
        StartCoroutine(AnimateBlink());

        if (currentHeartCount < 1)
        {
            GameManager.Instance.EndGame();
        }
    }

    IEnumerator AnimateBlink()
    {
        float characterSpeed = PlayerController.Instance.GetSpeed();
        PlayerController.Instance.SetSpeed(0);

        PlayerController.Instance.SetIsKinematic(true);

        float startTime = Time.time;  
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime = Time.time - startTime;

            characterMaterial.SetFloat("_Metallic", 0f);
            yield return new WaitForSeconds(blinkSpeed);

            characterMaterial.SetFloat("_Metallic", 1f);
            yield return new WaitForSeconds(blinkSpeed);
        }

        characterMaterial.SetFloat("_Metallic", 0f);
        PlayerController.Instance.SetSpeed(characterSpeed);
        PlayerController.Instance.SetIsKinematic(false);
    }
}