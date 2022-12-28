using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPuzzleIconContainer : PuzzlesIconContainer
{
    public List<ColorPuzzleIcon> ColorPuzzlesIcons { get; set; } = new List<ColorPuzzleIcon>();

    public override void CreatePuzzlesIcons(int size, Puzzle[,] puzzles)
    {

        ColorPuzzle[,] shapedPuzzles = puzzles as ColorPuzzle[,];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                var icon = Instantiate(IconPrefab, transform).GetComponent<ColorPuzzleIcon>();

                icon.ColorPuzzle = shapedPuzzles[i, j];

                ColorPuzzlesIcons.Add(icon);
            }
        }
    }

    public override void ShuffleIcons()
    {
        var n = ColorPuzzlesIcons.Count;

        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            ColorPuzzlesIcons[n].gameObject.transform.SetSiblingIndex(k);
        }
    }

}
