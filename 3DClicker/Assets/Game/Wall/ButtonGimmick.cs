using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;

public class ButtonGimmick : MonoBehaviour
{
    [SerializeField]
    Transform rotPoint1;
    [SerializeField]
    Transform rotPoint2;
    [SerializeField]
    Transform rotPoint3;

    [SerializeField]
    Button button;

    private void Start()
    {
        button.onClick.AddListener(Onclick);
    }

    public async void Onclick()
    {
        rotPoint1.DOLocalRotate(new Vector3(0, 0, -90), 2.0f).SetLink(gameObject).ToUniTask().Forget();
        rotPoint2.DOLocalRotate(new Vector3(0, 0, 90), 2.0f).SetLink(gameObject).ToUniTask().Forget();
        await rotPoint3.DOLocalRotate(new Vector3(90, 0, 0), 2.0f).SetLink(gameObject);

        await UniTask.Delay(2000);

        rotPoint1.DOLocalRotate(new Vector3(0, 0, 0), 2.0f).SetLink(gameObject).ToUniTask().Forget();
        rotPoint2.DOLocalRotate(new Vector3(0, 0, 0), 2.0f).SetLink(gameObject).ToUniTask().Forget();
        await rotPoint3.DOLocalRotate(new Vector3(0, 0, 0), 2.0f).SetLink(gameObject);
    }
}