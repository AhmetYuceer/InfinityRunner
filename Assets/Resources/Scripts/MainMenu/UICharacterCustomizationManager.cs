using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterCustomizationManager : MonoBehaviour
{
    public static UICharacterCustomizationManager Instance;

    [Header("Customization Buttons")]
    [SerializeField] Button maleButton;
    [SerializeField] Button femaleButton;

    [Header("Color Buttons")]
    [SerializeField] Button blueButton;
    [SerializeField] Button redButton;
    [SerializeField] Button grayButton;
    [SerializeField] Button yellowButton;

    [Header("Colors Price Parent Object")]
    [SerializeField] private GameObject redPriceParent;
    [SerializeField] private GameObject grayPriceParent;
    [SerializeField] private GameObject yellowPriceParent;

    [Header("Customization Panel")]
    [SerializeField] private TextMeshProUGUI coinText;


    private List<GameObject> humanBodys; // 0: maleBody  1: femaleBody
    private List<GameObject> humanHairs; // 0: maleHair  1: femaleHair
    private List<Texture> colorTexture; // 0: blueTexture 1: redTexture 2: grayTexture 3: yellowTexture

    private int humanBodyIndex, colorTextureIndex;

    private bool redPurchasedStatus, grayPurchasedStatus, yellowPurchasedStatus;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        SaveAndLoad.RESTARTCoin();
        SaveAndLoad.RESTARTScore();
        SaveAndLoad.RESTARTRedColor();
        SaveAndLoad.RESTARTGrayColor();
        SaveAndLoad.RESTARTYellowColor();

        SetPurchasedStatus();

        humanBodys = PlayerCustomization.Instance.humanBodys;
        humanHairs = PlayerCustomization.Instance.humanHairs;
        colorTexture = PlayerCustomization.Instance.colorTexture;

        #region Gender Buttons
        maleButton.onClick.AddListener(() =>
        {
            humanBodys[0].SetActive(true);
            humanHairs[0].SetActive(true);
            humanBodys[1].SetActive(false);
            humanHairs[1].SetActive(false);

            humanBodyIndex = 0;
        });      
        femaleButton.onClick.AddListener(() =>
        {
            humanBodys[1].SetActive(true);
            humanHairs[1].SetActive(true);
            humanBodys[0].SetActive(false);
            humanHairs[0].SetActive(false);
            
            humanBodyIndex = 1;
        });
        #endregion

        #region Color Buttons
        blueButton.onClick.AddListener(() =>
        {
            PlayerCustomization.Instance.characterMaterial.mainTexture = colorTexture[0];
            colorTextureIndex = 0;
        });        
        redButton.onClick.AddListener(() =>
        {
            if (!redPurchasedStatus)
            {
                int coins = SaveAndLoad.GetCoin();
                if (coins < 500)
                {
                    return;
                }
                SaveAndLoad.SetCoin(-500);

                SaveAndLoad.BuyRedColor();
                redPriceParent.SetActive(false);
                SetPurchasedStatus();
            }
            PlayerCustomization.Instance.characterMaterial.mainTexture = colorTexture[1];
            colorTextureIndex = 1;
        });        
        grayButton.onClick.AddListener(() =>
        {
            if (!grayPurchasedStatus)
            {
                int coins = SaveAndLoad.GetCoin();
                if (coins < 500)
                {
                    return;
                }
                SaveAndLoad.SetCoin(-500);

                SaveAndLoad.BuyGrayColor();
                grayPriceParent.SetActive(false);
                SetPurchasedStatus();
            }

            PlayerCustomization.Instance.characterMaterial.mainTexture = colorTexture[2];
            colorTextureIndex = 2;
        });        
        yellowButton.onClick.AddListener(() =>
        {
            if (!yellowPurchasedStatus)
            {
                int coins = SaveAndLoad.GetCoin();
                if (coins < 500)
                {
                    return;
                }
                SaveAndLoad.SetCoin(-500);

                SaveAndLoad.BuyYellowColor();
                yellowPriceParent.SetActive(false);
                SetPurchasedStatus();
            }

            PlayerCustomization.Instance.characterMaterial.mainTexture = colorTexture[3];
            colorTextureIndex = 3;
        });
        #endregion
    }
 
    private void SetPurchasedStatus()
    {
        redPurchasedStatus = (SaveAndLoad.GetRedColor() == 1) ? true : false;
        grayPurchasedStatus = (SaveAndLoad.GetGrayColor() == 1) ? true : false;
        yellowPurchasedStatus = (SaveAndLoad.GetYellowColor() == 1) ? true : false;

        if (redPurchasedStatus)
        {
            redPriceParent.SetActive(false);
        }
        if (grayPurchasedStatus)
        {
            grayPriceParent.SetActive(false);
        }
        if (yellowPurchasedStatus)
        {
            yellowPriceParent.SetActive(false);
        }
        SetCointext();
    }

    public void SetCointext()
    {
        coinText.text = SaveAndLoad.GetCoin().ToString();
    }

    public void SaveIndexs()
    {
        SaveAndLoad.SetBodyIndex(humanBodyIndex);
        SaveAndLoad.SetColorTextureIndex(colorTextureIndex);
    }
}