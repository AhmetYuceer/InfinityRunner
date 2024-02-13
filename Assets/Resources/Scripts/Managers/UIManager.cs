using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    public void UpdateCoinText(int coinCount)
    {
        coinText.text = coinCount.ToString();
    }
    public void UpdateScoreText(float distance)
    {
        scoreText.text = distance.ToString();
    }
}