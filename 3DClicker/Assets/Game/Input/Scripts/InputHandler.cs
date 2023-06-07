using UniRx;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour, IInputHandler
{
    [SerializeField]
    PlayerInput playerInput;

    private Subject<Vector2> clickProp = new Subject<Vector2>();
    public IObservable<Vector2> Clicked => clickProp;

    private void Start()
    {
        playerInput.actions["Click"].performed += OnClicked;
    }

    private void OnClicked(InputAction.CallbackContext callback)
    {
        clickProp.OnNext(Mouse.current.position.ReadValue());
    }
}