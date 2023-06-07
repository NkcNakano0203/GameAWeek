using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    public abstract int Cost { get; }
    public abstract int CoinPerSecond { get; }
    protected abstract void ChangeActive(bool isActive);
    protected abstract bool isAvailable(int moneyValue);
}