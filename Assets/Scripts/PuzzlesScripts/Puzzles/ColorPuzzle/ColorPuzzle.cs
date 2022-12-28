using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPuzzle : Puzzle
{
    [field: SerializeField] public ColorSide LeftSide { get; set; }
    [field: SerializeField] public ColorSide RightSide { get; set; }
    [field: SerializeField] public ColorSide UpperSide { get; set; }
    [field: SerializeField] public ColorSide LowerSide { get; set; }

    private Animator _animator;

    public override bool Setted { get; set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public override void Rotate()
    {
        Color FirstAdditionalColor = RightSide.SideColor;
        Color SecondAdditionalColor = LowerSide.SideColor;

        RightSide.SideColor = UpperSide.SideColor;

        LowerSide.SideColor = FirstAdditionalColor;

        FirstAdditionalColor = LeftSide.SideColor;

        LeftSide.SideColor = SecondAdditionalColor;

        UpperSide.SideColor = FirstAdditionalColor;

        print(1);
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
