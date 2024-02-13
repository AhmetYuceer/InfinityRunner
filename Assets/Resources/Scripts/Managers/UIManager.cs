using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private List<GameObject> hearts = new List<GameObject>();

    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Image blinkEffectImage;

    private float distance;
    private const float blinkDuration = 2f;
    private const float blinkSpeed = 0.2f;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }
 
    public void SetHearts(int currentHeartCount)
    {
        foreach (var heart in hearts)
        {
            heart.SetActive(false);
        }
        for (int i = 0; i < currentHeartCount; i++)
        {
            hearts[i].SetActive(true);
        }
        if (currentHeartCount < 3 && currentHeartCount > 0)
        {
            StartCoroutine(UIBlinkEffect());
        }
    }

    private IEnumerator UIBlinkEffect()
    {
        float startTime = Time.time;
        float elapsedTime = 0f;
        Color currentColor = blinkEffectImage.color;

        while (elapsedTime < blinkDuration)
        {
            elapsedTime = Time.time - startTime;

            currentColor.a = 0f;
            blinkEffectImage.color = currentColor; 
            yield return new WaitForSeconds(blinkSpeed);

            currentColor.a = 1f;
            blinkEffectImage.color = currentColor;
            yield return new WaitForSeconds(blinkSpeed);
        }
        currentColor.a = 0f;
        blinkEffectImage.color = currentColor;
    }


    public float GetDistance()
    {
        return distance;
    }

    public void UpdateCoinText(int coinCount)
    {
        coinText.text = coinCount.ToString();
    }
    public void UpdateScoreText(float distance)
    {
        this.distance = distance;
        scoreText.text = distance.ToString();
    }
}