using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        RectTransform rectTransform = transform as RectTransform;
        rectTransform.DOScale(1.25f, 0.25f).SetEase(Ease.InOutQuad);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        RectTransform rectTransform = transform as RectTransform;
        rectTransform.DOScale(1.0f, 0.25f);
    }
}
