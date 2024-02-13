using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleObjectManager : MonoBehaviour
{
    public static CollectibleObjectManager Instance;
    
    private int currentCoin = 0;
    private const int coinValue = 5;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        UIManager.Instance.UpdateCoinText(currentCoin);
    }

    public void CollectCoins()
    {
        currentCoin += coinValue;
        UIManager.Instance.UpdateCoinText(currentCoin);
    }
}