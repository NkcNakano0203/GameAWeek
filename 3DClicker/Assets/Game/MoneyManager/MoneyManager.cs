using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class MoneyManager : MonoBehaviour
{
    private ReactiveProperty<int> moneyProp = new();
    public IReadOnlyReactiveProperty<int> MoneyProp => moneyProp;

    void Start()
    {

    }
}