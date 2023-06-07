using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class ItemBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    protected Text toolTip;

    [SerializeField]
    protected Button button;

    public abstract int Cost { get; }

    protected abstract string ToolTipText { get; }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!button.interactable) return;
        toolTip.text = ToolTipText;
        toolTip.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!button.interactable) return;
        Debug.Log("Exit");
        toolTip.enabled = false;
    }

    protected abstract bool isAvailable(int moneyValue);
}