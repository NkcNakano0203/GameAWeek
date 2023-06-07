using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class ButtonClicked : MonoBehaviour, IClickable
{
    [SerializeField]
    Transform coinParent;

    [SerializeField]
    Coin coin;

    [SerializeField]
    Transform insPos;

    private Subject<int> clickSubject = new();
    public IObservable<int> ClickSubject => clickSubject;

    int clickPower = 1;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnClicked()
    {
        // ‚¢‚Â‚¢‚©‚È‚éŽž‚Å‚àƒNƒŠƒbƒN”»’è‚ÍŽæ‚è‚½‚¢
        clickSubject.OnNext(clickPower);
        rb.AddTorque(new Vector3(0, 1, 0), ForceMode.Impulse);

        Coin coin = Instantiate(this.coin, insPos.position, Quaternion.identity, coinParent);
        coin.RandomAddForce();
    }
}