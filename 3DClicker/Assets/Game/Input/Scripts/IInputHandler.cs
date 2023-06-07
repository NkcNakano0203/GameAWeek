using System;
using UniRx;
using UnityEngine;

public interface IInputHandler 
{
    public IObservable<Vector2> Clicked { get; }
    public IObservable<Unit> Escape { get; }
}