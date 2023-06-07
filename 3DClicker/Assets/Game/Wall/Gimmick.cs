using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class Gimmick : MonoBehaviour
{
    [SerializeField]
    Transform rotPoint;

    private async void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent<Coin>(out _)) return;
        await rotPoint.DOLocalRotate(new Vector3(0, 0, 90), 2.0f).SetLink(gameObject);
        await UniTask.Delay(5000);
        await rotPoint.DOLocalRotate(new Vector3(0, 0, 0), 2.0f).SetLink(gameObject);
    }
}