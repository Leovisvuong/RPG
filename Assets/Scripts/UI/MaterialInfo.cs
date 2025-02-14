using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Material")]
public class MaterialInfo : ScriptableObject
{
    public string materialDescription;
    public string materialName;
    public Sprite materialSprite;
}
