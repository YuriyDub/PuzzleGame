using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPuzzleHandler : PuzzleHandler
{
    [SerializeField] private ColorsData _colorsData;

    protected override void Awake()
    {
        base.Awake();

        CurrentPuzzleMatrix = new ColorPuzzle[MatrixSize, MatrixSize];
        PuzzleMatrix = new ColorPuzzle[MatrixSize, MatrixSize];
    }

    private void Start()
    {
        CreatePuzzlesCells();
        CreatePuzzles();
        RandomGeneratePuzzles();
        RandomRotationPuzzles();

        ColorPuzzleIconContainer colorPuzzlesIconContainer = PuzzlesIconContainer as ColorPuzzleIconContainer;

        colorPuzzlesIconContainer.CreatePuzzlesIcons(MatrixSize, PuzzleMatrix);
        colorPuzzlesIconContainer.ShuffleIcons();
    }


    protected override void CreatePuzzles()
    {
        for (int i = 0; i < MatrixSize; i++)
        {
            for (int j = 0; j < MatrixSize; j++)
            {
                PuzzleGameObjectsMatrix[i, j] = Instantiate(PuzzlePrefab);
                PuzzleMatrix[i, j] = PuzzleGameObjectsMatrix[i, j].GetComponent<ColorPuzzle>();
                PuzzleGameObjectsMatrix[i, j].SetActive(false);
                CurrentPuzzleMatrix[i, j] = PuzzlePrefab.GetComponent<ColorPuzzle>();
            }
        }
    }

    protected override void RandomGeneratePuzzles()
    {
        ColorPuzzle[,] coloredPuzzleMatrix = PuzzleMatrix as ColorPuzzle[,];

        for (int x = 0; x < MatrixSize; x++)
        {
            for (int y = 0; y < MatrixSize; y++)
            {
                ColorPuzzle leftPuzzle, rightPuzzle, upperPuzzle, lowerPuzzle, puzzle;

                if (x == 0) upperPuzzle = null;
                else upperPuzzle = coloredPuzzleMatrix[x - 1, y];

                if (x == MatrixSize - 1) lowerPuzzle = null;
                else lowerPuzzle = coloredPuzzleMatrix[x + 1, y];

                if (y == 0) leftPuzzle = null;
                else leftPuzzle = coloredPuzzleMatrix[x, y - 1];

                if (y == MatrixSize - 1) rightPuzzle = null;
                else rightPuzzle = coloredPuzzleMatrix[x, y + 1];

                puzzle = coloredPuzzleMatrix[x, y];

                if (upperPuzzle == null) puzzle.UpperSide.SideColor = _colorsData.EdgeColor;
                else if (upperPuzzle.LowerSide.SideColor != _colorsData.DefaultColor) puzzle.UpperSide.SideColor = upperPuzzle.LowerSide.SideColor;
                else puzzle.UpperSide.SideColor = _colorsData.RandomColor();

                if (lowerPuzzle == null) puzzle.LowerSide.SideColor = _colorsData.EdgeColor;
                else if (lowerPuzzle.UpperSide.SideColor != _colorsData.DefaultColor) puzzle.LowerSide.SideColor = lowerPuzzle.UpperSide.SideColor;
                else puzzle.LowerSide.SideColor = _colorsData.RandomColor();

                if (rightPuzzle == null) puzzle.RightSide.SideColor = _colorsData.EdgeColor;
                else if (rightPuzzle.LeftSide.SideColor != _colorsData.DefaultColor) puzzle.RightSide.SideColor = rightPuzzle.LeftSide.SideColor;
                else puzzle.RightSide.SideColor = _colorsData.RandomColor();

                if (leftPuzzle == null) puzzle.LeftSide.SideColor = _colorsData.EdgeColor;
                else if (leftPuzzle.RightSide.SideColor != _colorsData.DefaultColor) puzzle.LeftSide.SideColor = leftPuzzle.RightSide.SideColor;
                else puzzle.LeftSide.SideColor = _colorsData.RandomColor();              
            }
        }
    }

    protected override void RandomRotationPuzzles()
    {
        for (int x = 0; x < MatrixSize; x++)
        {
            for (int y = 0; y < MatrixSize; y++)
            {
                if (Random.Range(0, 2) == 1)
                {
                    PuzzleMatrix[x, y].Rotate();
                }
            }
        }
    }

    public override bool CanPutPuzzle(Puzzle puzzle, int x, int y)
    {
        ColorPuzzle shapedPuzzle = puzzle as ColorPuzzle;

        ColorPuzzle[,] currentShapedPuzzleMatrix = CurrentPuzzleMatrix as ColorPuzzle[,];

        ColorPuzzle leftPuzzle, rightPuzzle, upperPuzzle, lowerPuzzle;      

        if (x == 0)
        {
            lowerPuzzle = PuzzleEdgePrefab.GetComponent<ColorPuzzle>();
        }
        else
        {
            lowerPuzzle = currentShapedPuzzleMatrix[x - 1, y];
        }

        if (x == MatrixSize - 1)
        {
            upperPuzzle = PuzzleEdgePrefab.GetComponent<ColorPuzzle>();
        }
        else
        {
            upperPuzzle = currentShapedPuzzleMatrix[x + 1, y];
        }

        if (y == 0)
        {
            leftPuzzle = PuzzleEdgePrefab.GetComponent<ColorPuzzle>();
        }
        else
        {
            leftPuzzle = currentShapedPuzzleMatrix[x, y - 1];
        }

        if (y == MatrixSize - 1)
        {
            rightPuzzle = PuzzleEdgePrefab.GetComponent<ColorPuzzle>();
        }
        else
        {
            rightPuzzle = currentShapedPuzzleMatrix[x, y + 1];
        }

        if (shapedPuzzle.UpperSide.SideColor == _colorsData.EdgeColor && upperPuzzle.LowerSide.SideColor == _colorsData.DefaultColor)
        {
            return false;
        }

        if (shapedPuzzle.LowerSide.SideColor == _colorsData.EdgeColor && lowerPuzzle.UpperSide.SideColor == _colorsData.DefaultColor)
        {
            return false;
        }

        if (shapedPuzzle.RightSide.SideColor == _colorsData.EdgeColor && rightPuzzle.LeftSide.SideColor == _colorsData.DefaultColor)
        {
            return false;
        }

        if (shapedPuzzle.LeftSide.SideColor == _colorsData.EdgeColor && leftPuzzle.RightSide.SideColor == _colorsData.DefaultColor)
        {
            return false;
        }

        if (shapedPuzzle.UpperSide.SideColor != upperPuzzle.LowerSide.SideColor && upperPuzzle.LowerSide.SideColor != _colorsData.DefaultColor)
        {
            return false;
        }

        if (shapedPuzzle.LowerSide.SideColor != lowerPuzzle.UpperSide.SideColor && lowerPuzzle.UpperSide.SideColor != _colorsData.DefaultColor)
        {
            return false;
        }

        if (shapedPuzzle.RightSide.SideColor != rightPuzzle.LeftSide.SideColor && rightPuzzle.LeftSide.SideColor != _colorsData.DefaultColor)
        {
            return false;
        }

        if (shapedPuzzle.LeftSide.SideColor != leftPuzzle.RightSide.SideColor && leftPuzzle.RightSide.SideColor != _colorsData.DefaultColor)
        {
            return false;
        }

        return true;
    }
}
