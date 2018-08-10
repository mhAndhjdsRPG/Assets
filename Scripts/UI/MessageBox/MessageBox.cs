using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public abstract class MessageBox : WindowBase, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public bool canDrag = true;

    private Vector2 startMousePos;
    private Vector2 startWindowPos;
    protected override void OnAwake()
    {
        base.OnAwake();
        windowType = WindowType.MessageBox;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (canDrag)
        {
            UIManager.Instance.CurrentWindow = this;
            startMousePos = Input.mousePosition;
            startWindowPos = windowBackground.rectTransform.anchoredPosition;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canDrag)
        {
            windowBackground.rectTransform.anchoredPosition = startWindowPos + ((Vector2)Input.mousePosition - startMousePos);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }
}
