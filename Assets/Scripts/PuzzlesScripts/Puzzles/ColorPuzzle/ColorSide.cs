using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSide : MonoBehaviour
{

    [SerializeField] private Color _sideColor;

    public SpriteRenderer SpriteRenderer { get; set; }

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public Color SideColor
    {
        get
        {
            return _sideColor;
        }
        set
        {
            SpriteRenderer.color = value;
            _sideColor = value;
        }

    }

    public static bool operator ==(ColorSide shapeSide1, ColorSide shapeSide2)
    {
        if (shapeSide1.SideColor == shapeSide2.SideColor)
        {
           return true;
        }

        return false;
    }

    public static bool operator !=(ColorSide shapeSide1, ColorSide shapeSide2)
    {
        if (shapeSide1.SideColor == shapeSide2.SideColor)
        {
            return false;
        }

        return true;
    }


    public ColorSide(Color sideColor)
    {
        _sideColor = sideColor;
    }
}
