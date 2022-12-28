using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ColorPuzzleIcon : MonoBehaviour, IPointerDownHandler, IPointerMoveHandler
{
    [field: SerializeField] public Image LeftSideImage { get; set; }
    [field: SerializeField] public Image RightSideImage { get; set; }
    [field: SerializeField] public Image UpperSideImage { get; set; }
    [field: SerializeField] public Image LowerSideImage { get; set; }

    [SerializeField] private float _timeToDestroy;

    private Animator _animator;

    private TouchHandler _touchHandler;

    private bool _isPointerDown;

    public ColorPuzzle ColorPuzzle { get; set; }

    private void Start()
    {
        _touchHandler = FindObjectOfType<TouchHandler>();
        _animator = GetComponent<Animator>();

        Invoke("SetPuzzlesSprites", Time.deltaTime);
    }

    private void SetPuzzlesSprites()
    {
        LeftSideImage.color = ColorPuzzle.LeftSide.SpriteRenderer.color;
        RightSideImage.color = ColorPuzzle.RightSide.SpriteRenderer.color;
        UpperSideImage.color = ColorPuzzle.UpperSide.SpriteRenderer.color;
        LowerSideImage.color = ColorPuzzle.LowerSide.SpriteRenderer.color;
    }

    private void DisAppear()
    {
        _animator.SetBool("IsAppear", false);
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        _isPointerDown = true;
    }

    public void OnPointerMove(PointerEventData pointerEventData)
    {
        if (_isPointerDown)
        {
            ColorPuzzle.gameObject.SetActive(true);

            _touchHandler.Puzzle = ColorPuzzle.gameObject;

            ColorPuzzle.Appear();

            DisAppear();

            Destroy(gameObject, _timeToDestroy);
        }
    }
}
