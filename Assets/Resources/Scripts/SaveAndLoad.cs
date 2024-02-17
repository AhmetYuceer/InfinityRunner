using UnityEngine;

public class SaveAndLoad
{
    
    public static void SetMaxScore(int score)
    {
        PlayerPrefs.SetInt("MaxScore", score);
    }
    public static int GetMaxScore()
    {
        return PlayerPrefs.GetInt("MaxScore");
    }

    public static void AddCoin(int currentCoin)
    {
        int maxCoins = GetCoins();
        maxCoins += currentCoin;
        PlayerPrefs.SetInt("Coin", maxCoins);
    }
    public static int GetCoins()
    {
        return PlayerPrefs.GetInt("Coin");
    }

    
}
