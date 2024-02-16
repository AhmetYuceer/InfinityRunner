using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Distance Meter")]
    private Transform playerTransform;
    private float distance;
    private int currentScore;
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
    }

    private void Update()
    {
        if (PlayerController.Instance.isMove)
        {
            distance = Vector3.Distance(meterStartingPosition.position, playerTransform.position);
            currentScore = (int)Mathf.Round(distance) * 2;
            UIManager.Instance.UpdateScoreText(currentScore);
            PlayerController.Instance.UpSpeed();
        }
    }

    public void EndGame()
    {
        PlayerController.Instance.isMove = false;
        int maxScore = SaveAndLoad.GetMaxScore();
        bool checkMaxScore = false;

        if (currentScore > maxScore)
        {
            checkMaxScore = true;
            SaveAndLoad.SetMaxScore(currentScore);
            UIManager.Instance.SetMaxScore(currentScore);
            UIManager.Instance.SetCurrentScore(currentScore);
        }
        else
        {
            UIManager.Instance.SetMaxScore(maxScore);
            UIManager.Instance.SetCurrentScore(currentScore);
        }

        UIManager.Instance.ActivateEndPanel(checkMaxScore);
    }
}