using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShapePuzzleIcon : MonoBehaviour, IPointerDownHandler, IPointerMoveHandler
{
    [field: SerializeField] public Image LeftSideImage { get; set; }
    [field: SerializeField] public Image RightSideImage { get; set; }
    [field: SerializeField] public Image UpperSideImage { get; set; }
    [field: SerializeField] public Image LowerSideImage { get; set; }

    [SerializeField] private float _timeToDestroy;

    private Animator _animator;

    private TouchHandler _touchHandler;

    private bool _isPointerDown;

    public ShapePuzzle ShapePuzzle { get; set; }
   
    private void Start()
    {
        _touchHandler = FindObjectOfType<TouchHandler>();
        _animator = GetComponent<Animator>();

        Invoke("SetPuzzlesSprites", Time.deltaTime);
    }

    private void SetPuzzlesSprites()
    {
        LeftSideImage.sprite = ShapePuzzle.LeftSide.SpriteRenderer.sprite;
        RightSideImage.sprite = ShapePuzzle.RightSide.SpriteRenderer.sprite;
        UpperSideImage.sprite = ShapePuzzle.UpperSide.SpriteRenderer.sprite;
        LowerSideImage.sprite = ShapePuzzle.LowerSide.SpriteRenderer.sprite;
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
            ShapePuzzle.gameObject.SetActive(true);

            _touchHandler.Puzzle = ShapePuzzle.gameObject;

            ShapePuzzle.Appear();

            DisAppear();

            Destroy(gameObject,_timeToDestroy);            
        }
    }
}
