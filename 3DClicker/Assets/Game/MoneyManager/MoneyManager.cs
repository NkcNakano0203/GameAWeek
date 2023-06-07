using UnityEngine;
using UniRx;

public class MoneyManager : MonoBehaviour
{
    [SerializeField]
    ButtonClicked buttonClicked;

    [SerializeField]
    Item[] items;

    private ReactiveProperty<int> moneyProp = new();
    public IReadOnlyReactiveProperty<int> MoneyProp => moneyProp;

    // •\Ž¦—p
    [SerializeField, ReadOnly]
    private int money = 0;

    int coinPerSecond = 0;

    private void Start()
    {
        buttonClicked.ClickSubject.Subscribe(x => { moneyProp.Value += x; });
        MoneyProp.Subscribe(x => { money = x; });
        foreach (var item in items)
        {
            item.UseSubject.Subscribe(_ =>
            {
                moneyProp.Value -= item.Cost;
                coinPerSecond += item.CoinPerSecond;
            });
        }
    }

    float t = 0;
    private void FixedUpdate()
    {
        t += Time.fixedDeltaTime;
        if (t >= 1.0f)
        {
            moneyProp.Value += coinPerSecond;
            t = 0;
        }
    }
}