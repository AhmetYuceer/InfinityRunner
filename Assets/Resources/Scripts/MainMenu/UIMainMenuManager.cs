using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIMainMenuManager : MonoBehaviour
{
    public static UIMainMenuManager Instance;

    [SerializeField] private Animator animator;
    [SerializeField] private Transform character;
    [SerializeField] private Transform cam;

    [Header("Panels")]
    [SerializeField] private RectTransform mainPanelRect;
    [SerializeField] private RectTransform customizationPanelRect;
    [SerializeField] private RectTransform settingsPanelRect;
    [SerializeField] private RectTransform exitPanelRect;

    [Header("Menu Buttons")]
    [SerializeField] private Button startButton; 
    [SerializeField] private Button settingsButton; 
    [SerializeField] private Button customizationButton; 
    [SerializeField] private Button exitButton;
    [SerializeField] private Button quitButton;
    
    [Header("Back Buttons")]
    [SerializeField] private Button backCustomizationButton;
    [SerializeField] private Button backExitButton;
    [SerializeField] private Button backSettingsButton;

    [Header("CameraPositons")]
    [SerializeField] private Transform camMainMenuPos;
    [SerializeField] private Transform camCustomizationPos;
    [SerializeField] private Transform camSettingsPos;
    [SerializeField] private Transform camExitPos;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        mainPanelRect.gameObject.SetActive(true);
        exitPanelRect.gameObject.SetActive(false);
        customizationPanelRect.gameObject.SetActive(false);
        settingsPanelRect.gameObject.SetActive(false);

        animator = character.GetComponent<Animator>();

        #region Buttons Onclick

        startButton.onClick.AddListener(() =>
        {
            ClickStartButton();
        });
        settingsButton.onClick.AddListener(() =>
        {
            ClickSettingsButton();

        });
        customizationButton.onClick.AddListener(() =>
        {
            ClickCustomizationButton();
        });
        exitButton.onClick.AddListener(() =>
        {
            ClickExitButton();
        });
        backCustomizationButton.onClick.AddListener(() =>
        {
            UICharacterCustomizationManager.Instance.SaveIndexs();
            BackButton(customizationPanelRect);
        });
        backExitButton.onClick.AddListener(() =>
        {
            BackButton(exitPanelRect);
        });
        backSettingsButton.onClick.AddListener(() =>
        {
            UISoundManager.Instance.SaveMusicSliderValue();
            BackButton(settingsPanelRect);
        });
        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
        #endregion
    }

    private void BackButton(RectTransform panelRectTranform)
    {
        panelRectTranform.gameObject.SetActive(false);
        cam.transform.DORotateQuaternion(camMainMenuPos.rotation, 1f);
        cam.transform.DOMove(camMainMenuPos.position, 1f, false)
            .OnComplete(() =>
            {
                mainPanelRect.gameObject.SetActive(true);
            });
    } 
     
    private void ClickStartButton()
    {
        mainPanelRect.DOAnchorPos(new Vector2(0, -1000), 1f, false);
        cam.DORotateQuaternion(Quaternion.Euler(0,0,0), 1f);
        animator.SetBool("Run",true);
        
        character.DOMoveZ(10, 3f, false);
        cam.transform.DOMoveZ(5, 3f, false)
            .OnComplete(() =>
            {
                 SceneManager.LoadSceneAsync("GameScene");
 
                //SceneManager.LoadScene(1);
            });
    }

    private void ClickSettingsButton()
    {
        mainPanelRect.gameObject.SetActive(false);
        cam.transform.DOMove(camSettingsPos.position, 1f, false)
            .OnComplete(() =>
            {
                settingsPanelRect.gameObject.SetActive(true);
            });
    }   
    private void ClickCustomizationButton()
    {
        mainPanelRect.gameObject.SetActive(false);
        cam.transform.DORotateQuaternion(camCustomizationPos.rotation, 1f);
        cam.transform.DOMove(camCustomizationPos.position, 1f, false)
            .OnComplete(() =>
            {
                customizationPanelRect.gameObject.SetActive(true);
            });
    }    
    private void ClickExitButton()
    {
        mainPanelRect.gameObject.SetActive(false);
        cam.DOMove(camExitPos.position, 1f,false)
            .OnComplete(() =>
            {
                exitPanelRect.gameObject.SetActive(true);
            });
    }
}