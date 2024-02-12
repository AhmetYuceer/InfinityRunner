using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static PlayerController Instance;

    [SerializeField] private CharacterPosition playerPosition;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        playerPosition = CharacterPosition.midle;
        ChancePosition(playerPosition);
    }

    void Update()
    {
        Move();
    }
    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            switch (playerPosition)
            {
                case CharacterPosition.midle:
                    playerPosition = CharacterPosition.left;
                    ChancePosition(playerPosition);
                    break;
                case CharacterPosition.right:
                    playerPosition = CharacterPosition.midle;
                    ChancePosition(playerPosition);
                    break;
                default:
                    break;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            switch (playerPosition)
            {
                case CharacterPosition.left:
                    playerPosition = CharacterPosition.midle;
                    ChancePosition(playerPosition);
                    break;
                case CharacterPosition.midle:
                    playerPosition = CharacterPosition.right;
                    ChancePosition(playerPosition);
                    break;  
                default:
                    break;
            }
        }
    }
    private void ChancePosition(CharacterPosition playerPosition)
    {
        var playerPos = this.transform.position;
        switch (playerPosition)
        {
            case CharacterPosition.left:
                playerPos.x = PlayerPositionConstants.leftPositionValue;
                this.transform.position = playerPos;
                break;
            case CharacterPosition.midle:
                playerPos.x = PlayerPositionConstants.middlePositionValue;
                this.transform.position = playerPos;
                break;
            case CharacterPosition.right:
                playerPos.x = PlayerPositionConstants.rightPositionValue;
                this.transform.position = playerPos;
                break;
            default:
                break;
        }
    }

}

public enum CharacterPosition
{
    left,
    midle,
    right
}

public class PlayerPositionConstants
{
    public const float leftPositionValue = 1.3f;
    public const float middlePositionValue = 3.8f;
    public const float rightPositionValue = 6.3f;
}
