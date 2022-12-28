using UnityEngine;

public class ShapedPuzzleHandler : PuzzleHandler
{
    protected override void Awake()
    {
        base.Awake();

        CurrentPuzzleMatrix = new ShapePuzzle[MatrixSize, MatrixSize];
        PuzzleMatrix = new ShapePuzzle[MatrixSize, MatrixSize];
    }

    private void Start()
    {
        CreatePuzzlesCells();
        CreatePuzzles();
        RandomGeneratePuzzles();
        RandomRotationPuzzles();

        ShapedPuzzlesIconContainer shapedPuzzlesIconContainer = PuzzlesIconContainer as ShapedPuzzlesIconContainer;

        shapedPuzzlesIconContainer.CreatePuzzlesIcons(MatrixSize, PuzzleMatrix);
        shapedPuzzlesIconContainer.ShuffleIcons();    
    }

    protected override void CreatePuzzles()
    { 
        for (int i = 0; i < MatrixSize; i++)
        {
            for (int j = 0; j < MatrixSize; j++)
            {               
                PuzzleGameObjectsMatrix[i, j] = Instantiate(PuzzlePrefab);
                PuzzleMatrix[i, j] = PuzzleGameObjectsMatrix[i, j].GetComponent<ShapePuzzle>();
                PuzzleGameObjectsMatrix[i, j].SetActive(false);
                CurrentPuzzleMatrix[i, j] = PuzzlePrefab.GetComponent<ShapePuzzle>();
            }
        }
    }

    protected override void RandomGeneratePuzzles()
    {
        ShapePuzzle[,] shapedPuzzleMatrix = PuzzleMatrix as ShapePuzzle[,];

        for (int x = 0; x < MatrixSize; x++)
        {
            for (int y = 0; y < MatrixSize; y++)
            {
                ShapePuzzle leftPuzzle, rightPuzzle, upperPuzzle, lowerPuzzle, puzzle;

                if (x == 0) upperPuzzle = null;
                else upperPuzzle = shapedPuzzleMatrix[x - 1, y];

                if (x == MatrixSize - 1) lowerPuzzle = null;
                else lowerPuzzle = shapedPuzzleMatrix[x + 1, y];

                if (y == 0) leftPuzzle = null;
                else leftPuzzle = shapedPuzzleMatrix[x, y - 1];

                if (y == MatrixSize - 1) rightPuzzle = null;
                else rightPuzzle = shapedPuzzleMatrix[x, y + 1];

                puzzle = shapedPuzzleMatrix[x, y];

                if (upperPuzzle == null) puzzle.UpperSide.SideShape = Shape.Flat;
                else if (upperPuzzle.LowerSide.SideShape != Shape.Default) puzzle.UpperSide.SideShape = upperPuzzle.LowerSide.Invert();
                else puzzle.UpperSide.SideShape = (Shape)Random.Range(1, 3);

                if (lowerPuzzle == null) puzzle.LowerSide.SideShape = Shape.Flat;
                else if (lowerPuzzle.UpperSide.SideShape != Shape.Default) puzzle.LowerSide.SideShape = lowerPuzzle.UpperSide.Invert();
                else puzzle.LowerSide.SideShape = (Shape)Random.Range(1, 3);
            
                if (rightPuzzle == null) puzzle.RightSide.SideShape = Shape.Flat;
                else if (rightPuzzle.LeftSide.SideShape != Shape.Default) puzzle.RightSide.SideShape = rightPuzzle.LeftSide.Invert();
                else puzzle.RightSide.SideShape = (Shape)Random.Range(1, 3);

                if (leftPuzzle == null) puzzle.LeftSide.SideShape = Shape.Flat;
                else if (leftPuzzle.RightSide.SideShape != Shape.Default) puzzle.LeftSide.SideShape = leftPuzzle.RightSide.Invert(); 
                else puzzle.LeftSide.SideShape = (Shape)Random.Range(1, 3);
              
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
        ShapePuzzle shapedPuzzle = puzzle as ShapePuzzle;

        ShapePuzzle[,] currentShapedPuzzleMatrix = CurrentPuzzleMatrix as ShapePuzzle[,];

        ShapePuzzle leftPuzzle, rightPuzzle, upperPuzzle, lowerPuzzle;

        if (x == 0) 
        { 
            lowerPuzzle = PuzzleEdgePrefab.GetComponent<ShapePuzzle>(); 
        }
        else 
        { 
            lowerPuzzle = currentShapedPuzzleMatrix[x - 1, y]; 
        }

        if (x == MatrixSize - 1) 
        { 
            upperPuzzle = PuzzleEdgePrefab.GetComponent<ShapePuzzle>(); }
        else 
        { 
            upperPuzzle = currentShapedPuzzleMatrix[x + 1, y]; 
        }

        if (y == 0) 
        { 
            leftPuzzle = PuzzleEdgePrefab.GetComponent<ShapePuzzle>(); 
        }
        else 
        { 
            leftPuzzle = currentShapedPuzzleMatrix[x, y - 1]; 
        }

        if (y == MatrixSize - 1) 
        { 
            rightPuzzle = PuzzleEdgePrefab.GetComponent<ShapePuzzle>(); 
        }
        else 
        { 
            rightPuzzle = currentShapedPuzzleMatrix[x, y + 1]; 
        }


        if (shapedPuzzle.UpperSide != upperPuzzle.LowerSide)
        {
            return false;
        }

        if (shapedPuzzle.LowerSide != lowerPuzzle.UpperSide)
        {
            return false;
        }

        if (shapedPuzzle.RightSide != rightPuzzle.LeftSide)
        {
            return false;
        }

        if (shapedPuzzle.LeftSide != leftPuzzle.RightSide)
        {
            return false;
        }

        return true;
    }

}
