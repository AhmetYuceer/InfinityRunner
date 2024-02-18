using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCustomization : MonoBehaviour
{
    public static PlayerCustomization Instance;

    public List<GameObject> humanBodys = new List<GameObject>(); // 0: maleBody  1: femaleBody
    public List<GameObject> humanHairs = new List<GameObject>(); // 0: maleHair  1: femaleHair
    public List<Texture> colorTexture = new List<Texture>();  // 0: blueTexture 1: redTexture 2: grayTexture 3: yellowTexture
    public Material characterMaterial;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        SetCharacter();
    }

    public void SetCharacter()
    {
        int humanBodyIndex = SaveAndLoad.GetBodyIndex();
        int colorTextureIndex = SaveAndLoad.GetColorTextureIndex();

        foreach (var body in humanBodys)
        {
            body.SetActive(false);
        }
        foreach (var hair in humanHairs)
        {
            hair.SetActive(false);
        }

        humanBodys[humanBodyIndex].SetActive(true);
        humanHairs[humanBodyIndex].SetActive(true);

        characterMaterial.mainTexture = colorTexture[colorTextureIndex];

    }
}