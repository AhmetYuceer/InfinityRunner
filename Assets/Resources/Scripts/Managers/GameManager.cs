using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float distance;

    void Update()
    {
        distance = UIManager.Instance.GetDistance();
        if (distance % 100  == 0  && distance > 0)
        {
            PlayerController.Instance.IncreaseSpeed();
        }        
    }
}
