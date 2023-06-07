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

    // �g�p�\�������v���p�e�B
    private ReactiveProperty<bool> interactableProp = new();
    public IReadOnlyReactiveProperty<bool> InteractableProp => interactableProp;


    private Subject<Unit> useSubject = new Subject<Unit>();
    public IObservable<Unit> UseSubject => useSubject;

    private string toolTipText;
    protected override string ToolTipText => toolTipText;

    private void Start()
    {
        toolTipText = $"1�b�Ԃ�{CoinPerSecond}�R�C��������";

        // ��������������g�p�\�����f����
        moneyManager.MoneyProp.Subscribe(x =>
        {
            button.interactable = isAvailable(x);
            if (!button.interactable) toolTip.enabled = false;
        });

        // �g�p���ꂽ���ɃC�x���g�𔭍s����
        button.OnClickAsObservable().Subscribe(_ =>
        {
            useSubject.OnNext(default);
        });
    }

    // �g�p�\�����f
    protected override bool isAvailable(int moneyValue)
    {
        return moneyValue > cost;
    }
}