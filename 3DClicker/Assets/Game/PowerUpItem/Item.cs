using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

public class Item : ItemBase
{
    [SerializeField]
    MoneyManager moneyManager;

    [SerializeField]
    private int cost = 0;
    public override int Cost => cost;

    [SerializeField]
    private int coinPerSecond = 0;
    public int CoinPerSecond => coinPerSecond;

    // 使用可能かを持つプロパティ
    private ReactiveProperty<bool> interactableProp = new();
    public IReadOnlyReactiveProperty<bool> InteractableProp => interactableProp;


    private Subject<Unit> useSubject = new Subject<Unit>();
    public IObservable<Unit> UseSubject => useSubject;

    private string toolTipText;
    protected override string ToolTipText => toolTipText;

    private void Start()
    {
        toolTipText = $"1秒間に{CoinPerSecond}コイン増える";

        // お金が増えたら使用可能か判断する
        moneyManager.MoneyProp.Subscribe(x =>
        {
            button.interactable = isAvailable(x);
            if (!button.interactable) toolTip.enabled = false;
        });

        // 使用された時にイベントを発行する
        button.OnClickAsObservable().Subscribe(_ =>
        {
            useSubject.OnNext(default);
        });
    }

    // 使用可能か判断
    protected override bool isAvailable(int moneyValue)
    {
        return moneyValue > cost;
    }
}