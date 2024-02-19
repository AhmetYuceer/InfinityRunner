using UnityEngine;

public class SaveAndLoad
{
    #region Score
    public static void SetScore(int score)
    {
        PlayerPrefs.SetInt("SCORE", score);
    }
    public static int GetScore()
    {
        return PlayerPrefs.GetInt("SCORE");
    }
    #endregion

    #region Coin
    public static void SetCoin(int currentCoin)
    {
        int maxCoin = GetCoin();
        maxCoin += currentCoin;
        PlayerPrefs.SetInt("COÝN", maxCoin);
    }
    public static int GetCoin()
    {
        return PlayerPrefs.GetInt("COÝN");
    }
    #endregion


    #region Character Customization

    public static void SetBodyIndex(int index)
    {
        PlayerPrefs.SetInt("HEAD", index);
        PlayerPrefs.SetInt("HAIR", index);
    }
    public static void SetColorTextureIndex(int index)
    {
        PlayerPrefs.SetInt("SKIN_COLOR", index);
    } 

    public static int GetBodyIndex()
    {
        return PlayerPrefs.GetInt("HEAD");
    }
    public static int GetColorTextureIndex()
    {
        return PlayerPrefs.GetInt("SKIN_COLOR");
    }
    #endregion

    #region Buy Colors

    
    public static void BuyRedColor()
    {
        PlayerPrefs.SetInt("RED", 1);
    }
    public static void BuyGrayColor()
    {
        PlayerPrefs.SetInt("GRAY", 1);
    }
    public static void BuyYellowColor()
    {
        PlayerPrefs.SetInt("YELLOW", 1);
    }   
     
   
    public static int GetRedColor()
    {
        return PlayerPrefs.GetInt("RED");
    }
    public static int GetGrayColor()
    {
        return PlayerPrefs.GetInt("GRAY");
    }
    public static int GetYellowColor()
    {
        return PlayerPrefs.GetInt("YELLOW");
    }
    #endregion

    #region Sound
    
    public static void SetMusicValue(float value)
    {
        PlayerPrefs.SetFloat("MUSÝC_VALUE", value);
    }
    public static float GetMusicValue()
    {
        return PlayerPrefs.GetFloat("MUSÝC_VALUE");
    }
 
    public static void SetSFXValue(float value)
    {
        PlayerPrefs.SetFloat("SFX_VALUE", value);
    }
    public static float GetSFXValue()
    {
       return PlayerPrefs.GetFloat("SFX_VALUE");
    }

    #endregion


    //public static void RESTARTScore()
    //{
    //    PlayerPrefs.SetInt("SCORE", 0);
    //}

    //public static void RESTARTCoin()
    //{
    //    PlayerPrefs.SetInt("COÝN", 0);
    //}

    //public static void RESTARTRedColor()
    //{
    //    PlayerPrefs.SetInt("RED", 0);
    //}
    //public static void RESTARTGrayColor()
    //{
    //    PlayerPrefs.SetInt("GRAY", 0);
    //}
    //public static void RESTARTYellowColor()
    //{
    //    PlayerPrefs.SetInt("YELLOW", 0);
    //}

}