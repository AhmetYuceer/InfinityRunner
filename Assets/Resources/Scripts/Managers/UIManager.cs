using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private List<GameObject> hearts = new List<GameObject>();

    [Header("UI Coin And Score")]
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI scoreText;
   
    [Header("UI Effects")]
    [SerializeField] private Image blinkEffectImage;
    [SerializeField] private RectTransform pressKeyText;
    [SerializeField] private RectTransform highScoreObjectRectTranform;
    private const float blinkDuration = 2f;
    private const float blinkSpeed = 0.2f;

    [Header("UI End Game Panel")]
    [SerializeField] private RectTransform endPanelRectTransform;
    [SerializeField] private Button homeButton;
    [SerializeField] private Button restartButton;

    [SerializeField] private TextMeshProUGUI maxScoreText;
    [SerializeField] private TextMeshProUGUI currentScoreText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }
    Tween tween;
    private void Start()
    {
        pressKeyText.gameObject.SetActive(true);
        highScoreObjectRectTranform.gameObject.SetActive(false);
        endPanelRectTransform = endPanelRectTransform.GetComponent<RectTransform>();
        endPanelRectTransform.gameObject.SetActive(false);
        AddButtonListeners();

       tween = pressKeyText.DOScale(new Vector3(0.9f, 0.9f, 0.9f), 1f)
                        .SetEase(Ease.Linear)
                        .SetLoops(-1, LoopType.Yoyo);
    }
 
    public void DeactivatePressKeyText()
    {
        tween.Pause();
        pressKeyText.gameObject.SetActive(false);
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

    #region End Game Panel

    public void SetMaxScore(int maxScore)
    {
        maxScoreText.text = maxScore.ToString();
    }
    public void SetCurrentScore(int currentScore)
    {
        currentScoreText.text = currentScore.ToString();
    }

    private void AddButtonListeners()
    {
        homeButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });

        restartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(1);
        });
    }

    public void ActivateEndPanel(bool checkMaxScore)
    {
        endPanelRectTransform.gameObject.SetActive(true);

        endPanelRectTransform.DOAnchorPosY(endPanelRectTransform.anchoredPosition.y + 415f, 1f)
        .SetEase(Ease.OutBounce);
        currentScoreText.color = Color.black;
        if (checkMaxScore)
        {
            highScoreObjectRectTranform.gameObject.SetActive(true);
            currentScoreText.color = Color.red;

            highScoreObjectRectTranform.DOAnchorPos(new Vector2(112, -116), 1.2f)
                .SetEase(Ease.OutQuad)
                  .OnComplete(() =>
                  {
                      highScoreObjectRectTranform.DORotate(new Vector3(0f, 0f, 328f), 1f, RotateMode.FastBeyond360)
                        .SetEase(Ease.OutBounce)
                        .SetLoops(1, LoopType.Restart)
                           .OnComplete(() => 
                           {
                               highScoreObjectRectTranform.rotation = Quaternion.Euler(0,0,-32);
                           });
                  });
        }
    }
    #endregion

    #region UIEffects
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
    #endregion

    #region ScoreAndCoin
    public void UpdateCoinText(int coinCount)
    {
        coinText.text = coinCount.ToString();
    }
    public void UpdateScoreText(float distance)
    {
        scoreText.text = distance.ToString();
    }
    #endregion
}