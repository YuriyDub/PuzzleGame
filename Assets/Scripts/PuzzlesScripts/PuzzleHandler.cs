using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PuzzleHandler : MonoBehaviour
{
    [Header("Prefabs:")]
    [SerializeField] protected GameObject Cell;
    [SerializeField] protected GameObject PuzzleEdgePrefab;
    [field: SerializeField] public GameObject PuzzlePrefab { get; set; }

    protected GameObject[,] PuzzleGameObjectsMatrix;


    [Header("UI")]
    [SerializeField] protected PuzzlesIconContainer PuzzlesIconContainer;


    [Header("Parameters")]
    [SerializeField] protected float CellSize;
    [SerializeField] protected int MatrixSize;
    [SerializeField] protected Vector2 Offset;

    protected Puzzle[,] PuzzleMatrix;

    public Cell[,] Cells { get; set; }

    public Puzzle[,] CurrentPuzzleMatrix { get; set; }

    protected virtual void Awake()
    {
        PuzzleGameObjectsMatrix = new GameObject[MatrixSize, MatrixSize];
        Cells = new Cell[MatrixSize, MatrixSize];
    }

    protected void CreatePuzzlesCells()
    {
        Vector2 matrixOffset = (new Vector2(MatrixSize * CellSize - CellSize, MatrixSize * CellSize - CellSize)) / 2 - Offset;

        for (int i = 0; i < MatrixSize; i++)
        {
            for (int j = 0; j < MatrixSize; j++)
            {
                Cells[i, j] = Instantiate(Cell, CellSize * new Vector2(j, i) - matrixOffset, Quaternion.identity, transform).GetComponent<Cell>();
                Cells[i, j].Xpos = i;
                Cells[i, j].Ypos = j;
            }
        }
    }
    protected abstract void CreatePuzzles();
    protected abstract void RandomGeneratePuzzles();
    protected abstract void RandomRotationPuzzles();
    public abstract bool CanPutPuzzle(Puzzle puzzle, int x, int y);

}
