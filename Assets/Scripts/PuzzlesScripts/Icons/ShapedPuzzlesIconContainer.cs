using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShapedPuzzlesIconContainer : PuzzlesIconContainer
{
    public List<ShapePuzzleIcon> ShapePuzzlesIcons { get; set; } = new List<ShapePuzzleIcon>();

    public override void CreatePuzzlesIcons(int size, Puzzle[,] puzzles)
    {

        ShapePuzzle[,] shapedPuzzles = puzzles as ShapePuzzle[,];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {               
                 var icon = Instantiate(IconPrefab, transform).GetComponent<ShapePuzzleIcon>();

                 icon.ShapePuzzle = shapedPuzzles[i, j];

                 ShapePuzzlesIcons.Add(icon);
            }
        }
    }

    public override void ShuffleIcons()
    {
        var n = ShapePuzzlesIcons.Count;

        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            ShapePuzzlesIcons[n].gameObject.transform.SetSiblingIndex(k);
        }
    }
}
