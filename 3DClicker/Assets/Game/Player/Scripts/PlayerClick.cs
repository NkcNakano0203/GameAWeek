using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerClick : MonoBehaviour
{
    [SerializeField]
    InputHandler inputHandler;
    IInputHandler input;
    Camera mainCamera;

    void Start()
    {
        input = inputHandler.GetComponent<IInputHandler>();
        mainCamera = Camera.main;
        input.Clicked.Subscribe(OnClicked);
    }

    private void OnClicked(Vector2 mousePos)
    {
        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        RaycastHit[] hits = Physics.RaycastAll(ray);
        if (hits.Length == 0) return;
        foreach (var item in hits)
        {
            if (!item.collider.TryGetComponent(out IClickable clickableObj)) continue;
            clickableObj.OnClicked();
        }
    }
}