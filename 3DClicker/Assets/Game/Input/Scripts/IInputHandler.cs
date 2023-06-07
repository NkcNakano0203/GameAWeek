using System;
using UnityEngine;

public interface IInputHandler 
{
    public IObservable<Vector2> Clicked { get; }
}