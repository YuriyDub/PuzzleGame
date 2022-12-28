using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShapePuzzle : Puzzle
{
    [field: SerializeField] public ShapeSide LeftSide { get; set; }
    [field: SerializeField] public ShapeSide RightSide { get; set; } 
    [field: SerializeField] public ShapeSide UpperSide { get; set; }
    [field: SerializeField] public ShapeSide LowerSide { get; set; } 

    private Animator _animator;

    public override bool Setted { get; set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public override void Rotate()
    {
        Shape FirstAdditionalShape = RightSide.SideShape;
        Shape SecondAdditionalShape = LowerSide.SideShape;

        RightSide.SideShape = UpperSide.SideShape;

        LowerSide.SideShape = FirstAdditionalShape;

        FirstAdditionalShape = LeftSide.SideShape;

        LeftSide.SideShape = SecondAdditionalShape;

        UpperSide.SideShape = FirstAdditionalShape;
    }

    public override void Appear()
    {
        _animator.SetBool("IsAppear", true);
    }

    public override void Set()
    {
        _animator.SetBool("IsSetted", true);
    }

    public override void UnSet()
    {
        _animator.SetBool("IsSetted", false);
    }

    public override void WrongPosition()
    {
        _animator.SetTrigger("InWrongPosition");
    }
}
