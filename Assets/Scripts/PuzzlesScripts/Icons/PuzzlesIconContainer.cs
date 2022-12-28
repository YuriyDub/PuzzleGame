using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PuzzlesIconContainer : MonoBehaviour
{
    [field: Header("IconPrefab")]
    [field: SerializeField] protected GameObject IconPrefab { get; set; }

    public abstract void CreatePuzzlesIcons(int size, Puzzle[,] puzzles);

    public abstract void ShuffleIcons();
}
