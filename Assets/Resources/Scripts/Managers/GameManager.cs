using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isPlay;
 
    [Header("Distance Meter")]
    private Transform playerTransform;
    private float distance;
    private int currentScore;
    private int coin;
    [SerializeField] private Transform meterStartingPosition;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        playerTransform = PlayerController.Instance.gameObject.transform;
        isPlay = false;
    }

    private void Update()
    {
        if (isPlay)
        {
            distance = Vector3.Distance(meterStartingPosition.position, playerTransform.position);
            currentScore = (int)Mathf.Round(distance) * 2;
            UIManager.Instance.UpdateScoreText(currentScore);
            PlayerController.Instance.UpSpeed();
        }
    }

    public void StartGame()
    {
        isPlay = true;
        SoundManager.Instance.PlayBackroundMusic();
        UIManager.Instance.HideControls();
    }

    public void EndGame()
    {
        isPlay = false;
        PlayerController.Instance.isCanPressKey = false;
        PlayerController.Instance.isMove = false;

        int maxScore = SaveAndLoad.GetScore();
        bool checkMaxScore = false;
        coin = UIManager.Instance.GetCurrentCoin();

        if (currentScore > maxScore)
        {
            checkMaxScore = true;
            SaveAndLoad.SetScore(currentScore);
            UIManager.Instance.SetMaxScore(currentScore);
            UIManager.Instance.SetCurrentScore(currentScore);
        }
        else
        {
            UIManager.Instance.SetMaxScore(maxScore);
            UIManager.Instance.SetCurrentScore(currentScore);
        }

        SaveAndLoad.SetCoin(coin);
        UIManager.Instance.ActivateEndPanel(checkMaxScore);

    }
}