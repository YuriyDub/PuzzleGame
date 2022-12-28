using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeSide : MonoBehaviour
{
    [SerializeField] private Sprite _flatSideSprite;
    [SerializeField] private Sprite _convexSideSprite;
    [SerializeField] private Sprite _concaveSideSprite;

    [SerializeField] private Shape _sideShape;

    public SpriteRenderer SpriteRenderer { get; set; }

    public Shape SideShape 
    {
        get 
        {
            return _sideShape; 
        }
        set 
        {
            _sideShape = value;

            switch (value)
            {
                case Shape.Convex:
                    SpriteRenderer.sprite = _convexSideSprite;
                    break;
                case Shape.Flat:
                    SpriteRenderer.sprite = _flatSideSprite;                    
                    break;
                case Shape.Concave:
                    SpriteRenderer.sprite = _concaveSideSprite;
                    break;
            }

        }

    }

    public static bool operator == (ShapeSide shapeSide1, ShapeSide shapeSide2)
    {
        switch (shapeSide1.SideShape, shapeSide2.SideShape)
        {
            case (Shape.Concave, Shape.Convex):
                return true;

            case (Shape.Convex, Shape.Concave):
                return true;

            case (Shape.Flat, Shape.Edge):
                return true;

            case (Shape.Edge, Shape.Flat):
                return true;

            default:
                return false;

        }
    }

    public static bool operator !=(ShapeSide shapeSide1, ShapeSide shapeSide2)
    {
        switch (shapeSide1.SideShape, shapeSide2.SideShape)
        {
            case (Shape.Concave, Shape.Concave):
                return true;

            case (Shape.Convex, Shape.Convex):
                return true;

            case (Shape.Convex, Shape.Edge):
                return true;

            case (Shape.Concave, Shape.Edge):
                return true;

            case (Shape.Edge, Shape.Convex):
                return true;

            case (Shape.Edge, Shape.Concave):
                return true;

            case (Shape.Convex, Shape.Flat):
                return true;

            case (Shape.Concave, Shape.Flat):
                return true;

            case (Shape.Flat, Shape.Convex):
                return true;

            case (Shape.Flat, Shape.Concave):
                return true;

            case (Shape.Flat, Shape.Flat):
                return true;

            case (Shape.Default, Shape.Flat):
                return true;

            case (Shape.Flat, Shape.Default):
                return true;

            default:
                return false;

        }
    }

    public Shape Invert()
    {   
        if (SideShape == Shape.Concave) return Shape.Convex; 
        else return Shape.Concave;
    }

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public ShapeSide(Shape sideShape)
    {
        _sideShape = sideShape;
    }
}

public enum Shape
{
    Flat,
    Concave,
    Convex,
    Edge,
    Default
}