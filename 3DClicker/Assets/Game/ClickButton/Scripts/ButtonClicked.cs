using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UniRx;
using System;

public class ButtonClicked : MonoBehaviour, IPointerClickHandler
{
    RectTransform rectTransform;

    int clickPower = 1;
    bool isAnimation = false;

    private Subject<int> clickSubject = new Subject<int>();
    public IObservable<int> ClickSubject => clickSubject;

    private void Awake()
    {
        rectTransform = transform as RectTransform;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // いついかなる時でもクリック判定は取りたい
        clickSubject.OnNext(clickPower);

        //アニメーションは同時に再生されない
        if (isAnimation) return;
        isAnimation = true;
        rectTransform.DOShakeAnchorPos(0.1f, 50, randomnessMode: ShakeRandomnessMode.Harmonic)
            .SetLink(gameObject)
            .OnComplete(() =>
            {
                isAnimation = false;
            });
    }

    void Start()
    {
        ClickSubject.Subscribe(_ => { Debug.Log("クリック！"); });
    }
}