using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Puzzle : MonoBehaviour
{
    public abstract bool Setted { get; set; }

    public abstract void Rotate();

    public abstract void Appear();

    public abstract void Set();

    public abstract void UnSet();

    public abstract void WrongPosition();

}
