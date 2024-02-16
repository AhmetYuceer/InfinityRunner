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
}
