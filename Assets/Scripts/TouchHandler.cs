using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchHandler : MonoBehaviour
{
    [SerializeField] private PuzzleHandler _puzzleHandler;

    [SerializeField] private LayerMask _whatIsPuzzle;
    [SerializeField] private LayerMask _whatIsCells;

    [SerializeField] private float _touchRadius;
    [SerializeField] private float _mergeRadius;
    [SerializeField] private float _tapRadius;

    private Vector2 _startTouchPosition;

    private bool _isTap = false;

    public GameObject Puzzle { get; set; }

    private Camera _mainCamera;

    private void Awake()
    {
        Input.multiTouchEnabled = false;
    }

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        OnTouch();
    }

    private void OnTouch()
    {      
        foreach (var touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                Puzzle = Physics2D.OverlapCircle(_mainCamera.ScreenToWorldPoint(touch.position), _touchRadius, _whatIsPuzzle)?.gameObject;

                var cell = Physics2D.OverlapCircle(_mainCamera.ScreenToWorldPoint(touch.position), _mergeRadius, _whatIsCells);

                _isTap = true;
                _startTouchPosition = _mainCamera.ScreenToWorldPoint(touch.position);

                if (cell != null && Puzzle != null)
                {
                    var puzzle = Puzzle.GetComponent<Puzzle>();

                    var x = cell.GetComponent<Cell>().Xpos;
                    var y = cell.GetComponent<Cell>().Ypos;

                    _puzzleHandler.CurrentPuzzleMatrix[x, y] = _puzzleHandler.PuzzlePrefab.GetComponent<Puzzle>();
                    puzzle.Setted = false;
                    puzzle.UnSet();
                }
            }

            if (touch.phase == TouchPhase.Moved && Puzzle != null)
            {
                if (Vector2.Distance(_mainCamera.ScreenToWorldPoint(touch.position), _startTouchPosition) > _tapRadius)
                {
                    _isTap = false;
                }
                
                Puzzle.transform.position = (Vector2)_mainCamera.ScreenToWorldPoint(touch.position);
                Puzzle.GetComponent<Puzzle>().Setted = false;
            }

            if (touch.phase == TouchPhase.Ended && Puzzle != null)
            {
                var cell = Physics2D.OverlapCircle(_mainCamera.ScreenToWorldPoint(touch.position), _mergeRadius, _whatIsCells);
                var puzzle = Puzzle.GetComponent<Puzzle>();

                if(_isTap)
                {
                    puzzle.Rotate();                  
                }

                if (cell != null)
                {                  
                    var x = cell.GetComponent<Cell>().Xpos;
                    var y = cell.GetComponent<Cell>().Ypos;                 

                    if (_puzzleHandler.CanPutPuzzle(puzzle, x, y))
                    {
                        Puzzle.transform.position = cell.transform.position;
                        _puzzleHandler.CurrentPuzzleMatrix[x, y] = puzzle;

                        puzzle.Setted = true;
                        puzzle.Set();

                        foreach (var currentPuzzle in _puzzleHandler.CurrentPuzzleMatrix)
                        {
                            if (currentPuzzle.Setted == false)
                            {
                                return;
                            }
                        }

                        ActionHandler.WonGame();
                    }
                    else
                    {
                        puzzle.WrongPosition();
                    }
                }                               
            }
        }
    }
}
