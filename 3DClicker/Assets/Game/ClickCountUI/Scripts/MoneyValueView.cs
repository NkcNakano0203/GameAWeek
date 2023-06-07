using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class MoneyValueView : MonoBehaviour
{
    public Text text;

    public void SetText(int value)
    {
        text.text = $"{value}";
    }
}