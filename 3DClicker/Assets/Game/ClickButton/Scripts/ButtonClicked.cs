using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using DG.Tweening;

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
    bool isAnimation = false;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnClicked()
    {
        // いついかなる時でもクリック判定は取りたい
        clickSubject.OnNext(clickPower);
        rb.AddTorque(new Vector3(0, 1, 0), ForceMode.Impulse);

        Coin coin = Instantiate(this.coin, insPos.position, Quaternion.identity, coinParent);
        coin.RandomAddForce();

        // アニメーションは重複しない
        if (isAnimation) return;
        //isAnimation = true;
        //Vector3 beforePos = transform.position;
        //transform.DOJump(beforePos, 0.1f, 1, 0.2f)
        //    .OnComplete(() =>
        //    {
        //        isAnimation = false;
        //        transform.position = beforePos;
        //    });
        //StartCoroutine(MoveAniamtion());
    }

    IEnumerator MoveAniamtion()
    {
        isAnimation = true;
        Vector3 defaultPos = transform.position;
        float destinationPosY = defaultPos.y + 0.1f;
        for (float t = 0; t <= 1.0f; t += Time.deltaTime / 10)
        {
            float movePosY = Mathf.Lerp(destinationPosY, defaultPos.y, t);
            transform.position = new Vector3(defaultPos.x, movePosY, defaultPos.z);
        }
        for (float t = 0; t <= 1.0f; t += Time.deltaTime / 10)
        {
            float movePosY = Mathf.Lerp(defaultPos.y, destinationPosY, t);
            transform.position = new Vector3(defaultPos.x, movePosY, defaultPos.z);
        }
        isAnimation = false;
        yield return null;
    }
}