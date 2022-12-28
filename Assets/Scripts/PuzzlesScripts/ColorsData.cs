using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorsData",menuName = "Assets/Data/ColorsData")]
public class ColorsData : ScriptableObject
{
    [field: SerializeField] public Color DefaultColor;
    [field: SerializeField] public Color EdgeColor;
    [field: SerializeField] public Color[] SideColors;

    public Color RandomColor()
    {
       return SideColors[Random.Range(0, SideColors.Length)];
    }
}
