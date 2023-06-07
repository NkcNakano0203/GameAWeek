using UniRx;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour, IInputHandler
{
    [SerializeField]
    PlayerInput playerInput;

    private Subject<Vector2> clickSubject = new Subject<Vector2>();
    public IObservable<Vector2> Clicked => clickSubject;

    private Subject<Unit> escapeSubject = new Subject<Unit>();
    public IObservable<Unit> Escape => escapeSubject;

    private void Start()
    {
        playerInput.actions["Click"].performed += OnClicked;
        playerInput.actions["ESC"].performed += OnEscaped;
    }

    private void OnClicked(InputAction.CallbackContext callback)
    {
        clickSubject.OnNext(Mouse.current.position.ReadValue());
    }

    private void OnEscaped(InputAction.CallbackContext callback)
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}