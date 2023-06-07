using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Item : ItemBase
{
    [SerializeField]
    MoneyManager moneyManager;

    private ReactiveProperty<bool> activeProp = new();
    public IReadOnlyReactiveProperty<bool> ActiveProp => activeProp;

    [SerializeField]
    private int cost = 0;
    public override int Cost => cost;

    [SerializeField]
    private int coinPerSecond = 0;
    public override int CoinPerSecond => coinPerSecond;

    private void Start()
    {
        ActiveProp.Subscribe(ChangeActive);
        moneyManager.MoneyProp.Subscribe(x =>
        {
            activeProp.Value = isAvailable(x);
        });
    }

    protected override void ChangeActive(bool isActive)
    {
        activeProp.Value = isActive;
    }

    protected override bool isAvailable(int moneyValue)
    {
        return moneyValue > cost;
    }
}