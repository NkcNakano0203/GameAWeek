using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class MoneyValuePresenter : MonoBehaviour
{
    [SerializeField]
    private MoneyManager model;

    [SerializeField]
    private MoneyValueView view;

    private void Start()
    {
        model.MoneyProp.Subscribe(view.SetText);
    }
}
