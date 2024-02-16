using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomization : MonoBehaviour
{
    public static CharacterCustomization Instance;
    public Material characterMaterial;

    [SerializeField] private CustomizationScriptableObject characterSO;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }


}