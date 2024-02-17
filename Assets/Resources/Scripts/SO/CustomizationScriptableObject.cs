using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Charater Customization / Customization", fileName = "CharaterSO")]
public class CustomizationScriptableObject : ScriptableObject
{
    public bool male;
    public bool female;   
    public Texture texture;
}